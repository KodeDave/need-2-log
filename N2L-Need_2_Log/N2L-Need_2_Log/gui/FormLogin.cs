using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace N2L_Need_2_Log
{
    /// <summary>
    /// Classe che definisce il comportamento del form FormLogin
    /// </summary>
    public partial class FormLogin : Form
    {
        /// <summary>
        /// Costruttore della classe FormLogin
        /// </summary>
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
                this.labelWrongPassword.Visible = false;
                string message = "Bentornato! Premi Invio per accedere";
                string caption = "Ciao " + Properties.Settings.Default.username;
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(this, message, caption, buttons);

                Properties.Settings.Default.logged = true;

                this.Close();
            }
            catch (SQLiteException sqle)
            {
                Properties.Settings.Default.logged = false;
                this.labelWrongPassword.Visible = true;
                this.textBoxPwd.SelectAll();
            }
        }
        private void buttonExit_Click(object sender, EventArgs e)
        {
            core.Controller.OnClose();
            this.Close();
            
        }
    }
}
