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
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mainMenu = new System.Windows.Forms.MenuItem();
            this.listenToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.listenTextContents = new System.Windows.Forms.MenuItem();
            this.listenImageContents = new System.Windows.Forms.MenuItem();
            this.listenHTMLContents = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.stayOnTopToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.hideAfterCopyToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.searchContent = new System.Windows.Forms.MenuItem();
            this.searchTimespan = new System.Windows.Forms.MenuItem();
            this.searchURL = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.openDatabase = new System.Windows.Forms.MenuItem();
            this.openDatabaseLocation = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.cleanDatabase = new System.Windows.Forms.MenuItem();
            this.menuPages = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.showTextContents = new System.Windows.Forms.MenuItem();
            this.showImageContents = new System.Windows.Forms.MenuItem();
            this.showHTMLContents = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.showAscendingOrder = new System.Windows.Forms.MenuItem();
            this.showDescendingOrder = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.showFavorites = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.viewSettings = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mainData = new Clipboarder.EntryPanel();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mainMenu,
            this.menuItem8,
            this.menuPages,
            this.menuItem1,
            this.menuItem10});
            // 
            // mainMenu
            // 
            this.mainMenu.Index = 0;
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.listenToolStripMenuItem,
            this.listenTextContents,
            this.listenImageContents,
            this.listenHTMLContents,
            this.menuItem7,
            this.stayOnTopToolStripMenuItem,
            this.hideAfterCopyToolStripMenuItem,
            this.menuItem4,
            this.exitToolStripMenuItem});
            resources.ApplyResources(this.mainMenu, "mainMenu");
            // 
            // listenToolStripMenuItem
            // 
            this.listenToolStripMenuItem.Index = 0;
            resources.ApplyResources(this.listenToolStripMenuItem, "listenToolStripMenuItem");
            this.listenToolStripMenuItem.Click += new System.EventHandler(this.listenToolStripMenuItem_Click);
            // 
            // listenTextContents
            // 
            this.listenTextContents.Index = 1;
            resources.ApplyResources(this.listenTextContents, "listenTextContents");
            // 
            // listenImageContents
            // 
            this.listenImageContents.Index = 2;
            resources.ApplyResources(this.listenImageContents, "listenImageContents");
            // 
            // listenHTMLContents
            // 
            this.listenHTMLContents.Index = 3;
            resources.ApplyResources(this.listenHTMLContents, "listenHTMLContents");
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 4;
            resources.ApplyResources(this.menuItem7, "menuItem7");
            // 
            // stayOnTopToolStripMenuItem
            // 
            this.stayOnTopToolStripMenuItem.Index = 5;
            resources.ApplyResources(this.stayOnTopToolStripMenuItem, "stayOnTopToolStripMenuItem");
            this.stayOnTopToolStripMenuItem.Click += new System.EventHandler(this.stayOnTopToolStripMenuItem_Click);
            // 
            // hideAfterCopyToolStripMenuItem
            // 
            this.hideAfterCopyToolStripMenuItem.Index = 6;
            resources.ApplyResources(this.hideAfterCopyToolStripMenuItem, "hideAfterCopyToolStripMenuItem");
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 7;
            resources.ApplyResources(this.menuItem4, "menuItem4");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Index = 8;
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 1;
            this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.searchContent,
            this.searchTimespan,
            this.searchURL,
            this.menuItem9,
            this.openDatabase,
            this.openDatabaseLocation,
            this.menuItem11,
            this.cleanDatabase});
            resources.ApplyResources(this.menuItem8, "menuItem8");
            // 
            // searchContent
            // 
            this.searchContent.Index = 0;
            resources.ApplyResources(this.searchContent, "searchContent");
            this.searchContent.Tag = "name";
            // 
            // searchTimespan
            // 
            this.searchTimespan.Index = 1;
            resources.ApplyResources(this.searchTimespan, "searchTimespan");
            this.searchTimespan.Tag = "time";
            // 
            // searchURL
            // 
            this.searchURL.Index = 2;
            resources.ApplyResources(this.searchURL, "searchURL");
            this.searchURL.Tag = "url";
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 3;
            resources.ApplyResources(this.menuItem9, "menuItem9");
            // 
            // openDatabase
            // 
            this.openDatabase.Index = 4;
            resources.ApplyResources(this.openDatabase, "openDatabase");
            this.openDatabase.Click += new System.EventHandler(this.statusDbPath_Click);
            // 
            // openDatabaseLocation
            // 
            this.openDatabaseLocation.Index = 5;
            resources.ApplyResources(this.openDatabaseLocation, "openDatabaseLocation");
            this.openDatabaseLocation.Click += new System.EventHandler(this.openDatabaseLocationToolStripMenuItem_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 6;
            resources.ApplyResources(this.menuItem11, "menuItem11");
            // 
            // cleanDatabase
            // 
            this.cleanDatabase.Index = 7;
            resources.ApplyResources(this.cleanDatabase, "cleanDatabase");
            this.cleanDatabase.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuPages
            // 
            this.menuPages.Index = 2;
            resources.ApplyResources(this.menuPages, "menuPages");
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 3;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.showTextContents,
            this.showImageContents,
            this.showHTMLContents,
            this.menuItem2,
            this.showAscendingOrder,
            this.showDescendingOrder,
            this.menuItem3,
            this.showFavorites,
            this.menuItem6,
            this.viewSettings});
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // showTextContents
            // 
            this.showTextContents.Index = 0;
            resources.ApplyResources(this.showTextContents, "showTextContents");
            // 
            // showImageContents
            // 
            this.showImageContents.Index = 1;
            resources.ApplyResources(this.showImageContents, "showImageContents");
            // 
            // showHTMLContents
            // 
            this.showHTMLContents.Index = 2;
            resources.ApplyResources(this.showHTMLContents, "showHTMLContents");
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 3;
            resources.ApplyResources(this.menuItem2, "menuItem2");
            // 
            // showAscendingOrder
            // 
            this.showAscendingOrder.Index = 4;
            this.showAscendingOrder.RadioCheck = true;
            resources.ApplyResources(this.showAscendingOrder, "showAscendingOrder");
            this.showAscendingOrder.Click += new System.EventHandler(this.showAscendingOrder_Click);
            // 
            // showDescendingOrder
            // 
            this.showDescendingOrder.Checked = true;
            this.showDescendingOrder.Index = 5;
            this.showDescendingOrder.RadioCheck = true;
            resources.ApplyResources(this.showDescendingOrder, "showDescendingOrder");
            this.showDescendingOrder.Click += new System.EventHandler(this.showAscendingOrder_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 6;
            resources.ApplyResources(this.menuItem3, "menuItem3");
            // 
            // showFavorites
            // 
            this.showFavorites.Index = 7;
            resources.ApplyResources(this.showFavorites, "showFavorites");
            this.showFavorites.Click += new System.EventHandler(this.showFavorites_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 8;
            resources.ApplyResources(this.menuItem6, "menuItem6");
            // 
            // viewSettings
            // 
            this.viewSettings.Index = 9;
            resources.ApplyResources(this.viewSettings, "viewSettings");
            this.viewSettings.Click += new System.EventHandler(this.shortcutsToolStripMenuItem_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 4;
            this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem12});
            resources.ApplyResources(this.menuItem10, "menuItem10");
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 0;
            resources.ApplyResources(this.menuItem12, "menuItem12");
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
            // mainData
            // 
            resources.ApplyResources(this.mainData, "mainData");
            this.mainData.Name = "mainData";
            // 
            // FormCB
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.mainData);
            this.Menu = this.mainMenu1;
            this.Name = "FormCB";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCB_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.FormCB_Resize);
            this.ResumeLayout(false);

      }

      #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private EntryPanel mainData;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem mainMenu;
        private System.Windows.Forms.MenuItem listenToolStripMenuItem;
        private System.Windows.Forms.MenuItem listenTextContents;
        private System.Windows.Forms.MenuItem listenImageContents;
        private System.Windows.Forms.MenuItem listenHTMLContents;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem showTextContents;
        private System.Windows.Forms.MenuItem showImageContents;
        private System.Windows.Forms.MenuItem showHTMLContents;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem showFavorites;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem exitToolStripMenuItem;
        private System.Windows.Forms.MenuItem searchContent;
        private System.Windows.Forms.MenuItem searchTimespan;
        private System.Windows.Forms.MenuItem searchURL;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem hideAfterCopyToolStripMenuItem;
        private System.Windows.Forms.MenuItem stayOnTopToolStripMenuItem;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem viewSettings;
        private System.Windows.Forms.MenuItem openDatabase;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItem11;
        private System.Windows.Forms.MenuItem openDatabaseLocation;
        private System.Windows.Forms.MenuItem showAscendingOrder;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem showDescendingOrder;
        private System.Windows.Forms.MenuItem menuPages;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem menuItem12;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem cleanDatabase;
    }
}

