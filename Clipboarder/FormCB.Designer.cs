namespace Clipboarder
{
   partial class FormCB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCB));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.mainData = new System.Windows.Forms.DataGridView();
            this.entryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entryUse = new System.Windows.Forms.DataGridViewButtonColumn();
            this.entryContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelNav = new System.Windows.Forms.Panel();
            this.toolbarEdit = new System.Windows.Forms.ToolStrip();
            this.buttonSaveChange = new System.Windows.Forms.ToolStripButton();
            this.buttonEdit = new System.Windows.Forms.ToolStripButton();
            this.statusInfo = new System.Windows.Forms.StatusStrip();
            this.statusTotalEntries = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusDbPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.clipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whatToListenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listenTextContents = new System.Windows.Forms.ToolStripMenuItem();
            this.listenImageContents = new System.Windows.Forms.ToolStripMenuItem();
            this.listenHTMLContents = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.hideAfterCopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stayOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearEntriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.showTextContents = new System.Windows.Forms.ToolStripMenuItem();
            this.showImageContents = new System.Windows.Forms.ToolStripMenuItem();
            this.showHTMLContents = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.openDatabaseLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shortcutsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainData)).BeginInit();
            this.toolbarEdit.SuspendLayout();
            this.statusInfo.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.mainData);
            this.splitContainer.Panel1.Controls.Add(this.panelNav);
            this.splitContainer.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.toolbarEdit);
            // 
            // mainData
            // 
            this.mainData.AllowUserToAddRows = false;
            this.mainData.BackgroundColor = System.Drawing.SystemColors.Window;
            this.mainData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mainData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mainData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.entryName,
            this.entryUse,
            this.entryContent});
            resources.ApplyResources(this.mainData, "mainData");
            this.mainData.GridColor = System.Drawing.SystemColors.ControlLight;
            this.mainData.Name = "mainData";
            this.mainData.RowHeadersVisible = false;
            this.mainData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mainData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mainData_CellClick);
            this.mainData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mainData_CellContentClick);
            this.mainData.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.mainData_CellEndEdit);
            this.mainData.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.mainData_UserDeletingRow);
            // 
            // entryName
            // 
            resources.ApplyResources(this.entryName, "entryName");
            this.entryName.Name = "entryName";
            // 
            // entryUse
            // 
            resources.ApplyResources(this.entryUse, "entryUse");
            this.entryUse.Name = "entryUse";
            this.entryUse.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.entryUse.Text = "Copy";
            // 
            // entryContent
            // 
            this.entryContent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.entryContent, "entryContent");
            this.entryContent.Name = "entryContent";
            this.entryContent.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.entryContent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panelNav
            // 
            resources.ApplyResources(this.panelNav, "panelNav");
            this.panelNav.Name = "panelNav";
            // 
            // toolbarEdit
            // 
            resources.ApplyResources(this.toolbarEdit, "toolbarEdit");
            this.toolbarEdit.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.toolbarEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonSaveChange,
            this.buttonEdit});
            this.toolbarEdit.Name = "toolbarEdit";
            // 
            // buttonSaveChange
            // 
            resources.ApplyResources(this.buttonSaveChange, "buttonSaveChange");
            this.buttonSaveChange.Name = "buttonSaveChange";
            this.buttonSaveChange.Click += new System.EventHandler(this.buttonSaveChange_Click);
            // 
            // buttonEdit
            // 
            resources.ApplyResources(this.buttonEdit, "buttonEdit");
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // statusInfo
            // 
            this.statusInfo.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusTotalEntries,
            this.statusMessage,
            this.statusDbPath});
            resources.ApplyResources(this.statusInfo, "statusInfo");
            this.statusInfo.Name = "statusInfo";
            // 
            // statusTotalEntries
            // 
            this.statusTotalEntries.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusTotalEntries.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusTotalEntries.Name = "statusTotalEntries";
            this.statusTotalEntries.Padding = new System.Windows.Forms.Padding(2);
            resources.ApplyResources(this.statusTotalEntries, "statusTotalEntries");
            // 
            // statusMessage
            // 
            this.statusMessage.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusMessage.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusMessage.Name = "statusMessage";
            this.statusMessage.Padding = new System.Windows.Forms.Padding(2);
            resources.ApplyResources(this.statusMessage, "statusMessage");
            // 
            // statusDbPath
            // 
            this.statusDbPath.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusDbPath.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusDbPath.IsLink = true;
            this.statusDbPath.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.statusDbPath.Name = "statusDbPath";
            this.statusDbPath.Padding = new System.Windows.Forms.Padding(2);
            resources.ApplyResources(this.statusDbPath, "statusDbPath");
            this.statusDbPath.Click += new System.EventHandler(this.statusDbPath_Click);
            // 
            // menuMain
            // 
            this.menuMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clipboardToolStripMenuItem,
            this.viewToolStripMenuItem});
            resources.ApplyResources(this.menuMain, "menuMain");
            this.menuMain.Name = "menuMain";
            // 
            // clipboardToolStripMenuItem
            // 
            this.clipboardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listenToolStripMenuItem,
            this.whatToListenToolStripMenuItem,
            this.toolStripSeparator4,
            this.hideAfterCopyToolStripMenuItem,
            this.stayOnTopToolStripMenuItem,
            this.toolStripSeparator6,
            this.shortcutsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.clipboardToolStripMenuItem.Name = "clipboardToolStripMenuItem";
            resources.ApplyResources(this.clipboardToolStripMenuItem, "clipboardToolStripMenuItem");
            // 
            // listenToolStripMenuItem
            // 
            this.listenToolStripMenuItem.Checked = true;
            this.listenToolStripMenuItem.CheckOnClick = true;
            this.listenToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.listenToolStripMenuItem.Name = "listenToolStripMenuItem";
            resources.ApplyResources(this.listenToolStripMenuItem, "listenToolStripMenuItem");
            this.listenToolStripMenuItem.Click += new System.EventHandler(this.listenToolStripMenuItem_Click);
            // 
            // whatToListenToolStripMenuItem
            // 
            this.whatToListenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listenTextContents,
            this.listenImageContents,
            this.listenHTMLContents});
            this.whatToListenToolStripMenuItem.Name = "whatToListenToolStripMenuItem";
            resources.ApplyResources(this.whatToListenToolStripMenuItem, "whatToListenToolStripMenuItem");
            // 
            // listenTextContents
            // 
            this.listenTextContents.Checked = true;
            this.listenTextContents.CheckOnClick = true;
            this.listenTextContents.CheckState = System.Windows.Forms.CheckState.Checked;
            this.listenTextContents.Name = "listenTextContents";
            resources.ApplyResources(this.listenTextContents, "listenTextContents");
            // 
            // listenImageContents
            // 
            this.listenImageContents.Checked = true;
            this.listenImageContents.CheckOnClick = true;
            this.listenImageContents.CheckState = System.Windows.Forms.CheckState.Checked;
            this.listenImageContents.Name = "listenImageContents";
            resources.ApplyResources(this.listenImageContents, "listenImageContents");
            // 
            // listenHTMLContents
            // 
            this.listenHTMLContents.Checked = true;
            this.listenHTMLContents.CheckOnClick = true;
            this.listenHTMLContents.CheckState = System.Windows.Forms.CheckState.Checked;
            this.listenHTMLContents.Name = "listenHTMLContents";
            resources.ApplyResources(this.listenHTMLContents, "listenHTMLContents");
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // hideAfterCopyToolStripMenuItem
            // 
            this.hideAfterCopyToolStripMenuItem.CheckOnClick = true;
            this.hideAfterCopyToolStripMenuItem.Name = "hideAfterCopyToolStripMenuItem";
            resources.ApplyResources(this.hideAfterCopyToolStripMenuItem, "hideAfterCopyToolStripMenuItem");
            // 
            // stayOnTopToolStripMenuItem
            // 
            this.stayOnTopToolStripMenuItem.CheckOnClick = true;
            this.stayOnTopToolStripMenuItem.Name = "stayOnTopToolStripMenuItem";
            resources.ApplyResources(this.stayOnTopToolStripMenuItem, "stayOnTopToolStripMenuItem");
            this.stayOnTopToolStripMenuItem.Click += new System.EventHandler(this.stayOnTopToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.horizontalVerticalToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.toolStripSeparator2,
            this.clearEntriesToolStripMenuItem,
            this.toolStripSeparator3,
            this.showTextContents,
            this.showImageContents,
            this.showHTMLContents,
            this.toolStripSeparator5,
            this.openDatabaseLocationToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
            // 
            // horizontalVerticalToolStripMenuItem
            // 
            this.horizontalVerticalToolStripMenuItem.Name = "horizontalVerticalToolStripMenuItem";
            resources.ApplyResources(this.horizontalVerticalToolStripMenuItem, "horizontalVerticalToolStripMenuItem");
            this.horizontalVerticalToolStripMenuItem.Click += new System.EventHandler(this.horizontalVerticalToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            resources.ApplyResources(this.refreshToolStripMenuItem, "refreshToolStripMenuItem");
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // clearEntriesToolStripMenuItem
            // 
            this.clearEntriesToolStripMenuItem.Name = "clearEntriesToolStripMenuItem";
            resources.ApplyResources(this.clearEntriesToolStripMenuItem, "clearEntriesToolStripMenuItem");
            this.clearEntriesToolStripMenuItem.Click += new System.EventHandler(this.clearEntriesToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // showTextContents
            // 
            this.showTextContents.Checked = true;
            this.showTextContents.CheckOnClick = true;
            this.showTextContents.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showTextContents.Name = "showTextContents";
            resources.ApplyResources(this.showTextContents, "showTextContents");
            this.showTextContents.Click += new System.EventHandler(this.showTextContentsToolStripMenuItem_Click);
            // 
            // showImageContents
            // 
            this.showImageContents.Checked = true;
            this.showImageContents.CheckOnClick = true;
            this.showImageContents.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showImageContents.Name = "showImageContents";
            resources.ApplyResources(this.showImageContents, "showImageContents");
            this.showImageContents.Click += new System.EventHandler(this.showTextContentsToolStripMenuItem_Click);
            // 
            // showHTMLContents
            // 
            this.showHTMLContents.Checked = true;
            this.showHTMLContents.CheckOnClick = true;
            this.showHTMLContents.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showHTMLContents.Name = "showHTMLContents";
            resources.ApplyResources(this.showHTMLContents, "showHTMLContents");
            this.showHTMLContents.Click += new System.EventHandler(this.showTextContentsToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // openDatabaseLocationToolStripMenuItem
            // 
            this.openDatabaseLocationToolStripMenuItem.Name = "openDatabaseLocationToolStripMenuItem";
            resources.ApplyResources(this.openDatabaseLocationToolStripMenuItem, "openDatabaseLocationToolStripMenuItem");
            this.openDatabaseLocationToolStripMenuItem.Click += new System.EventHandler(this.openDatabaseLocationToolStripMenuItem_Click);
            // 
            // notifyIcon
            // 
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // shortcutsToolStripMenuItem
            // 
            this.shortcutsToolStripMenuItem.Name = "shortcutsToolStripMenuItem";
            resources.ApplyResources(this.shortcutsToolStripMenuItem, "shortcutsToolStripMenuItem");
            this.shortcutsToolStripMenuItem.Click += new System.EventHandler(this.shortcutsToolStripMenuItem_Click);
            // 
            // FormCB
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.statusInfo);
            this.Name = "FormCB";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCB_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.FormCB_Resize);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainData)).EndInit();
            this.toolbarEdit.ResumeLayout(false);
            this.toolbarEdit.PerformLayout();
            this.statusInfo.ResumeLayout(false);
            this.statusInfo.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.StatusStrip statusInfo;
        private System.Windows.Forms.DataGridView mainData;
        private System.Windows.Forms.Panel panelNav;
        private System.Windows.Forms.DataGridViewTextBoxColumn entryName;
        private System.Windows.Forms.DataGridViewButtonColumn entryUse;
        private System.Windows.Forms.DataGridViewTextBoxColumn entryContent;
        private System.Windows.Forms.ToolStripStatusLabel statusTotalEntries;
        private System.Windows.Forms.ToolStripStatusLabel statusMessage;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem clipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stayOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horizontalVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideAfterCopyToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolbarEdit;
        private System.Windows.Forms.ToolStripButton buttonSaveChange;
        private System.Windows.Forms.ToolStripButton buttonEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem clearEntriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem showTextContents;
        private System.Windows.Forms.ToolStripMenuItem showImageContents;
        private System.Windows.Forms.ToolStripMenuItem showHTMLContents;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem whatToListenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listenTextContents;
        private System.Windows.Forms.ToolStripMenuItem listenImageContents;
        private System.Windows.Forms.ToolStripMenuItem listenHTMLContents;
        private System.Windows.Forms.ToolStripStatusLabel statusDbPath;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem openDatabaseLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem shortcutsToolStripMenuItem;
    }
}

