using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace N2L_Need_2_Log.gui
{
    /// <summary>
    /// Classe che definisce il comportamento del form FormFirstAccess
    /// </summary>
    public partial class FormFirstAccess : Form
    {
        /// <summary>
        /// costruttore della classe FormFirstAccess
        /// </summary>
        public FormFirstAccess()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.textBoxUsername.Text) || String.Equals(this.textBoxUsername.Text, ""))
            {
                Properties.Settings.Default.username = "User";
                Properties.Settings.Default.Save();
            }
            if (!this.textBoxPassword.Text.Equals(this.textBoxConfirmPassword.Text))
            {
                string message = "Le password non corrispondono, impossibile settare la password.";
                string caption = "Errore in input!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(this, message, caption, buttons);
            }
            else
            {
                if (String.IsNullOrEmpty(this.textBoxPassword.Text) || String.Equals(this.textBoxPassword.Text, ""))    
                {
                    Properties.Settings.Default.password = "";
                }
                else
                {
                    Properties.Settings.Default.password = core.Cript.ComputeHash(this.textBoxPassword.Text, null);
                    try
                    {
                        var db = core.DBConnection.Connect;
                        Properties.Settings.Default.logged = true;
                        this.Close();
                    }
                    catch (SQLiteException sqle)
                    {
                        MessageBox.Show(this, sqle.Message, "Errore in creazione DB n.° " + sqle.ErrorCode, MessageBoxButtons.OK);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message, "Generic Error!", MessageBoxButtons.OK);
                        this.Close();
                    }
                }
            }
        }
        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxPassword.Text != String.Empty && this.textBoxConfirmPassword.Text != String.Empty)
            {
                if (this.textBoxPassword.Text.Equals(this.textBoxConfirmPassword.Text))
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
        private void textBoxConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxPassword.Text != String.Empty && this.textBoxConfirmPassword.Text != String.Empty)
            {
                if (this.textBoxPassword.Text.Equals(this.textBoxConfirmPassword.Text))
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
