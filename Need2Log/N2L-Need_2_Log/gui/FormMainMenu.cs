using System;
using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;
using Core;

namespace N2L_Need_2_Log.gui
{
    /// <summary>
    /// Classe che definisce il comportamento del form FormMainMenu
    /// </summary>
    public partial class FormMainMenu : Form
    {
        /// <summary>
        /// costruttore della classe FormMainMenu
        /// </summary>
        public FormMainMenu()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAboutBox fab = new FormAboutBox();
            fab.Activate();
            fab.ShowDialog(this);
        }
        private void opzioniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOption fo = new FormOption();
            fo.Activate();
            fo.ShowDialog(this);
        }
        private void esciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FormMainMenu_Load(object sender, EventArgs e)
        {
            this.Text = System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            this.listViewMainMenu.View = Properties.Settings.Default.MainMenuView;
            switch (this.listViewMainMenu.View)
            {
                case (View.LargeIcon):
                    this.iconegrandiToolStripMenuItem.Checked = true;
                    this.iconepiccoleToolStripMenuItem.Checked = false;
                    this.listaToolStripMenuItem.Checked = false;
                    break;
                case (View.SmallIcon):
                    this.iconegrandiToolStripMenuItem.Checked = false;
                    this.iconepiccoleToolStripMenuItem.Checked = true;
                    this.listaToolStripMenuItem.Checked = false;
                    break;
                case (View.List):
                    this.iconegrandiToolStripMenuItem.Checked = false;
                    this.iconepiccoleToolStripMenuItem.Checked = false;
                    this.listaToolStripMenuItem.Checked = true;
                    break;
                default:
                    break;
            }
            //Sign In or Sign Up
            if (!Controller.DbExist)
            {
                FormFirstAccess ffa = new FormFirstAccess();
                ffa.Activate();
                ffa.ShowDialog(this);
            }
            else
            {
                FormLogin fl = new FormLogin();
                fl.Activate();
                fl.ShowDialog(this);
            }
            //If user correctly signed in
            if (Controller.Logged == true)
            {
                try
                {
                    updateListViewMainMenu();
                }
                catch(DbException dbe)
                {
                    string message = "Errore durante la ricerca dei record presenti!\nErrore: " + dbe.Message;
                    string caption = "SQLite error n.: " + dbe.ErrorCode;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(this, message, caption, buttons);
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }

        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "sqlite3";
            saveFileDialog.Title = "Backup";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "file di database SQLite (*.sqlite3)|*.sqlite3|Tutti i file (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                DBConnector db = DBConnector.Connect;
                db.Backup(fileName, String.Empty);
            }
        }
        private void listViewMainMenu_ItemActivate(object sender, EventArgs e)
        {
            int Item = Convert.ToInt32(listViewMainMenu.FocusedItem.SubItems[1].Text);
            DetailedInfo detailedInfoForm = new DetailedInfo(Item);
            detailedInfoForm.Activate();
            detailedInfoForm.ShowDialog(this);
        }
        private void FormMainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Controller.OnClose();
        }
        private void iconegrandiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listViewMainMenu.View = View.LargeIcon;
            Properties.Settings.Default.MainMenuView = this.listViewMainMenu.View;
            Properties.Settings.Default.Save();
            this.iconegrandiToolStripMenuItem.Checked = true;
            this.iconepiccoleToolStripMenuItem.Checked = false;
            this.listaToolStripMenuItem.Checked = false;
        }
        private void iconepiccoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listViewMainMenu.View = View.SmallIcon;
            Properties.Settings.Default.MainMenuView = this.listViewMainMenu.View;
            Properties.Settings.Default.Save();
            this.iconegrandiToolStripMenuItem.Checked = false;
            this.iconepiccoleToolStripMenuItem.Checked = true;
            this.listaToolStripMenuItem.Checked = false;
        }
        private void listaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listViewMainMenu.View = View.List;
            Properties.Settings.Default.MainMenuView = this.listViewMainMenu.View;
            Properties.Settings.Default.Save();
            this.iconegrandiToolStripMenuItem.Checked = false;
            this.iconepiccoleToolStripMenuItem.Checked = false;
            this.listaToolStripMenuItem.Checked = true;
        }
        private void nuovoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCreate createRecordForm = new FormCreate();
            createRecordForm.Activate();
            createRecordForm.ShowDialog(this);
            updateListViewMainMenu();
        }
        private void modificaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int Item = System.Convert.ToInt32(listViewMainMenu.FocusedItem.SubItems[1].Text);
                FormCreate modifyRecordForm = new FormCreate(Item);
                modifyRecordForm.Activate();
                modifyRecordForm.ShowDialog(this);
                updateListViewMainMenu();
            }
            catch(Exception)
            {

            }
        }
        private void eliminaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem focusedItem = listViewMainMenu.FocusedItem;
            if (focusedItem != null)
            {
                int itemID = Convert.ToInt32(focusedItem.SubItems[1].Text);
                string message = "Sei sicuro di voler eliminare l'account " +
                    focusedItem.Text + "?\n" +
                    "Questa operazione non è annullabile una volta eseguita.";
                string caption = "Conferma cancellazione";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show(this, message, caption, buttons);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        DBConnector.Connect.Erase(itemID);
                    }
                    catch (DbException dbe)
                    {
                        message = "Errore durante cancellazione del record selezionato!\nErrore: " + dbe.Message;
                        caption = "SQLite error n.: " + dbe.ErrorCode;
                        buttons = MessageBoxButtons.OK;
                        result = MessageBox.Show(this, message, caption, buttons);
                    }
                }
                updateListViewMainMenu();
            }
        }
        private void listViewMainMenu_MouseClick(object sender, MouseEventArgs e)
        {
            ListView listView = sender as ListView;
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem item = listView.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    item.Selected = true;
                    item.Focused = true;
                    this.contextMenuStrip1.Show(listView, e.Location);
                }
            }
        }
        private void apriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Item = Convert.ToInt32(listViewMainMenu.FocusedItem.SubItems[1].Text);
            DetailedInfo detailedInfoForm = new DetailedInfo(Item);
            detailedInfoForm.Activate();
            detailedInfoForm.ShowDialog(this);
        }
        private void modificaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int Item = Convert.ToInt32(listViewMainMenu.FocusedItem.SubItems[1].Text);
            FormCreate modifyRecordForm = new FormCreate(Item);
            modifyRecordForm.Activate();
            modifyRecordForm.ShowDialog(this);
            updateListViewMainMenu();
        }
        private void eliminaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListViewItem focusedItem = listViewMainMenu.FocusedItem;
            int itemID = Convert.ToInt32(focusedItem.SubItems[1].Text);
            string message = "Sei sicuro di voler eliminare l'account " + focusedItem.Text + "?\n" +
                "Questa operazione non è annullabile una volta eseguita.";
            string caption = "Conferma cancellazione";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            result = MessageBox.Show(this, message, caption, buttons);
            if (result == DialogResult.Yes)
            {
                try
                {
                    DBConnector.Connect.Erase(itemID);
                }
                catch (DbException dbe)
                {
                    message = "Errore durante cancellazione del record selezionato!\nErrore: " + dbe.Message;
                    caption = "SQLite error n.: " + dbe.ErrorCode;
                    buttons = MessageBoxButtons.OK;
                    result = MessageBox.Show(this, message, caption, buttons);
                }
            }
            updateListViewMainMenu();
        }
        private void updateListViewMainMenu()
        {
            try
            {
                this.listViewMainMenu.Items.Clear();
                var database = DBConnector.Connect;
                DbDataReader data = database.GetMainView();
                ImageList imageListSmall = new ImageList();
                imageListSmall.ImageSize = new Size(64, 64);
                ImageList imageListLarge = new ImageList();
                imageListLarge.ImageSize = new Size(128, 128);

                int i = 0;
                while (data.Read())
                {
                    imageListLarge.Images.Add(Image.FromFile("..\\..\\..\\Core\\Resources\\Pers\\icon\\" + data["IMAGE"]));
                    imageListSmall.Images.Add(Image.FromFile("..\\..\\..\\Core\\Resources\\Pers\\icon\\" + data["IMAGE"]));
                    var item = this.listViewMainMenu.Items.Add(data["NAME"].ToString(), data["NAME"].ToString(), i);
                    item.SubItems.Add(data["ID"].ToString());
                    item.SubItems.Add(data["URL"].ToString());
                    i++;
                }

                listViewMainMenu.LargeImageList = imageListLarge;
                listViewMainMenu.SmallImageList = imageListSmall;

                if(this.listViewMainMenu.FocusedItem == null && listViewMainMenu.Items.Count > 0)
                {
                    try
                    {
                        this.listViewMainMenu.FocusedItem = this.listViewMainMenu.TopItem;
                    }
                    catch (InvalidOperationException) { }
                }
            }
            catch (DbException dbe)
            {
                throw dbe;
            }
        }
        private void nuovoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormCreate createRecordForm = new FormCreate();
            createRecordForm.Activate();
            createRecordForm.ShowDialog(this);
            updateListViewMainMenu();
        }
        private void backupToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "sqlite3";
            saveFileDialog.Title = "Backup";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "file di database SQLite (*.sqlite3)|*.sqlite3|Tutti i file (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                DBConnector db = DBConnector.Connect;
                db.Backup(fileName, String.Empty);
            }
        }
    }
}
