namespace NTDLS.WinFormsHelpers.Forms
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            pictureBoxLogo = new PictureBox();
            cmdOk = new Button();
            listViewVersions = new ListView();
            columnHeaderApplication = new ColumnHeader();
            columnHeaderVersion = new ColumnHeader();
            linkLabel1 = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxLogo
            // 
            pictureBoxLogo.BackgroundImageLayout = ImageLayout.Center;
            pictureBoxLogo.Image = (Image)resources.GetObject("pictureBoxLogo.Image");
            pictureBoxLogo.Location = new Point(18, 32);
            pictureBoxLogo.Margin = new Padding(4, 3, 4, 3);
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new Size(103, 108);
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxLogo.TabIndex = 0;
            pictureBoxLogo.TabStop = false;
            // 
            // cmdOk
            // 
            cmdOk.Location = new Point(394, 167);
            cmdOk.Margin = new Padding(4, 3, 4, 3);
            cmdOk.Name = "cmdOk";
            cmdOk.Size = new Size(88, 27);
            cmdOk.TabIndex = 7;
            cmdOk.Text = "Ok";
            cmdOk.UseVisualStyleBackColor = true;
            cmdOk.Click += ButtonOk_Click;
            // 
            // listViewVersions
            // 
            listViewVersions.Columns.AddRange(new ColumnHeader[] { columnHeaderApplication, columnHeaderVersion });
            listViewVersions.FullRowSelect = true;
            listViewVersions.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listViewVersions.Location = new Point(150, 12);
            listViewVersions.Margin = new Padding(4, 3, 4, 3);
            listViewVersions.MultiSelect = false;
            listViewVersions.Name = "listViewVersions";
            listViewVersions.Size = new Size(332, 149);
            listViewVersions.Sorting = SortOrder.Ascending;
            listViewVersions.TabIndex = 8;
            listViewVersions.UseCompatibleStateImageBehavior = false;
            listViewVersions.View = View.Details;
            // 
            // columnHeaderApplication
            // 
            columnHeaderApplication.Text = "Application";
            columnHeaderApplication.Width = 100;
            // 
            // columnHeaderVersion
            // 
            columnHeaderVersion.Text = "Version";
            columnHeaderVersion.Width = 75;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Microsoft Sans Serif", 11F);
            linkLabel1.Location = new Point(150, 170);
            linkLabel1.Margin = new Padding(4, 0, 4, 0);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(164, 18);
            linkLabel1.TabIndex = 10;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "www.NetworkDLS.com";
            linkLabel1.LinkClicked += LinkWebsiteNtdls_LinkClicked;
            // 
            // FormAbout
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(497, 202);
            Controls.Add(linkLabel1);
            Controls.Add(listViewVersions);
            Controls.Add(cmdOk);
            Controls.Add(pictureBoxLogo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormAbout";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "NetworkDLS";
            Load += FormAbout_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxLogo;
        private Button cmdOk;
        private ListView listViewVersions;
        private ColumnHeader columnHeaderApplication;
        private ColumnHeader columnHeaderVersion;
        private LinkLabel linkLabel1;
    }
}