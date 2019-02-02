namespace Clipboarder
{
    partial class FormSearch
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
            this.button1 = new System.Windows.Forms.Button();
            this.listUrls = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textUrlSearch = new System.Windows.Forms.TextBox();
            this.tab = new System.Windows.Forms.TabControl();
            this.tabPageTime = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimeEndPicker = new System.Windows.Forms.DateTimePicker();
            this.dateTimeStartPicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPageURL = new System.Windows.Forms.TabPage();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.tabPageName = new System.Windows.Forms.TabPage();
            this.textSearchName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tab.SuspendLayout();
            this.tabPageTime.SuspendLayout();
            this.tabPageURL.SuspendLayout();
            this.tabPageName.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(452, 288);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 37);
            this.button1.TabIndex = 5;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listUrls
            // 
            this.listUrls.FormattingEnabled = true;
            this.listUrls.ItemHeight = 15;
            this.listUrls.Location = new System.Drawing.Point(6, 37);
            this.listUrls.Name = "listUrls";
            this.listUrls.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listUrls.Size = new System.Drawing.Size(548, 199);
            this.listUrls.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Search URLs:";
            // 
            // textUrlSearch
            // 
            this.textUrlSearch.Location = new System.Drawing.Point(126, 6);
            this.textUrlSearch.Name = "textUrlSearch";
            this.textUrlSearch.Size = new System.Drawing.Size(427, 25);
            this.textUrlSearch.TabIndex = 8;
            this.textUrlSearch.TextChanged += new System.EventHandler(this.textQuery_TextChanged);
            // 
            // tab
            // 
            this.tab.Controls.Add(this.tabPageName);
            this.tab.Controls.Add(this.tabPageTime);
            this.tab.Controls.Add(this.tabPageURL);
            this.tab.Location = new System.Drawing.Point(12, 12);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(569, 274);
            this.tab.TabIndex = 9;
            this.tab.SelectedIndexChanged += new System.EventHandler(this.tab_SelectedIndexChanged);
            // 
            // tabPageTime
            // 
            this.tabPageTime.Controls.Add(this.button5);
            this.tabPageTime.Controls.Add(this.button4);
            this.tabPageTime.Controls.Add(this.button3);
            this.tabPageTime.Controls.Add(this.label3);
            this.tabPageTime.Controls.Add(this.dateTimeEndPicker);
            this.tabPageTime.Controls.Add(this.dateTimeStartPicker);
            this.tabPageTime.Controls.Add(this.label2);
            this.tabPageTime.Location = new System.Drawing.Point(4, 25);
            this.tabPageTime.Name = "tabPageTime";
            this.tabPageTime.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTime.Size = new System.Drawing.Size(561, 245);
            this.tabPageTime.TabIndex = 1;
            this.tabPageTime.Text = "Timespan";
            this.tabPageTime.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "End:";
            // 
            // dateTimeEndPicker
            // 
            this.dateTimeEndPicker.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dateTimeEndPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeEndPicker.Location = new System.Drawing.Point(105, 37);
            this.dateTimeEndPicker.Name = "dateTimeEndPicker";
            this.dateTimeEndPicker.Size = new System.Drawing.Size(450, 25);
            this.dateTimeEndPicker.TabIndex = 2;
            // 
            // dateTimeStartPicker
            // 
            this.dateTimeStartPicker.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dateTimeStartPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeStartPicker.Location = new System.Drawing.Point(105, 6);
            this.dateTimeStartPicker.Name = "dateTimeStartPicker";
            this.dateTimeStartPicker.Size = new System.Drawing.Size(450, 25);
            this.dateTimeStartPicker.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Start:";
            // 
            // tabPageURL
            // 
            this.tabPageURL.Controls.Add(this.label1);
            this.tabPageURL.Controls.Add(this.textUrlSearch);
            this.tabPageURL.Controls.Add(this.listUrls);
            this.tabPageURL.Location = new System.Drawing.Point(4, 25);
            this.tabPageURL.Name = "tabPageURL";
            this.tabPageURL.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageURL.Size = new System.Drawing.Size(561, 245);
            this.tabPageURL.TabIndex = 0;
            this.tabPageURL.Text = "URLs";
            this.tabPageURL.UseVisualStyleBackColor = true;
            // 
            // buttonSearch
            // 
            this.buttonSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonSearch.Location = new System.Drawing.Point(321, 288);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(125, 37);
            this.buttonSearch.TabIndex = 10;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button3.Location = new System.Drawing.Point(9, 79);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(125, 37);
            this.button3.TabIndex = 11;
            this.button3.Text = "Last 24h";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button4.Location = new System.Drawing.Point(140, 79);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(125, 37);
            this.button4.TabIndex = 12;
            this.button4.Text = "Today";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button5.Location = new System.Drawing.Point(430, 79);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(125, 37);
            this.button5.TabIndex = 13;
            this.button5.Text = "Latest Entry";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // tabPageName
            // 
            this.tabPageName.Controls.Add(this.label4);
            this.tabPageName.Controls.Add(this.textSearchName);
            this.tabPageName.Location = new System.Drawing.Point(4, 25);
            this.tabPageName.Name = "tabPageName";
            this.tabPageName.Size = new System.Drawing.Size(561, 245);
            this.tabPageName.TabIndex = 2;
            this.tabPageName.Text = "Name";
            this.tabPageName.UseVisualStyleBackColor = true;
            // 
            // textSearchName
            // 
            this.textSearchName.Location = new System.Drawing.Point(3, 42);
            this.textSearchName.Name = "textSearchName";
            this.textSearchName.Size = new System.Drawing.Size(553, 25);
            this.textSearchName.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "Query:";
            // 
            // FormSearch
            // 
            this.AcceptButton = this.buttonSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 334);
            this.ControlBox = false;
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.tab);
            this.Controls.Add(this.button1);
            this.Name = "FormSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search";
            this.Load += new System.EventHandler(this.FormUrls_Load);
            this.tab.ResumeLayout(false);
            this.tabPageTime.ResumeLayout(false);
            this.tabPageTime.PerformLayout();
            this.tabPageURL.ResumeLayout(false);
            this.tabPageURL.PerformLayout();
            this.tabPageName.ResumeLayout(false);
            this.tabPageName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listUrls;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textUrlSearch;
        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage tabPageURL;
        private System.Windows.Forms.TabPage tabPageTime;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimeStartPicker;
        private System.Windows.Forms.DateTimePicker dateTimeEndPicker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TabPage tabPageName;
        private System.Windows.Forms.TextBox textSearchName;
        private System.Windows.Forms.Label label4;
    }
}