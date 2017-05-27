using System;
using System.Data.Common;
using System.Windows.Forms;
using Core;

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
            this.textBoxUsername.Text = Controller.Username;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!this.textBoxUsername.Text.Equals(Controller.Username))
            {
               Controller.Username = this.textBoxUsername.Text;
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
                string previousPwd = Controller.Password;
                try
                {
                    var database = DBConnector.Connect;
                    if (String.IsNullOrEmpty(this.textBoxPassword.Text))
                    {
                        database.ChangeSettings(String.Empty);
                    }
                    else
                    {
                        database.ChangeSettings(Cript.ComputeHash(this.textBoxPassword.Text, null));

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
                catch(DbException dbe)
                {
                    string message = "Impossibile modificare la password! Errore con il DB.\n" + dbe.Message;
                    string caption = "Operazione non riuscita!";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(this, message, caption, buttons);
                    if (result == DialogResult.OK)
                    {
                        this.Close();
                    }
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
