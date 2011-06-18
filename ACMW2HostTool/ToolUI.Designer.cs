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
			this.playerListContextStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// playerList
			// 
			this.playerList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnIP,
            this.columnName,
            this.columnCountry,
            this.columnHostName});
			this.playerList.ContextMenuStrip = this.playerListContextStrip;
			this.playerList.FullRowSelect = true;
			this.playerList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.playerList.Location = new System.Drawing.Point(12, 39);
			this.playerList.Name = "playerList";
			this.playerList.Size = new System.Drawing.Size(560, 411);
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
			this.playerListContextStrip.Size = new System.Drawing.Size(153, 48);
			this.playerListContextStrip.Opening += new System.ComponentModel.CancelEventHandler(this.playerListContextStrip_Opening);
			// 
			// copyNameToolStripMenuItem
			// 
			this.copyNameToolStripMenuItem.Name = "copyNameToolStripMenuItem";
			this.copyNameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.copyNameToolStripMenuItem.Text = "Copy Name";
			this.copyNameToolStripMenuItem.Click += new System.EventHandler(this.copyNameToolStripMenuItem_Click);
			// 
			// deviceList
			// 
			this.deviceList.DisplayMember = "Description";
			this.deviceList.FormattingEnabled = true;
			this.deviceList.Location = new System.Drawing.Point(12, 12);
			this.deviceList.Name = "deviceList";
			this.deviceList.Size = new System.Drawing.Size(560, 21);
			this.deviceList.TabIndex = 2;
			this.deviceList.SelectedIndexChanged += new System.EventHandler(this.deviceList_SelectedIndexChanged);
			// 
			// ToolUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 462);
			this.Controls.Add(this.playerList);
			this.Controls.Add(this.deviceList);
			this.KeyPreview = true;
			this.Name = "ToolUI";
			this.Text = "AlicanC\'s Modern Warfare 2 Tool";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
			this.playerListContextStrip.ResumeLayout(false);
			this.ResumeLayout(false);

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
    }
}

