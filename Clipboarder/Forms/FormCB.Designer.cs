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
            this.toolStripNav = new System.Windows.Forms.ToolStrip();
            this.buttonFirstPage = new System.Windows.Forms.ToolStripButton();
            this.buttonLastPage = new System.Windows.Forms.ToolStripButton();
            this.buttonClearWhere = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.buttonFavorites = new System.Windows.Forms.ToolStripButton();
            this.buttonSearchName = new System.Windows.Forms.ToolStripButton();
            this.buttonSearchTimespan = new System.Windows.Forms.ToolStripButton();
            this.buttonSearchUrls = new System.Windows.Forms.ToolStripButton();
            this.statusInfo = new System.Windows.Forms.StatusStrip();
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
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.shortcutsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearEntriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.showTextContents = new System.Windows.Forms.ToolStripMenuItem();
            this.showImageContents = new System.Windows.Forms.ToolStripMenuItem();
            this.showHTMLContents = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.openDatabaseLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.mainData = new Clipboarder.EntryPanel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStripNav.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.statusInfo.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripNav
            // 
            resources.ApplyResources(this.toolStripNav, "toolStripNav");
            this.toolStripNav.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.toolStripNav.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonFirstPage,
            this.buttonLastPage,
            this.buttonClearWhere});
            this.toolStripNav.Name = "toolStripNav";
            // 
            // buttonFirstPage
            // 
            this.buttonFirstPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.buttonFirstPage, "buttonFirstPage");
            this.buttonFirstPage.Name = "buttonFirstPage";
            this.buttonFirstPage.Click += new System.EventHandler(this.NavBtn_Click);
            // 
            // buttonLastPage
            // 
            resources.ApplyResources(this.buttonLastPage, "buttonLastPage");
            this.buttonLastPage.Name = "buttonLastPage";
            this.buttonLastPage.Click += new System.EventHandler(this.NavBtn_Click);
            // 
            // buttonClearWhere
            // 
            resources.ApplyResources(this.buttonClearWhere, "buttonClearWhere");
            this.buttonClearWhere.Name = "buttonClearWhere";
            this.buttonClearWhere.Click += new System.EventHandler(this.buttonClearWhere_Click);
            // 
            // toolStripMain
            // 
            resources.ApplyResources(this.toolStripMain, "toolStripMain");
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonFavorites,
            this.buttonSearchName,
            this.buttonSearchTimespan,
            this.buttonSearchUrls});
            this.toolStripMain.Name = "toolStripMain";
            // 
            // buttonFavorites
            // 
            this.buttonFavorites.CheckOnClick = true;
            resources.ApplyResources(this.buttonFavorites, "buttonFavorites");
            this.buttonFavorites.Name = "buttonFavorites";
            this.buttonFavorites.Tag = "name";
            this.buttonFavorites.Click += new System.EventHandler(this.buttonFavorites_Click);
            // 
            // buttonSearchName
            // 
            resources.ApplyResources(this.buttonSearchName, "buttonSearchName");
            this.buttonSearchName.Name = "buttonSearchName";
            this.buttonSearchName.Tag = "name";
            this.buttonSearchName.Click += new System.EventHandler(this.buttonUrls_Click);
            // 
            // buttonSearchTimespan
            // 
            resources.ApplyResources(this.buttonSearchTimespan, "buttonSearchTimespan");
            this.buttonSearchTimespan.Name = "buttonSearchTimespan";
            this.buttonSearchTimespan.Tag = "time";
            this.buttonSearchTimespan.Click += new System.EventHandler(this.buttonUrls_Click);
            // 
            // buttonSearchUrls
            // 
            resources.ApplyResources(this.buttonSearchUrls, "buttonSearchUrls");
            this.buttonSearchUrls.Name = "buttonSearchUrls";
            this.buttonSearchUrls.Tag = "url";
            this.buttonSearchUrls.Click += new System.EventHandler(this.buttonUrls_Click);
            // 
            // statusInfo
            // 
            this.statusInfo.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMessage,
            this.statusDbPath});
            resources.ApplyResources(this.statusInfo, "statusInfo");
            this.statusInfo.Name = "statusInfo";
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
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // shortcutsToolStripMenuItem
            // 
            this.shortcutsToolStripMenuItem.Name = "shortcutsToolStripMenuItem";
            resources.ApplyResources(this.shortcutsToolStripMenuItem, "shortcutsToolStripMenuItem");
            this.shortcutsToolStripMenuItem.Click += new System.EventHandler(this.shortcutsToolStripMenuItem_Click);
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
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.toolStripSeparator2,
            this.clearEntriesToolStripMenuItem,
            this.searchDeleteToolStripMenuItem,
            this.toolStripSeparator3,
            this.showTextContents,
            this.showImageContents,
            this.showHTMLContents,
            this.toolStripSeparator5,
            this.openDatabaseLocationToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
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
            // searchDeleteToolStripMenuItem
            // 
            this.searchDeleteToolStripMenuItem.Name = "searchDeleteToolStripMenuItem";
            resources.ApplyResources(this.searchDeleteToolStripMenuItem, "searchDeleteToolStripMenuItem");
            this.searchDeleteToolStripMenuItem.Click += new System.EventHandler(this.searchDeleteToolStripMenuItem_Click);
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
            // mainData
            // 
            resources.ApplyResources(this.mainData, "mainData");
            this.mainData.Name = "mainData";
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
            // FormCB
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainData);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.toolStripNav);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.statusInfo);
            this.Name = "FormCB";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCB_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.FormCB_Resize);
            this.toolStripNav.ResumeLayout(false);
            this.toolStripNav.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.statusInfo.ResumeLayout(false);
            this.statusInfo.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.StatusStrip statusInfo;
        private System.Windows.Forms.ToolStripStatusLabel statusMessage;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem clipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stayOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideAfterCopyToolStripMenuItem;
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
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton buttonSearchUrls;
        private System.Windows.Forms.ToolStrip toolStripNav;
        private System.Windows.Forms.ToolStripButton buttonFirstPage;
        private System.Windows.Forms.ToolStripButton buttonLastPage;
        private System.Windows.Forms.ToolStripButton buttonClearWhere;
        private System.Windows.Forms.ToolStripButton buttonSearchTimespan;
        private System.Windows.Forms.ToolStripButton buttonSearchName;
        private EntryPanel mainData;
        private System.Windows.Forms.ToolStripMenuItem searchDeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton buttonFavorites;
    }
}

