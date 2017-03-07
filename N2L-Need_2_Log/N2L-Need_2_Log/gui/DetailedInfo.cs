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
    public partial class DetailedInfo : Form
    {
        public DetailedInfo(int item)
        {
//data["ID"]+data["NAME"]+data["IMAGE"]+data["NOTE"]+data["PASSWORD"]+data["URL"]+data["USERNAME"]);
            InitializeComponent();
            core.DBConnection database = core.DBConnection.Connect;
            System.Data.Common.DbDataReader data = database.GetRecordInfo(item);
            while (data.Read())
            {
                
                string name = data["NAME"].ToString();
                this.Text = name + " - " + System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                this.labelName.Text = name;
                string imagePath = @"..\..\Resources\pers\icon\" + data["IMAGE"];
                this.recordIcon.Image = Image.FromFile(imagePath);
                this.recordIcon.MaximumSize = new Size(128, 128);
                this.richTextBoxNote.Text = data["NOTE"].ToString();
                this.textBoxPassword.Text = data["PASSWORD"].ToString();
                this.linkLabelUrl.Text = data["URL"].ToString();
                this.textBoxUsername.Text = data["USERNAME"].ToString();
            }
        }

        private void buttonPassword_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                textBoxPassword.UseSystemPasswordChar = false;
            }
        }

        private void buttonPassword_MouseUp(object sender, MouseEventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = true;
        }

        private void linkLabelUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(linkLabelUrl.Text);
            }
            catch
                (
                 System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
