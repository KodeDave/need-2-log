using System;
using System.IO;
using System.Reflection;
using System.Data.Common;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Core;
using Core.DataStruct;

namespace N2L_Need_2_Log.gui
{
    /// <summary>
    /// Classe che definisce il comportamento del form DetailedInfo
    /// </summary>
    public partial class DetailedInfo : Form
    {
        private DBRecord record;
        private static int item;

        /// <summary>
        /// Costruttore della classe DetailedInfo
        /// </summary>
        /// <param name="itemID"></param>
        public DetailedInfo(int itemID)
        {
            InitializeComponent();
            item = itemID;
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
                Process.Start(linkLabelUrl.Text);
            }
            catch
                (Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (Exception other)
            {
                MessageBox.Show(this, "Error with your browser.",other.Message, MessageBoxButtons.OK);
                this.linkLabelUrl.Enabled = false;
            }
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void DetailedInfo_Load(object sender, EventArgs e)
        {
            try
            {
                DBConnector database = DBConnector.Connect;
                using (DbDataReader data = database.GetRecordInfo(item))
                {
                    while (data.Read())
                    {
                        record = new DBRecord(item, data["NAME"].ToString(),
                            data["IMAGE"].ToString(), data["NOTE"].ToString(),
                            data["PASSWORD"].ToString(), data["URL"].ToString(),
                            data["USERNAME"].ToString(), Convert.ToInt32(data["TYPE_ID"].ToString()));
                        this.Text = record.Name + " - " +
                            Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
                        this.labelName.Text = record.Name;
                        this.recordIcon.Image = Image.FromFile("..\\..\\..\\Core\\Resources\\Pers\\Icon\\" + record.Icon);
                        this.recordIcon.MaximumSize = new Size(128, 128);
                        this.richTextBoxNote.Text = record.Note;
                        this.textBoxPassword.Text = record.Password;
                        this.linkLabelUrl.Text = record.Url;
                        this.textBoxUsername.Text = record.Username;
                    }
                }
            }
            catch (DbException dbe)
            {
                MessageBox.Show(this, dbe.Message,
                    "Errore SQL n. " + dbe.ErrorCode, MessageBoxButtons.OK);
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
