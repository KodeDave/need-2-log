using System;
using System.Data.Common;
using System.Windows.Forms;
using Core;

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
            Controller.Password = Cript.ComputeHash(this.textBoxPwd.Text, null);
            try
            {
                var db = DBConnector.Connect;
                this.labelWrongPassword.Visible = false;
                string message = "Bentornato! Premi Invio per accedere";
                string caption = "Ciao " + Controller.Username;
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(this, message, caption, buttons);

                Controller.Logged = true;

                this.Close();
            }
            catch (DbException)
            {
                Controller.Logged = false;
                this.labelWrongPassword.Visible = true;
                this.textBoxPwd.SelectAll();
            }
        }
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Controller.OnClose();
            this.Close();
        }
    }
}
