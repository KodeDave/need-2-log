using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace N2L_Need_2_Log.gui
{
    public partial class FormOption : Form
    {
        public FormOption()
        {
            InitializeComponent();
            this.textBoxUsername.Text = N2L_Need_2_Log.Properties.Settings.Default.username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!this.textBoxUsername.Text.Equals(N2L_Need_2_Log.Properties.Settings.Default.username))
            {
                N2L_Need_2_Log.Properties.Settings.Default.username = this.textBoxUsername.Text;
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
                var database = N2L_Need_2_Log.core.DBConnection.Connect;
                if(String.IsNullOrEmpty(this.textBoxPassword.Text))
                {
                    database.ChangeSettings("");
                    N2L_Need_2_Log.Properties.Settings.Default.password_hash = String.Empty;
                }
                else
                {
                    database.ChangeSettings(this.textBoxPassword.Text);
                    N2L_Need_2_Log.Properties.Settings.Default.password_hash = N2L_Need_2_Log.core.Cript.ComputeHash(this.textBoxPassword.Text, null);
                }
                string message = "La password è stata correttamente settata/modificata.";
                string caption = "Operazione completata!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(this, message, caption, buttons);
                if(result == DialogResult.OK)
                {
                    this.Close();
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
