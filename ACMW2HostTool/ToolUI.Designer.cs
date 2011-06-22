namespace ACMW2Tool
{
    partial class ToolUI
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
			this.playerList = new System.Windows.Forms.ListView();
			this.columnIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnCountry = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHostName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.playerListContextStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deviceList = new System.Windows.Forms.ComboBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.applicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.playerListContextStrip.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// playerList
			// 
			this.playerList.AllowColumnReorder = true;
			this.playerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.playerList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnIP,
            this.columnName,
            this.columnCountry,
            this.columnHostName});
			this.playerList.ContextMenuStrip = this.playerListContextStrip;
			this.playerList.FullRowSelect = true;
			this.playerList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.playerList.Location = new System.Drawing.Point(12, 54);
			this.playerList.Name = "playerList";
			this.playerList.Size = new System.Drawing.Size(560, 396);
			this.playerList.TabIndex = 1;
			this.playerList.UseCompatibleStateImageBehavior = false;
			this.playerList.View = System.Windows.Forms.View.Details;
			// 
			// columnIP
			// 
			this.columnIP.DisplayIndex = 2;
			this.columnIP.Text = "Player IP";
			this.columnIP.Width = 120;
			// 
			// columnName
			// 
			this.columnName.DisplayIndex = 0;
			this.columnName.Text = "Name";
			this.columnName.Width = 100;
			// 
			// columnCountry
			// 
			this.columnCountry.DisplayIndex = 1;
			this.columnCountry.Text = "Country";
			this.columnCountry.Width = 120;
			// 
			// columnHostName
			// 
			this.columnHostName.Text = "Host Name";
			this.columnHostName.Width = 210;
			// 
			// playerListContextStrip
			// 
			this.playerListContextStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyNameToolStripMenuItem});
			this.playerListContextStrip.Name = "playerListContextStrip";
			this.playerListContextStrip.Size = new System.Drawing.Size(138, 26);
			this.playerListContextStrip.Opening += new System.ComponentModel.CancelEventHandler(this.playerListContextStrip_Opening);
			// 
			// copyNameToolStripMenuItem
			// 
			this.copyNameToolStripMenuItem.Name = "copyNameToolStripMenuItem";
			this.copyNameToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.copyNameToolStripMenuItem.Text = "Copy Name";
			this.copyNameToolStripMenuItem.Click += new System.EventHandler(this.copyNameToolStripMenuItem_Click);
			// 
			// deviceList
			// 
			this.deviceList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.deviceList.DisplayMember = "Description";
			this.deviceList.FormattingEnabled = true;
			this.deviceList.Location = new System.Drawing.Point(12, 27);
			this.deviceList.Name = "deviceList";
			this.deviceList.Size = new System.Drawing.Size(560, 21);
			this.deviceList.TabIndex = 2;
			this.deviceList.SelectedIndexChanged += new System.EventHandler(this.deviceList_SelectedIndexChanged);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.menuStrip1.Size = new System.Drawing.Size(584, 24);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// applicationToolStripMenuItem
			// 
			this.applicationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
			this.applicationToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
			this.applicationToolStripMenuItem.Text = "Application";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// ToolUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 462);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.playerList);
			this.Controls.Add(this.deviceList);
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "ToolUI";
			this.Text = "AlicanC\'s Modern Warfare 2 Tool";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ToolUI_FormClosed);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
			this.playerListContextStrip.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.ColumnHeader columnIP;
        private System.Windows.Forms.ColumnHeader columnCountry;
        private System.Windows.Forms.ComboBox deviceList;
        private System.Windows.Forms.ColumnHeader columnHostName;
		private System.Windows.Forms.ContextMenuStrip playerListContextStrip;
		private System.Windows.Forms.ColumnHeader columnName;
		public System.Windows.Forms.ListView playerList;
		private System.Windows.Forms.ToolStripMenuItem copyNameToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem applicationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

