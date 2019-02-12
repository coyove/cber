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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSearch));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.listUrls = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textUrlSearch = new System.Windows.Forms.TextBox();
            this.tab = new System.Windows.Forms.TabControl();
            this.tabPageName = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.textSearchName = new System.Windows.Forms.TextBox();
            this.tabPageTime = new System.Windows.Forms.TabPage();
            this.button5 = new System.Windows.Forms.Button();
            this.dateTimeEndPicker = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimeStartPicker = new System.Windows.Forms.DateTimePicker();
            this.tabPageURL = new System.Windows.Forms.TabPage();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.checkSearchInFavs = new System.Windows.Forms.CheckBox();
            this.tableMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableButtons = new System.Windows.Forms.TableLayoutPanel();
            this.tab.SuspendLayout();
            this.tabPageName.SuspendLayout();
            this.tabPageTime.SuspendLayout();
            this.tabPageURL.SuspendLayout();
            this.tableMain.SuspendLayout();
            this.tableButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // listUrls
            // 
            resources.ApplyResources(this.listUrls, "listUrls");
            this.listUrls.FormattingEnabled = true;
            this.listUrls.Name = "listUrls";
            this.listUrls.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textUrlSearch
            // 
            resources.ApplyResources(this.textUrlSearch, "textUrlSearch");
            this.textUrlSearch.Name = "textUrlSearch";
            this.textUrlSearch.TextChanged += new System.EventHandler(this.textQuery_TextChanged);
            // 
            // tab
            // 
            resources.ApplyResources(this.tab, "tab");
            this.tab.Controls.Add(this.tabPageName);
            this.tab.Controls.Add(this.tabPageTime);
            this.tab.Controls.Add(this.tabPageURL);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.SelectedIndexChanged += new System.EventHandler(this.tab_SelectedIndexChanged);
            // 
            // tabPageName
            // 
            resources.ApplyResources(this.tabPageName, "tabPageName");
            this.tabPageName.Controls.Add(this.label5);
            this.tabPageName.Controls.Add(this.textSearchName);
            this.tabPageName.Name = "tabPageName";
            this.tabPageName.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textSearchName
            // 
            resources.ApplyResources(this.textSearchName, "textSearchName");
            this.textSearchName.Name = "textSearchName";
            // 
            // tabPageTime
            // 
            resources.ApplyResources(this.tabPageTime, "tabPageTime");
            this.tabPageTime.Controls.Add(this.button5);
            this.tabPageTime.Controls.Add(this.dateTimeEndPicker);
            this.tabPageTime.Controls.Add(this.label3);
            this.tabPageTime.Controls.Add(this.button4);
            this.tabPageTime.Controls.Add(this.button3);
            this.tabPageTime.Controls.Add(this.label2);
            this.tabPageTime.Controls.Add(this.dateTimeStartPicker);
            this.tabPageTime.Name = "tabPageTime";
            this.tabPageTime.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            resources.ApplyResources(this.button5, "button5");
            this.button5.Name = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // dateTimeEndPicker
            // 
            resources.ApplyResources(this.dateTimeEndPicker, "dateTimeEndPicker");
            this.dateTimeEndPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeEndPicker.Name = "dateTimeEndPicker";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // dateTimeStartPicker
            // 
            resources.ApplyResources(this.dateTimeStartPicker, "dateTimeStartPicker");
            this.dateTimeStartPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeStartPicker.Name = "dateTimeStartPicker";
            // 
            // tabPageURL
            // 
            resources.ApplyResources(this.tabPageURL, "tabPageURL");
            this.tabPageURL.Controls.Add(this.listUrls);
            this.tabPageURL.Controls.Add(this.textUrlSearch);
            this.tabPageURL.Controls.Add(this.label1);
            this.tabPageURL.Name = "tabPageURL";
            this.tabPageURL.UseVisualStyleBackColor = true;
            // 
            // buttonSearch
            // 
            resources.ApplyResources(this.buttonSearch, "buttonSearch");
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkSearchInFavs
            // 
            resources.ApplyResources(this.checkSearchInFavs, "checkSearchInFavs");
            this.checkSearchInFavs.Name = "checkSearchInFavs";
            this.checkSearchInFavs.UseVisualStyleBackColor = true;
            // 
            // tableMain
            // 
            resources.ApplyResources(this.tableMain, "tableMain");
            this.tableMain.Controls.Add(this.tableButtons, 0, 1);
            this.tableMain.Controls.Add(this.tab, 0, 0);
            this.tableMain.Name = "tableMain";
            // 
            // tableButtons
            // 
            resources.ApplyResources(this.tableButtons, "tableButtons");
            this.tableButtons.Controls.Add(this.buttonCancel, 2, 0);
            this.tableButtons.Controls.Add(this.checkSearchInFavs, 0, 0);
            this.tableButtons.Controls.Add(this.buttonSearch, 1, 0);
            this.tableButtons.Name = "tableButtons";
            // 
            // FormSearch
            // 
            this.AcceptButton = this.buttonSearch;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tableMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormSearch";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.FormUrls_Load);
            this.tab.ResumeLayout(false);
            this.tabPageName.ResumeLayout(false);
            this.tabPageName.PerformLayout();
            this.tabPageTime.ResumeLayout(false);
            this.tabPageTime.PerformLayout();
            this.tabPageURL.ResumeLayout(false);
            this.tabPageURL.PerformLayout();
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
        private System.Windows.Forms.CheckBox checkSearchInFavs;
        private System.Windows.Forms.TableLayoutPanel tableMain;
        private System.Windows.Forms.TableLayoutPanel tableButtons;
        private System.Windows.Forms.Label label5;
    }
}