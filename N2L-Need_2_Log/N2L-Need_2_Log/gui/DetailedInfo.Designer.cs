namespace N2L_Need_2_Log.gui
{
    partial class DetailedInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetailedInfo));
            this.recordIcon = new System.Windows.Forms.PictureBox();
            this.richTextBoxNote = new System.Windows.Forms.RichTextBox();
            this.buttonPassword = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelNote = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.linkLabelUrl = new System.Windows.Forms.LinkLabel();
            this.buttonOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.recordIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // recordIcon
            // 
            this.recordIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.recordIcon.ErrorImage = ((System.Drawing.Image)(resources.GetObject("recordIcon.ErrorImage")));
            this.recordIcon.InitialImage = ((System.Drawing.Image)(resources.GetObject("recordIcon.InitialImage")));
            this.recordIcon.Location = new System.Drawing.Point(13, 13);
            this.recordIcon.Name = "recordIcon";
            this.recordIcon.Size = new System.Drawing.Size(128, 128);
            this.recordIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.recordIcon.TabIndex = 0;
            this.recordIcon.TabStop = false;
            // 
            // richTextBoxNote
            // 
            this.richTextBoxNote.Location = new System.Drawing.Point(13, 212);
            this.richTextBoxNote.Name = "richTextBoxNote";
            this.richTextBoxNote.ReadOnly = true;
            this.richTextBoxNote.Size = new System.Drawing.Size(425, 138);
            this.richTextBoxNote.TabIndex = 1;
            this.richTextBoxNote.Text = "";
            // 
            // buttonPassword
            // 
            this.buttonPassword.Location = new System.Drawing.Point(363, 171);
            this.buttonPassword.Name = "buttonPassword";
            this.buttonPassword.Size = new System.Drawing.Size(75, 23);
            this.buttonPassword.TabIndex = 2;
            this.buttonPassword.Text = "Mostra";
            this.buttonPassword.UseVisualStyleBackColor = true;
            this.buttonPassword.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonPassword_MouseDown);
            this.buttonPassword.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonPassword_MouseUp);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxPassword.HideSelection = false;
            this.textBoxPassword.Location = new System.Drawing.Point(72, 173);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.ReadOnly = true;
            this.textBoxPassword.Size = new System.Drawing.Size(285, 20);
            this.textBoxPassword.TabIndex = 3;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // labelNote
            // 
            this.labelNote.AutoSize = true;
            this.labelNote.Location = new System.Drawing.Point(13, 196);
            this.labelNote.Name = "labelNote";
            this.labelNote.Size = new System.Drawing.Size(30, 13);
            this.labelNote.TabIndex = 4;
            this.labelNote.Text = "Note";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(13, 176);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(53, 13);
            this.labelPassword.TabIndex = 5;
            this.labelPassword.Text = "Password";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(13, 150);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(55, 13);
            this.labelUsername.TabIndex = 6;
            this.labelUsername.Text = "Username";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(72, 147);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.ReadOnly = true;
            this.textBoxUsername.Size = new System.Drawing.Size(285, 20);
            this.textBoxUsername.TabIndex = 7;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(147, 25);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(154, 26);
            this.labelName.TabIndex = 8;
            this.labelName.Text = "Record name";
            // 
            // linkLabelUrl
            // 
            this.linkLabelUrl.AutoSize = true;
            this.linkLabelUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelUrl.Location = new System.Drawing.Point(147, 103);
            this.linkLabelUrl.Name = "linkLabelUrl";
            this.linkLabelUrl.Size = new System.Drawing.Size(91, 20);
            this.linkLabelUrl.TabIndex = 9;
            this.linkLabelUrl.TabStop = true;
            this.linkLabelUrl.Text = "linkLabelUrl";
            this.linkLabelUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelUrl_LinkClicked);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(362, 357);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 10;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // DetailedInfo
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 392);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.linkLabelUrl);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelNote);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.buttonPassword);
            this.Controls.Add(this.richTextBoxNote);
            this.Controls.Add(this.recordIcon);
            this.Name = "DetailedInfo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "DetailedInfo";
            ((System.ComponentModel.ISupportInitialize)(this.recordIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox recordIcon;
        private System.Windows.Forms.RichTextBox richTextBoxNote;
        private System.Windows.Forms.Button buttonPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelNote;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.LinkLabel linkLabelUrl;
        private System.Windows.Forms.Button buttonOk;
    }
}