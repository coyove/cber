namespace Clipboarder
{
   partial class Form1
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
         this.mainData = new System.Windows.Forms.DataGridView();
         this.entryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.entryContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
         ((System.ComponentModel.ISupportInitialize)(this.mainData)).BeginInit();
         this.SuspendLayout();
         // 
         // mainData
         // 
         this.mainData.BackgroundColor = System.Drawing.SystemColors.Window;
         this.mainData.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.mainData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.mainData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.entryName,
            this.entryContent});
         this.mainData.GridColor = System.Drawing.SystemColors.ControlLight;
         this.mainData.Location = new System.Drawing.Point(51, 24);
         this.mainData.Name = "mainData";
         this.mainData.RowHeadersVisible = false;
         this.mainData.Size = new System.Drawing.Size(592, 431);
         this.mainData.TabIndex = 0;
         // 
         // entryName
         // 
         this.entryName.HeaderText = "Name";
         this.entryName.Name = "entryName";
         // 
         // entryContent
         // 
         this.entryContent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
         this.entryContent.HeaderText = "Content";
         this.entryContent.Name = "entryContent";
         this.entryContent.Resizable = System.Windows.Forms.DataGridViewTriState.True;
         this.entryContent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(805, 505);
         this.Controls.Add(this.mainData);
         this.Name = "Form1";
         this.Text = "Form1";
         this.Activated += new System.EventHandler(this.Form1_Activated);
         this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
         this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
         this.Load += new System.EventHandler(this.Form1_Load);
         ((System.ComponentModel.ISupportInitialize)(this.mainData)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.DataGridView mainData;
      private System.Windows.Forms.DataGridViewTextBoxColumn entryName;
      private System.Windows.Forms.DataGridViewTextBoxColumn entryContent;
   }
}

