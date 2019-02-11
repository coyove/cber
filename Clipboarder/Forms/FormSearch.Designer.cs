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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.listUrls = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textUrlSearch = new System.Windows.Forms.TextBox();
            this.tab = new System.Windows.Forms.TabControl();
            this.tabPageName = new System.Windows.Forms.TabPage();
            this.tableMainSearch = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.textSearchName = new System.Windows.Forms.TextBox();
            this.tabPageTime = new System.Windows.Forms.TabPage();
            this.tableTimespan = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimeStartPicker = new System.Windows.Forms.DateTimePicker();
            this.dateTimeEndPicker = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button5 = new System.Windows.Forms.Button();
            this.tabPageURL = new System.Windows.Forms.TabPage();
            this.tableUrls = new System.Windows.Forms.TableLayoutPanel();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.checkSearchInFavs = new System.Windows.Forms.CheckBox();
            this.tableMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableButtons = new System.Windows.Forms.TableLayoutPanel();
            this.tab.SuspendLayout();
            this.tabPageName.SuspendLayout();
            this.tableMainSearch.SuspendLayout();
            this.tabPageTime.SuspendLayout();
            this.tableTimespan.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPageURL.SuspendLayout();
            this.tableUrls.SuspendLayout();
            this.tableMain.SuspendLayout();
            this.tableButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonCancel.Location = new System.Drawing.Point(382, 3);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(94, 32);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // listUrls
            // 
            this.tableUrls.SetColumnSpan(this.listUrls, 2);
            this.listUrls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listUrls.FormattingEnabled = true;
            this.listUrls.Location = new System.Drawing.Point(2, 29);
            this.listUrls.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.listUrls.Name = "listUrls";
            this.listUrls.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listUrls.Size = new System.Drawing.Size(464, 245);
            this.listUrls.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Search URLs:";
            // 
            // textUrlSearch
            // 
            this.textUrlSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textUrlSearch.Location = new System.Drawing.Point(145, 3);
            this.textUrlSearch.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textUrlSearch.Name = "textUrlSearch";
            this.textUrlSearch.Size = new System.Drawing.Size(321, 20);
            this.textUrlSearch.TabIndex = 8;
            this.textUrlSearch.TextChanged += new System.EventHandler(this.textQuery_TextChanged);
            // 
            // tab
            // 
            this.tab.Controls.Add(this.tabPageName);
            this.tab.Controls.Add(this.tabPageTime);
            this.tab.Controls.Add(this.tabPageURL);
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.Location = new System.Drawing.Point(2, 3);
            this.tab.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(480, 309);
            this.tab.TabIndex = 9;
            this.tab.SelectedIndexChanged += new System.EventHandler(this.tab_SelectedIndexChanged);
            // 
            // tabPageName
            // 
            this.tabPageName.Controls.Add(this.tableMainSearch);
            this.tabPageName.Location = new System.Drawing.Point(4, 22);
            this.tabPageName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPageName.Name = "tabPageName";
            this.tabPageName.Size = new System.Drawing.Size(472, 283);
            this.tabPageName.TabIndex = 2;
            this.tabPageName.Text = "Content";
            this.tabPageName.UseVisualStyleBackColor = true;
            // 
            // tableMainSearch
            // 
            this.tableMainSearch.ColumnCount = 1;
            this.tableMainSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableMainSearch.Controls.Add(this.label4, 0, 0);
            this.tableMainSearch.Controls.Add(this.textSearchName, 0, 1);
            this.tableMainSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableMainSearch.Location = new System.Drawing.Point(0, 0);
            this.tableMainSearch.Name = "tableMainSearch";
            this.tableMainSearch.RowCount = 2;
            this.tableMainSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableMainSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableMainSearch.Size = new System.Drawing.Size(472, 283);
            this.tableMainSearch.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 64);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(223, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Query: (搜索中文字符功能不可靠，且很慢)";
            // 
            // textSearchName
            // 
            this.textSearchName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textSearchName.Location = new System.Drawing.Point(2, 144);
            this.textSearchName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textSearchName.Name = "textSearchName";
            this.textSearchName.Size = new System.Drawing.Size(468, 20);
            this.textSearchName.TabIndex = 0;
            // 
            // tabPageTime
            // 
            this.tabPageTime.Controls.Add(this.tableTimespan);
            this.tabPageTime.Location = new System.Drawing.Point(4, 22);
            this.tabPageTime.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPageTime.Name = "tabPageTime";
            this.tabPageTime.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPageTime.Size = new System.Drawing.Size(472, 283);
            this.tabPageTime.TabIndex = 1;
            this.tabPageTime.Text = "Timespan";
            this.tabPageTime.UseVisualStyleBackColor = true;
            // 
            // tableTimespan
            // 
            this.tableTimespan.ColumnCount = 2;
            this.tableTimespan.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableTimespan.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableTimespan.Controls.Add(this.label2, 0, 0);
            this.tableTimespan.Controls.Add(this.label3, 0, 2);
            this.tableTimespan.Controls.Add(this.dateTimeStartPicker, 1, 0);
            this.tableTimespan.Controls.Add(this.dateTimeEndPicker, 1, 2);
            this.tableTimespan.Controls.Add(this.tableLayoutPanel1, 1, 1);
            this.tableTimespan.Controls.Add(this.tableLayoutPanel2, 1, 3);
            this.tableTimespan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableTimespan.Location = new System.Drawing.Point(2, 3);
            this.tableTimespan.Name = "tableTimespan";
            this.tableTimespan.RowCount = 4;
            this.tableTimespan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableTimespan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableTimespan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableTimespan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableTimespan.Size = new System.Drawing.Size(468, 277);
            this.tableTimespan.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Start:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 166);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "End:";
            // 
            // dateTimeStartPicker
            // 
            this.dateTimeStartPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimeStartPicker.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dateTimeStartPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeStartPicker.Location = new System.Drawing.Point(38, 24);
            this.dateTimeStartPicker.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dateTimeStartPicker.Name = "dateTimeStartPicker";
            this.dateTimeStartPicker.Size = new System.Drawing.Size(428, 20);
            this.dateTimeStartPicker.TabIndex = 1;
            // 
            // dateTimeEndPicker
            // 
            this.dateTimeEndPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimeEndPicker.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dateTimeEndPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeEndPicker.Location = new System.Drawing.Point(38, 162);
            this.dateTimeEndPicker.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dateTimeEndPicker.Name = "dateTimeEndPicker";
            this.dateTimeEndPicker.Size = new System.Drawing.Size(428, 20);
            this.dateTimeEndPicker.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.button3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button4, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(39, 72);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(426, 63);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // button3
            // 
            this.button3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button3.Location = new System.Drawing.Point(2, 3);
            this.button3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 32);
            this.button3.TabIndex = 11;
            this.button3.Text = "Last 24h";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button4.Location = new System.Drawing.Point(215, 3);
            this.button4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(94, 32);
            this.button4.TabIndex = 12;
            this.button4.Text = "Today";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.button5, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(39, 210);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(426, 64);
            this.tableLayoutPanel2.TabIndex = 14;
            // 
            // button5
            // 
            this.button5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button5.Location = new System.Drawing.Point(2, 3);
            this.button5.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(94, 32);
            this.button5.TabIndex = 13;
            this.button5.Text = "Latest Entry";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // tabPageURL
            // 
            this.tabPageURL.Controls.Add(this.tableUrls);
            this.tabPageURL.Location = new System.Drawing.Point(4, 22);
            this.tabPageURL.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPageURL.Name = "tabPageURL";
            this.tabPageURL.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPageURL.Size = new System.Drawing.Size(472, 283);
            this.tabPageURL.TabIndex = 0;
            this.tabPageURL.Text = "URLs";
            this.tabPageURL.UseVisualStyleBackColor = true;
            // 
            // tableUrls
            // 
            this.tableUrls.ColumnCount = 2;
            this.tableUrls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableUrls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableUrls.Controls.Add(this.listUrls, 0, 1);
            this.tableUrls.Controls.Add(this.textUrlSearch, 1, 0);
            this.tableUrls.Controls.Add(this.label1, 0, 0);
            this.tableUrls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableUrls.Location = new System.Drawing.Point(2, 3);
            this.tableUrls.Name = "tableUrls";
            this.tableUrls.RowCount = 2;
            this.tableUrls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableUrls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableUrls.Size = new System.Drawing.Size(468, 277);
            this.tableUrls.TabIndex = 9;
            // 
            // buttonSearch
            // 
            this.buttonSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonSearch.Location = new System.Drawing.Point(284, 3);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(94, 32);
            this.buttonSearch.TabIndex = 10;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkSearchInFavs
            // 
            this.checkSearchInFavs.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkSearchInFavs.AutoSize = true;
            this.checkSearchInFavs.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkSearchInFavs.Location = new System.Drawing.Point(2, 11);
            this.checkSearchInFavs.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkSearchInFavs.Name = "checkSearchInFavs";
            this.checkSearchInFavs.Size = new System.Drawing.Size(136, 17);
            this.checkSearchInFavs.TabIndex = 11;
            this.checkSearchInFavs.Text = "Search in favorites only";
            this.checkSearchInFavs.UseVisualStyleBackColor = true;
            // 
            // tableMain
            // 
            this.tableMain.ColumnCount = 1;
            this.tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableMain.Controls.Add(this.tableButtons, 0, 1);
            this.tableMain.Controls.Add(this.tab, 0, 0);
            this.tableMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableMain.Location = new System.Drawing.Point(0, 0);
            this.tableMain.Name = "tableMain";
            this.tableMain.RowCount = 2;
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableMain.Size = new System.Drawing.Size(484, 361);
            this.tableMain.TabIndex = 12;
            // 
            // tableButtons
            // 
            this.tableButtons.ColumnCount = 3;
            this.tableButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableButtons.Controls.Add(this.buttonCancel, 2, 0);
            this.tableButtons.Controls.Add(this.checkSearchInFavs, 0, 0);
            this.tableButtons.Controls.Add(this.buttonSearch, 1, 0);
            this.tableButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableButtons.Location = new System.Drawing.Point(3, 318);
            this.tableButtons.Name = "tableButtons";
            this.tableButtons.RowCount = 1;
            this.tableButtons.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableButtons.Size = new System.Drawing.Size(478, 40);
            this.tableButtons.TabIndex = 0;
            // 
            // FormSearch
            // 
            this.AcceptButton = this.buttonSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.tableMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "FormSearch";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search";
            this.Load += new System.EventHandler(this.FormUrls_Load);
            this.tab.ResumeLayout(false);
            this.tabPageName.ResumeLayout(false);
            this.tableMainSearch.ResumeLayout(false);
            this.tableMainSearch.PerformLayout();
            this.tabPageTime.ResumeLayout(false);
            this.tableTimespan.ResumeLayout(false);
            this.tableTimespan.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tabPageURL.ResumeLayout(false);
            this.tableUrls.ResumeLayout(false);
            this.tableUrls.PerformLayout();
            this.tableMain.ResumeLayout(false);
            this.tableButtons.ResumeLayout(false);
            this.tableButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonCancel;
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
        private System.Windows.Forms.CheckBox checkSearchInFavs;
        private System.Windows.Forms.TableLayoutPanel tableMain;
        private System.Windows.Forms.TableLayoutPanel tableButtons;
        private System.Windows.Forms.TableLayoutPanel tableUrls;
        private System.Windows.Forms.TableLayoutPanel tableTimespan;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableMainSearch;
    }
}