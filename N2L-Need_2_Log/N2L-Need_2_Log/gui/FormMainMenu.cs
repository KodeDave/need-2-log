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
    public partial class FormMainMenu : Form
    {
        public FormMainMenu()
        {
            InitializeComponent();
            this.Text = System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
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
            if(!Properties.Settings.Default.db_exist)
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
            if (N2L_Need_2_Log.Properties.Settings.Default.logged.Equals(true))
            {
                var database = N2L_Need_2_Log.core.DBConnection.Connect;
                System.Data.Common.DbDataReader data = database.GetMainView();
                // Create two ImageList objects.
                ImageList imageListSmall = new ImageList();
                imageListSmall.ImageSize = new Size(64, 64);
                ImageList imageListLarge = new ImageList();
                imageListLarge.ImageSize = new Size(128, 128);
                //Assign the ImageList objects to the ListView.

                int i = 0;
                while (data.Read())
                {
                    imageListLarge.Images.Add(Image.FromFile(@"..\..\Resources\pers\icon\" + data["IMAGE"]));
                    imageListSmall.Images.Add(Image.FromFile(@"..\..\Resources\pers\icon\" + data["IMAGE"]));
                    //data["ID"]/data["NAME"]/data["IMAGE"]/data["URL"]);
                    var item = this.listViewMainMenu.Items.Add(data["NAME"].ToString(), data["NAME"].ToString(), i);
                    item.SubItems.Add(data["ID"].ToString());
                    item.SubItems.Add(data["URL"].ToString());
                    i++;
                }

                listViewMainMenu.LargeImageList = imageListLarge;
                listViewMainMenu.SmallImageList = imageListSmall;
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
                core.DBConnection db = core.DBConnection.Connect;
                db.Backup(fileName, String.Empty);
            }
        }

        private void esportaInXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.Title = "Esporta Database";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "File di markup (*.xml)|*.xml|File di testo(*.txt)|*.txt|Tutti i file (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                core.DBConnection db = core.DBConnection.Connect;
                //db.getData().WriteXml(fileName);
            }
        }
        
        private void listViewMainMenu_ItemActivate(object sender, EventArgs e)
        {
            int Item = System.Convert.ToInt32(listViewMainMenu.FocusedItem.SubItems[1].Text);
            DetailedInfo detailedInfoForm = new DetailedInfo(Item);
            detailedInfoForm.Activate();
            detailedInfoForm.ShowDialog(this);
        }

        private void FormMainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            N2L_Need_2_Log.core.Controller.OnClose();
        }
    }
}
