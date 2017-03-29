namespace N2L_Need_2_Log.gui
{
    partial class FormMainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainMenu));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuovoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.strumentiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opzioniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.esciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visualizzaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iconegrandiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iconepiccoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewMainMenu = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.apriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuovoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.strumentiToolStripMenuItem,
            this.toolStripMenuItem2,
            this.visualizzaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(500, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuovoToolStripMenuItem,
            this.modificaToolStripMenuItem,
            this.eliminaToolStripMenuItem,
            this.backupToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // nuovoToolStripMenuItem
            // 
            this.nuovoToolStripMenuItem.Name = "nuovoToolStripMenuItem";
            this.nuovoToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.nuovoToolStripMenuItem.Text = "&Nuovo";
            this.nuovoToolStripMenuItem.Click += new System.EventHandler(this.nuovoToolStripMenuItem_Click);
            // 
            // modificaToolStripMenuItem
            // 
            this.modificaToolStripMenuItem.Name = "modificaToolStripMenuItem";
            this.modificaToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.modificaToolStripMenuItem.Text = "&Modifica";
            this.modificaToolStripMenuItem.Click += new System.EventHandler(this.modificaToolStripMenuItem_Click);
            // 
            // eliminaToolStripMenuItem
            // 
            this.eliminaToolStripMenuItem.Name = "eliminaToolStripMenuItem";
            this.eliminaToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.eliminaToolStripMenuItem.Text = "&Elimina";
            this.eliminaToolStripMenuItem.Click += new System.EventHandler(this.eliminaToolStripMenuItem_Click);
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.backupToolStripMenuItem.Text = "&Backup";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
            this.toolStripMenuItem1.Text = "Crea DB di backup";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // strumentiToolStripMenuItem
            // 
            this.strumentiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opzioniToolStripMenuItem,
            this.esciToolStripMenuItem});
            this.strumentiToolStripMenuItem.Name = "strumentiToolStripMenuItem";
            this.strumentiToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.strumentiToolStripMenuItem.Text = "&Strumenti";
            // 
            // opzioniToolStripMenuItem
            // 
            this.opzioniToolStripMenuItem.Name = "opzioniToolStripMenuItem";
            this.opzioniToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.opzioniToolStripMenuItem.Text = "&Opzioni";
            this.opzioniToolStripMenuItem.Click += new System.EventHandler(this.opzioniToolStripMenuItem_Click);
            // 
            // esciToolStripMenuItem
            // 
            this.esciToolStripMenuItem.Name = "esciToolStripMenuItem";
            this.esciToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.esciToolStripMenuItem.Text = "&Esci";
            this.esciToolStripMenuItem.Click += new System.EventHandler(this.esciToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItem2.Text = "&?";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.aboutToolStripMenuItem.Text = "&Informazioni su...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // visualizzaToolStripMenuItem
            // 
            this.visualizzaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iconegrandiToolStripMenuItem,
            this.iconepiccoleToolStripMenuItem,
            this.listaToolStripMenuItem});
            this.visualizzaToolStripMenuItem.Name = "visualizzaToolStripMenuItem";
            this.visualizzaToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.visualizzaToolStripMenuItem.Text = "&Visualizza";
            // 
            // iconegrandiToolStripMenuItem
            // 
            this.iconegrandiToolStripMenuItem.Name = "iconegrandiToolStripMenuItem";
            this.iconegrandiToolStripMenuItem.ShowShortcutKeys = false;
            this.iconegrandiToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.iconegrandiToolStripMenuItem.Text = "&Icone grandi";
            this.iconegrandiToolStripMenuItem.Click += new System.EventHandler(this.iconegrandiToolStripMenuItem_Click);
            // 
            // iconepiccoleToolStripMenuItem
            // 
            this.iconepiccoleToolStripMenuItem.Name = "iconepiccoleToolStripMenuItem";
            this.iconepiccoleToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.iconepiccoleToolStripMenuItem.Text = "Icone &piccole";
            this.iconepiccoleToolStripMenuItem.Click += new System.EventHandler(this.iconepiccoleToolStripMenuItem_Click);
            // 
            // listaToolStripMenuItem
            // 
            this.listaToolStripMenuItem.Name = "listaToolStripMenuItem";
            this.listaToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.listaToolStripMenuItem.Text = "&Lista";
            this.listaToolStripMenuItem.Click += new System.EventHandler(this.listaToolStripMenuItem_Click);
            // 
            // listViewMainMenu
            // 
            this.listViewMainMenu.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.listViewMainMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMainMenu.Location = new System.Drawing.Point(0, 24);
            this.listViewMainMenu.MultiSelect = false;
            this.listViewMainMenu.Name = "listViewMainMenu";
            this.listViewMainMenu.ShowGroups = false;
            this.listViewMainMenu.Size = new System.Drawing.Size(500, 274);
            this.listViewMainMenu.TabIndex = 1;
            this.listViewMainMenu.UseCompatibleStateImageBehavior = false;
            this.listViewMainMenu.ItemActivate += new System.EventHandler(this.listViewMainMenu_ItemActivate);
            this.listViewMainMenu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewMainMenu_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.apriToolStripMenuItem,
            this.modificaToolStripMenuItem1,
            this.eliminaToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(122, 70);
            // 
            // apriToolStripMenuItem
            // 
            this.apriToolStripMenuItem.Name = "apriToolStripMenuItem";
            this.apriToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.apriToolStripMenuItem.Text = "&Apri";
            this.apriToolStripMenuItem.Click += new System.EventHandler(this.apriToolStripMenuItem_Click);
            // 
            // modificaToolStripMenuItem1
            // 
            this.modificaToolStripMenuItem1.Name = "modificaToolStripMenuItem1";
            this.modificaToolStripMenuItem1.Size = new System.Drawing.Size(121, 22);
            this.modificaToolStripMenuItem1.Text = "&Modifica";
            this.modificaToolStripMenuItem1.Click += new System.EventHandler(this.modificaToolStripMenuItem1_Click);
            // 
            // eliminaToolStripMenuItem1
            // 
            this.eliminaToolStripMenuItem1.Name = "eliminaToolStripMenuItem1";
            this.eliminaToolStripMenuItem1.Size = new System.Drawing.Size(121, 22);
            this.eliminaToolStripMenuItem1.Text = "&Elimina";
            this.eliminaToolStripMenuItem1.Click += new System.EventHandler(this.eliminaToolStripMenuItem1_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuovoToolStripMenuItem1,
            this.backupToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(114, 48);
            // 
            // nuovoToolStripMenuItem1
            // 
            this.nuovoToolStripMenuItem1.Name = "nuovoToolStripMenuItem1";
            this.nuovoToolStripMenuItem1.Size = new System.Drawing.Size(113, 22);
            this.nuovoToolStripMenuItem1.Text = "Nuovo";
            this.nuovoToolStripMenuItem1.Click += new System.EventHandler(this.nuovoToolStripMenuItem1_Click);
            // 
            // backupToolStripMenuItem1
            // 
            this.backupToolStripMenuItem1.Name = "backupToolStripMenuItem1";
            this.backupToolStripMenuItem1.Size = new System.Drawing.Size(113, 22);
            this.backupToolStripMenuItem1.Text = "Backup";
            this.backupToolStripMenuItem1.Click += new System.EventHandler(this.backupToolStripMenuItem1_Click);
            // 
            // FormMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(500, 298);
            this.Controls.Add(this.listViewMainMenu);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMainMenu";
            this.Text = "N2L - Need2Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMainMenu_FormClosing);
            this.Load += new System.EventHandler(this.FormMainMenu_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuovoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem strumentiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem opzioniToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem esciToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ListView listViewMainMenu;
        private System.Windows.Forms.ToolStripMenuItem visualizzaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iconegrandiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iconepiccoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listaToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem apriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eliminaToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem nuovoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem1;
    }
}