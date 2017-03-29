using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Data.Common;
using System.Windows.Forms;
using N2L_Need_2_Log.core;
using N2L_Need_2_Log.core.DataStruct;

namespace N2L_Need_2_Log.gui
{
    /// <summary>
    /// Classe che definisce il comportamento del form FormCreate
    /// </summary>
    public partial class FormCreate : Form
    {
        private bool newRecord;
        private int item;
        private DBConnection database;
        private DBRecord record;
        private List<RecordTypeItem> recordList = new List<RecordTypeItem>();

        /// <summary>
        /// Costruttore della classe FormCreate utile per creare un nuovo record
        /// </summary>
        public FormCreate()
        {
            InitializeComponent();
            newRecord = true;
        }
        /// <summary>
        /// Costruttore della classe FormCreate utile per modificare un record già esistente
        /// </summary>
        /// <param name="itemID">id del record che si vuole modificare</param>
        public FormCreate(int itemID)
        {
            InitializeComponent();
            newRecord = false;
            item = itemID;
        }
        
        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxShowPassword.Checked == true)
            {
                this.textBoxPassword.UseSystemPasswordChar = true;
                this.checkBoxShowPassword.Text = "Nascondi";
            }
            else
            {
                this.textBoxPassword.UseSystemPasswordChar = false;
                this.checkBoxShowPassword.Text = "Mostra";
            }
        }
        private void FormCreate_Load(object sender, EventArgs e)
        {
            try
            {
                this.database = core.DBConnection.Connect;
                using (DbDataReader data = database.GetRecordsType())
                {
                    while (data.Read())
                    {
                        recordList.Add(new RecordTypeItem(Convert.ToInt32(data["id"].ToString()),
                            data["name"].ToString(), data["default_icon"].ToString(), data["url"].ToString()));
                        this.comboBoxAccountType.Items.Add(this.recordList.Last().GetName());
                    }
                }
                this.comboBoxAccountType.SelectedIndex = 0;
                if(!newRecord)
                {
                    DBConnection database = DBConnection.Connect;
                    using (DbDataReader data = database.GetRecordInfo(item))
                    {
                        while (data.Read())
                        {
                            record = new DBRecord(item, data["NAME"].ToString(),
                                    data["IMAGE"].ToString(), data["NOTE"].ToString(),
                                    data["PASSWORD"].ToString(), data["URL"].ToString(),
                                    data["USERNAME"].ToString(), Convert.ToInt32(data["TYPE_ID"].ToString()));
                        }
                        this.comboBoxAccountType.SelectedIndex = record.TypeId -1;
                        RecordTypeItem itemRec = recordList.ElementAt(this.comboBoxAccountType.SelectedIndex);
                        this.Text = "N2L - Need2Log | " + record.Name;
                        this.textBoxName.Text = record.Name;
                        this.pictureBox.Image = Image.FromFile(@"..\..\Resources\pers\icon\" + itemRec.GetIcon());
                        this.pictureBox.MaximumSize = new Size(128, 128);
                        this.richTextBoxNote.Text = record.Note;
                        this.textBoxPassword.Text = record.Password;
                        this.textBoxUrl.Text = record.Url;
                        this.textBoxUsername.Text = record.Username;
                    }
                }
            }
            catch(SQLiteException sqle)
            {
                throw sqle;
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (newRecord)
            {
                try
                {
                    string entryName = database.CheckEntryName(textBoxName.Text);
                    this.textBoxName.Text = entryName;
                    int typeId = this.comboBoxAccountType.SelectedIndex + 1;
                    string noteValue = this.richTextBoxNote.Text;
                    string passwordValue = this.textBoxPassword.Text;
                    string urlValue = this.textBoxUrl.Text;
                    string usernameValue = this.textBoxUsername.Text;
                    database.Insert(entryName, typeId, noteValue, passwordValue, urlValue, usernameValue);
                    this.Close();
                }
                catch (SQLiteException sqle)
                {
                    string message = "Errore in creazione nuovo record!\nErrore: " + sqle.Message;
                    string caption = "SQLite error n.°: " + sqle.ErrorCode;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(this, message, caption, buttons);
                }
            }
            else
            {
                try
                {
                    record.Name = this.textBoxName.Name;
                    record.TypeId = this.comboBoxAccountType.SelectedIndex + 1;
                    record.Note = this.richTextBoxNote.Text;
                    record.Password = this.textBoxPassword.Text;
                    record.Url = this.textBoxUrl.Text;
                    record.Username = this.textBoxUsername.Text;
                    using (DbDataReader data = database.GetRecordsType())
                    {
                        RecordTypeItem item = recordList.ElementAt(this.comboBoxAccountType.SelectedIndex);
                        record.Icon = item.GetIcon();
                    }
                    database.Update(record.Id, record.Name, record.TypeId, record.Icon, record.Note, record.Password, record.Url, record.Username);
                    this.Close();
                }
                catch(SQLiteException sqle)
                {
                    string message = "Errore in modifica del record!\nErrore: " + sqle.Message;
                    string caption = "SQLite error n.°: " + sqle.ErrorCode;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(this, message, caption, buttons);
                }
            }
        }
        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.textBoxName.Text))
            {
                this.Text = "N2L - Need2Log";
            }
            else
            {
                this.Text = "N2L - Need2Log | " + this.textBoxName.Text;
            }
        }
        private void FormCreate_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        private void comboBoxAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecordTypeItem item = recordList.ElementAt(this.comboBoxAccountType.SelectedIndex);
            if (newRecord)
            {
                this.textBoxName.Text = item.GetName();
            }
            this.pictureBox.Image = Image.FromFile(@"..\..\Resources\pers\icon\" + item.GetIcon());
            this.pictureBox.MaximumSize = new Size(128, 128);
            this.textBoxUrl.Text = item.GetUrl();
        }
    }
}
