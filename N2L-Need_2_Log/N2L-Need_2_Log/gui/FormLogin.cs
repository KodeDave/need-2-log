using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace N2L_Need_2_Log
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.textBoxPwd.Text))
            {
                Properties.Settings.Default.password = "";
            }
            else
            {
                Properties.Settings.Default.password = core.Cript.ComputeHash(this.textBoxPwd.Text, null);
            }
            try
            {
                var db = core.DBConnection.Connect;

                string message = "Bentornato! Premi Invio per accedere";
                string caption = "Ciao " + N2L_Need_2_Log.Properties.Settings.Default.username;
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(this, message, caption, buttons);

                N2L_Need_2_Log.Properties.Settings.Default.logged = true;

                this.Close();
            }
            catch (System.Data.SQLite.SQLiteException sqle)
            {
                    this.labelWrongPassword.Visible = true;
                    this.textBoxPwd.SelectAll();
            }
/*            if (String.IsNullOrEmpty(N2L_Need_2_Log.Properties.Settings.Default.password_hash) &&
                (String.IsNullOrEmpty(this.textBoxPwd.Text) || this.textBoxPwd.Text == ""))
            {
                N2L_Need_2_Log.Properties.Settings.Default.password = "";
                N2L_Need_2_Log.Properties.Settings.Default.logged = true;
            }
            else
            {
                string hash = N2L_Need_2_Log.core.Cript.ComputeHash(this.textBoxPwd.Text, null);
                if (N2L_Need_2_Log.core.Cript.Confirm(this.textBoxPwd.Text, N2L_Need_2_Log.Properties.Settings.Default.password_hash))
                {
                    N2L_Need_2_Log.Properties.Settings.Default.password = this.textBoxPwd.Text;
                    N2L_Need_2_Log.Properties.Settings.Default.logged = true;
                }
                else
                {
                    this.labelWrongPassword.Visible = true;
                    this.textBoxPwd.SelectAll();
                }
            }
            if (N2L_Need_2_Log.Properties.Settings.Default.logged.Equals(true))
            {
                string message = "Bentornato! Premi Invio per accedere";
                string caption = "Ciao " + N2L_Need_2_Log.Properties.Settings.Default.username;
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(this, message, caption, buttons);
                this.Close();
            }*/
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
