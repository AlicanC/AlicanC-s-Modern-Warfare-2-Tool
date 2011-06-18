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
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.kickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deviceList = new System.Windows.Forms.ComboBox();
			this.debugInfo = new System.Windows.Forms.ListView();
			this.infoColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.playerListContextStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
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
			this.playerList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.playerList.FullRowSelect = true;
			this.playerList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.playerList.Location = new System.Drawing.Point(0, 0);
			this.playerList.Name = "playerList";
			this.playerList.Size = new System.Drawing.Size(622, 303);
			this.playerList.TabIndex = 1;
			this.playerList.UseCompatibleStateImageBehavior = false;
			this.playerList.View = System.Windows.Forms.View.Details;
			// 
			// columnIP
			// 
			this.columnIP.Text = "Player IP";
			this.columnIP.Width = 100;
			// 
			// columnName
			// 
			this.columnName.Text = "Player Name";
			this.columnName.Width = 120;
			// 
			// columnCountry
			// 
			this.columnCountry.Text = "Country";
			this.columnCountry.Width = 160;
			// 
			// columnHostName
			// 
			this.columnHostName.Text = "Host Name";
			this.columnHostName.Width = 200;
			// 
			// playerListContextStrip
			// 
			this.playerListContextStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyNameToolStripMenuItem,
            this.toolStripSeparator1,
            this.kickToolStripMenuItem,
            this.dosToolStripMenuItem});
			this.playerListContextStrip.Name = "playerListContextStrip";
			this.playerListContextStrip.Size = new System.Drawing.Size(153, 98);
			this.playerListContextStrip.Opening += new System.ComponentModel.CancelEventHandler(this.playerListContextStrip_Opening);
			// 
			// copyNameToolStripMenuItem
			// 
			this.copyNameToolStripMenuItem.Name = "copyNameToolStripMenuItem";
			this.copyNameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.copyNameToolStripMenuItem.Text = "Copy Name";
			this.copyNameToolStripMenuItem.Click += new System.EventHandler(this.copyNameToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(134, 6);
			// 
			// kickToolStripMenuItem
			// 
			this.kickToolStripMenuItem.Name = "kickToolStripMenuItem";
			this.kickToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.kickToolStripMenuItem.Text = "Kick";
			// 
			// dosToolStripMenuItem
			// 
			this.dosToolStripMenuItem.Name = "dosToolStripMenuItem";
			this.dosToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.dosToolStripMenuItem.Text = "DOS";
			// 
			// deviceList
			// 
			this.deviceList.DisplayMember = "Description";
			this.deviceList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.deviceList.FormattingEnabled = true;
			this.deviceList.Location = new System.Drawing.Point(0, 0);
			this.deviceList.Name = "deviceList";
			this.deviceList.Size = new System.Drawing.Size(622, 21);
			this.deviceList.TabIndex = 2;
			this.deviceList.SelectedIndexChanged += new System.EventHandler(this.deviceList_SelectedIndexChanged);
			// 
			// debugInfo
			// 
			this.debugInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.infoColumn});
			this.debugInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.debugInfo.Location = new System.Drawing.Point(0, 0);
			this.debugInfo.Name = "debugInfo";
			this.debugInfo.Size = new System.Drawing.Size(622, 179);
			this.debugInfo.TabIndex = 3;
			this.debugInfo.UseCompatibleStateImageBehavior = false;
			this.debugInfo.View = System.Windows.Forms.View.Details;
			// 
			// infoColumn
			// 
			this.infoColumn.Text = "Info";
			this.infoColumn.Width = 500;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.debugInfo);
			this.splitContainer1.Size = new System.Drawing.Size(622, 516);
			this.splitContainer1.SplitterDistance = 332;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 4;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer2.IsSplitterFixed = true;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.deviceList);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.playerList);
			this.splitContainer2.Size = new System.Drawing.Size(622, 332);
			this.splitContainer2.SplitterDistance = 25;
			this.splitContainer2.TabIndex = 3;
			// 
			// ToolUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(622, 516);
			this.Controls.Add(this.splitContainer1);
			this.KeyPreview = true;
			this.Name = "ToolUI";
			this.Text = "AlicanC\'s Modern Warfare 2 Tool";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
			this.playerListContextStrip.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.ColumnHeader columnIP;
        private System.Windows.Forms.ColumnHeader columnCountry;
        private System.Windows.Forms.ComboBox deviceList;
        private System.Windows.Forms.ColumnHeader columnHostName;
        private System.Windows.Forms.ContextMenuStrip playerListContextStrip;
        private System.Windows.Forms.ToolStripMenuItem kickToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem dosToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader infoColumn;
        private System.Windows.Forms.ColumnHeader columnName;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		public System.Windows.Forms.ListView playerList;
		public System.Windows.Forms.ListView debugInfo;
		private System.Windows.Forms.ToolStripMenuItem copyNameToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

