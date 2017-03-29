using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace N2L_Need_2_Log.gui
{
    /// <summary>
    /// Classe che definisce il comportamento del form FormOption
    /// </summary>
    public partial class FormOption : Form
    {
        /// <summary>
        /// Costruttore della classe FormOption
        /// </summary>
        public FormOption()
        {
            InitializeComponent();
            this.textBoxUsername.Text = Properties.Settings.Default.username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!this.textBoxUsername.Text.Equals(Properties.Settings.Default.username))
            {
                Properties.Settings.Default.username = this.textBoxUsername.Text;
                Properties.Settings.Default.Save();
            }
            if (!this.textBoxPassword.Text.Equals(this.textBoxPasswordConfirm.Text))
            {
                string message = "Le password non corrispondono, impossibile settare/modificare la password.";
                string caption = "Errore in input!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(this, message, caption, buttons);
            }
            else
            {
                string previousPwd = Properties.Settings.Default.password;
                try
                {
                    var database = core.DBConnection.Connect;
                    if (String.IsNullOrEmpty(this.textBoxPassword.Text))
                    {
                        database.ChangeSettings("");
                        Properties.Settings.Default.password = String.Empty;
                    }
                    else
                    {
                        Properties.Settings.Default.password = core.Cript.ComputeHash(this.textBoxPassword.Text, null);
                        database.ChangeSettings(Properties.Settings.Default.password);

                    }
                    string message = "La password è stata correttamente settata/modificata.";
                    string caption = "Operazione completata!";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(this, message, caption, buttons);
                    if (result == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
                catch(SQLiteException sqle)
                {
                    string message = "Impossibile modificare la password! Errore con il DB.\n"+sqle.Message;
                    string caption = "Operazione non riuscita!";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(this, message, caption, buttons);
                    if (result == DialogResult.OK)
                    {
                        this.Close();
                    }
                    Properties.Settings.Default.password = previousPwd;
                }
            }

        }
        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            if(this.textBoxPassword.Text != String.Empty)
            {
                this.textBoxPasswordConfirm.Enabled = true;
            }
            else
            {
                this.textBoxPasswordConfirm.Enabled = false;
            }
            if (this.textBoxPassword.Text != String.Empty && this.textBoxPasswordConfirm.Text != String.Empty)
            {
                if (this.textBoxPassword.Text.Equals(this.textBoxPasswordConfirm.Text))
                {
                    this.labelPasswordError.Visible = false;
                    this.labelPasswordConfirmed.Visible = true;
                }
                else
                {
                    this.labelPasswordConfirmed.Visible = false;
                    this.labelPasswordError.Visible = true;
                }
            }
            else
            {
                this.labelPasswordConfirmed.Visible = false;
                this.labelPasswordError.Visible = false;
            }


        }
        private void textBoxPasswordConfirm_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxPassword.Text != String.Empty && this.textBoxPasswordConfirm.Text != String.Empty)
            {
                if (this.textBoxPassword.Text.Equals(this.textBoxPasswordConfirm.Text))
                {
                    this.labelPasswordError.Visible = false;
                    this.labelPasswordConfirmed.Visible = true;
                }
                else
                {
                    this.labelPasswordConfirmed.Visible = false;
                    this.labelPasswordError.Visible = true;
                }
            }
            else
            {
                this.labelPasswordConfirmed.Visible = false;
                this.labelPasswordError.Visible = false;
            }
        }
    }
}
