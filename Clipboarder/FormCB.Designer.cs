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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonSaveChange = new System.Windows.Forms.Button();
            this.statusInfo = new System.Windows.Forms.StatusStrip();
            this.statusTotalEntries = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.clipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stayOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hideAfterCopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainData)).BeginInit();
            this.panel1.SuspendLayout();
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
            this.splitContainer.Panel2.Controls.Add(this.panel1);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonEdit);
            this.panel1.Controls.Add(this.buttonSaveChange);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // buttonEdit
            // 
            resources.ApplyResources(this.buttonEdit, "buttonEdit");
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonSaveChange
            // 
            resources.ApplyResources(this.buttonSaveChange, "buttonSaveChange");
            this.buttonSaveChange.Name = "buttonSaveChange";
            this.buttonSaveChange.UseVisualStyleBackColor = true;
            this.buttonSaveChange.Click += new System.EventHandler(this.buttonSaveChange_Click);
            // 
            // statusInfo
            // 
            this.statusInfo.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusTotalEntries,
            this.statusMessage});
            resources.ApplyResources(this.statusInfo, "statusInfo");
            this.statusInfo.Name = "statusInfo";
            // 
            // statusTotalEntries
            // 
            this.statusTotalEntries.Name = "statusTotalEntries";
            resources.ApplyResources(this.statusTotalEntries, "statusTotalEntries");
            // 
            // statusMessage
            // 
            this.statusMessage.Name = "statusMessage";
            resources.ApplyResources(this.statusMessage, "statusMessage");
            // 
            // menuMain
            // 
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
            this.hideAfterCopyToolStripMenuItem,
            this.stayOnTopToolStripMenuItem,
            this.toolStripSeparator2,
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
            // stayOnTopToolStripMenuItem
            // 
            this.stayOnTopToolStripMenuItem.CheckOnClick = true;
            this.stayOnTopToolStripMenuItem.Name = "stayOnTopToolStripMenuItem";
            resources.ApplyResources(this.stayOnTopToolStripMenuItem, "stayOnTopToolStripMenuItem");
            this.stayOnTopToolStripMenuItem.Click += new System.EventHandler(this.stayOnTopToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
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
            this.horizontalVerticalToolStripMenuItem,
            this.refreshToolStripMenuItem});
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
            // hideAfterCopyToolStripMenuItem
            // 
            this.hideAfterCopyToolStripMenuItem.CheckOnClick = true;
            this.hideAfterCopyToolStripMenuItem.Name = "hideAfterCopyToolStripMenuItem";
            resources.ApplyResources(this.hideAfterCopyToolStripMenuItem, "hideAfterCopyToolStripMenuItem");
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
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonSaveChange;
        private System.Windows.Forms.Panel panelNav;
        private System.Windows.Forms.DataGridViewTextBoxColumn entryName;
        private System.Windows.Forms.DataGridViewButtonColumn entryUse;
        private System.Windows.Forms.DataGridViewTextBoxColumn entryContent;
        private System.Windows.Forms.ToolStripStatusLabel statusTotalEntries;
        private System.Windows.Forms.Button buttonEdit;
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
    }
}

