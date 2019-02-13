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
            this.statusInfo = new System.Windows.Forms.StatusStrip();
            this.cberMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.listenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listenToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listenTextContents = new System.Windows.Forms.ToolStripMenuItem();
            this.listenImageContents = new System.Windows.Forms.ToolStripMenuItem();
            this.listenHTMLContents = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteAllEntriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchDeleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.stayOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideAfterCopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusDbPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.showFavoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainData = new Clipboarder.EntryPanel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusInfo
            // 
            this.statusInfo.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cberMenu,
            this.statusDbPath});
            resources.ApplyResources(this.statusInfo, "statusInfo");
            this.statusInfo.Name = "statusInfo";
            // 
            // cberMenu
            // 
            this.cberMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cberMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listenToolStripMenuItem,
            this.listenToToolStripMenuItem,
            this.openToolStripMenuItem,
            this.selectToolStripMenuItem,
            this.toolStripSeparator5,
            this.showFavoritesToolStripMenuItem,
            this.toolStripSeparator1,
            this.searchToolStripMenuItem,
            this.deleteAllEntriesToolStripMenuItem,
            this.searchDeleteToolStripMenuItem1,
            this.toolStripSeparator2,
            this.stayOnTopToolStripMenuItem,
            this.hideAfterCopyToolStripMenuItem,
            this.toolStripSeparator3,
            this.refreshToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            resources.ApplyResources(this.cberMenu, "cberMenu");
            this.cberMenu.Name = "cberMenu";
            // 
            // listenToolStripMenuItem
            // 
            this.listenToolStripMenuItem.Checked = true;
            this.listenToolStripMenuItem.CheckOnClick = true;
            this.listenToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.listenToolStripMenuItem.Name = "listenToolStripMenuItem";
            resources.ApplyResources(this.listenToolStripMenuItem, "listenToolStripMenuItem");
            // 
            // listenToToolStripMenuItem
            // 
            this.listenToToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listenTextContents,
            this.listenImageContents,
            this.listenHTMLContents});
            this.listenToToolStripMenuItem.Name = "listenToToolStripMenuItem";
            resources.ApplyResources(this.listenToToolStripMenuItem, "listenToToolStripMenuItem");
            // 
            // listenTextContents
            // 
            this.listenTextContents.CheckOnClick = true;
            this.listenTextContents.Name = "listenTextContents";
            resources.ApplyResources(this.listenTextContents, "listenTextContents");
            // 
            // listenImageContents
            // 
            this.listenImageContents.CheckOnClick = true;
            this.listenImageContents.Name = "listenImageContents";
            resources.ApplyResources(this.listenImageContents, "listenImageContents");
            // 
            // listenHTMLContents
            // 
            this.listenHTMLContents.CheckOnClick = true;
            this.listenHTMLContents.Name = "listenHTMLContents";
            resources.ApplyResources(this.listenHTMLContents, "listenHTMLContents");
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openDatabaseLocationToolStripMenuItem_Click);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            resources.ApplyResources(this.selectToolStripMenuItem, "selectToolStripMenuItem");
            this.selectToolStripMenuItem.Click += new System.EventHandler(this.statusDbPath_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // deleteAllEntriesToolStripMenuItem
            // 
            this.deleteAllEntriesToolStripMenuItem.Name = "deleteAllEntriesToolStripMenuItem";
            resources.ApplyResources(this.deleteAllEntriesToolStripMenuItem, "deleteAllEntriesToolStripMenuItem");
            this.deleteAllEntriesToolStripMenuItem.Click += new System.EventHandler(this.clearEntriesToolStripMenuItem_Click);
            // 
            // searchDeleteToolStripMenuItem1
            // 
            this.searchDeleteToolStripMenuItem1.Name = "searchDeleteToolStripMenuItem1";
            resources.ApplyResources(this.searchDeleteToolStripMenuItem1, "searchDeleteToolStripMenuItem1");
            this.searchDeleteToolStripMenuItem1.Click += new System.EventHandler(this.searchDeleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // stayOnTopToolStripMenuItem
            // 
            this.stayOnTopToolStripMenuItem.CheckOnClick = true;
            this.stayOnTopToolStripMenuItem.Name = "stayOnTopToolStripMenuItem";
            resources.ApplyResources(this.stayOnTopToolStripMenuItem, "stayOnTopToolStripMenuItem");
            this.stayOnTopToolStripMenuItem.Click += new System.EventHandler(this.stayOnTopToolStripMenuItem_Click);
            // 
            // hideAfterCopyToolStripMenuItem
            // 
            this.hideAfterCopyToolStripMenuItem.CheckOnClick = true;
            this.hideAfterCopyToolStripMenuItem.Name = "hideAfterCopyToolStripMenuItem";
            resources.ApplyResources(this.hideAfterCopyToolStripMenuItem, "hideAfterCopyToolStripMenuItem");
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            resources.ApplyResources(this.refreshToolStripMenuItem, "refreshToolStripMenuItem");
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.shortcutsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // statusDbPath
            // 
            this.statusDbPath.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.statusDbPath.Name = "statusDbPath";
            this.statusDbPath.Padding = new System.Windows.Forms.Padding(2);
            resources.ApplyResources(this.statusDbPath, "statusDbPath");
            this.statusDbPath.Spring = true;
            // 
            // notifyIcon
            // 
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            resources.ApplyResources(this.searchToolStripMenuItem, "searchToolStripMenuItem");
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // showFavoritesToolStripMenuItem
            // 
            this.showFavoritesToolStripMenuItem.Name = "showFavoritesToolStripMenuItem";
            resources.ApplyResources(this.showFavoritesToolStripMenuItem, "showFavoritesToolStripMenuItem");
            this.showFavoritesToolStripMenuItem.Click += new System.EventHandler(this.showFavoritesToolStripMenuItem_Click);
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
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.mainData);
            this.Controls.Add(this.statusInfo);
            this.Name = "FormCB";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCB_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.FormCB_Resize);
            this.statusInfo.ResumeLayout(false);
            this.statusInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.StatusStrip statusInfo;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripStatusLabel statusDbPath;
        private EntryPanel mainData;
        private System.Windows.Forms.ToolStripDropDownButton cberMenu;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllEntriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem searchDeleteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem stayOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideAfterCopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listenToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listenTextContents;
        private System.Windows.Forms.ToolStripMenuItem listenImageContents;
        private System.Windows.Forms.ToolStripMenuItem listenHTMLContents;
        private System.Windows.Forms.ToolStripMenuItem showFavoritesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
    }
}

