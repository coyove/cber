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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.statusInfo = new System.Windows.Forms.StatusStrip();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mainData = new System.Windows.Forms.DataGridView();
            this.entryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entryUse = new System.Windows.Forms.DataGridViewButtonColumn();
            this.entryContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainData)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.mainData);
            this.splitContainer.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Size = new System.Drawing.Size(726, 551);
            this.splitContainer.SplitterDistance = 285;
            this.splitContainer.SplitterWidth = 3;
            this.splitContainer.TabIndex = 1;
            // 
            // statusInfo
            // 
            this.statusInfo.Location = new System.Drawing.Point(0, 529);
            this.statusInfo.Name = "statusInfo";
            this.statusInfo.Size = new System.Drawing.Size(726, 22);
            this.statusInfo.TabIndex = 2;
            this.statusInfo.Text = "statusStrip1";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Content";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            this.mainData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainData.GridColor = System.Drawing.SystemColors.ControlLight;
            this.mainData.Location = new System.Drawing.Point(0, 0);
            this.mainData.Name = "mainData";
            this.mainData.RowHeadersVisible = false;
            this.mainData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mainData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mainData.Size = new System.Drawing.Size(726, 285);
            this.mainData.TabIndex = 2;
            this.mainData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mainData_CellClick);
            // 
            // entryName
            // 
            this.entryName.HeaderText = "Name";
            this.entryName.Name = "entryName";
            // 
            // entryUse
            // 
            this.entryUse.HeaderText = "";
            this.entryUse.Name = "entryUse";
            this.entryUse.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.entryUse.Text = "Copy";
            this.entryUse.Width = 50;
            // 
            // entryContent
            // 
            this.entryContent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.entryContent.HeaderText = "Content";
            this.entryContent.Name = "entryContent";
            this.entryContent.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.entryContent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FormCB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 551);
            this.Controls.Add(this.statusInfo);
            this.Controls.Add(this.splitContainer);
            this.Name = "FormCB";
            this.ShowIcon = false;
            this.Text = "cber";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.StatusStrip statusInfo;
        private System.Windows.Forms.DataGridView mainData;
        private System.Windows.Forms.DataGridViewTextBoxColumn entryName;
        private System.Windows.Forms.DataGridViewButtonColumn entryUse;
        private System.Windows.Forms.DataGridViewTextBoxColumn entryContent;
    }
}

