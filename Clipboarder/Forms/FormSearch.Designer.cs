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
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // listUrls
            // 
            this.tableUrls.SetColumnSpan(this.listUrls, 2);
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
            this.tab.Controls.Add(this.tabPageName);
            this.tab.Controls.Add(this.tabPageTime);
            this.tab.Controls.Add(this.tabPageURL);
            resources.ApplyResources(this.tab, "tab");
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.SelectedIndexChanged += new System.EventHandler(this.tab_SelectedIndexChanged);
            // 
            // tabPageName
            // 
            this.tabPageName.Controls.Add(this.tableMainSearch);
            resources.ApplyResources(this.tabPageName, "tabPageName");
            this.tabPageName.Name = "tabPageName";
            this.tabPageName.UseVisualStyleBackColor = true;
            // 
            // tableMainSearch
            // 
            resources.ApplyResources(this.tableMainSearch, "tableMainSearch");
            this.tableMainSearch.Controls.Add(this.label4, 0, 0);
            this.tableMainSearch.Controls.Add(this.textSearchName, 0, 1);
            this.tableMainSearch.Name = "tableMainSearch";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textSearchName
            // 
            resources.ApplyResources(this.textSearchName, "textSearchName");
            this.textSearchName.Name = "textSearchName";
            // 
            // tabPageTime
            // 
            this.tabPageTime.Controls.Add(this.tableTimespan);
            resources.ApplyResources(this.tabPageTime, "tabPageTime");
            this.tabPageTime.Name = "tabPageTime";
            this.tabPageTime.UseVisualStyleBackColor = true;
            // 
            // tableTimespan
            // 
            resources.ApplyResources(this.tableTimespan, "tableTimespan");
            this.tableTimespan.Controls.Add(this.label2, 0, 0);
            this.tableTimespan.Controls.Add(this.label3, 0, 2);
            this.tableTimespan.Controls.Add(this.dateTimeStartPicker, 1, 0);
            this.tableTimespan.Controls.Add(this.dateTimeEndPicker, 1, 2);
            this.tableTimespan.Controls.Add(this.tableLayoutPanel1, 1, 1);
            this.tableTimespan.Controls.Add(this.tableLayoutPanel2, 1, 3);
            this.tableTimespan.Name = "tableTimespan";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // dateTimeStartPicker
            // 
            resources.ApplyResources(this.dateTimeStartPicker, "dateTimeStartPicker");
            this.dateTimeStartPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeStartPicker.Name = "dateTimeStartPicker";
            // 
            // dateTimeEndPicker
            // 
            resources.ApplyResources(this.dateTimeEndPicker, "dateTimeEndPicker");
            this.dateTimeEndPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeEndPicker.Name = "dateTimeEndPicker";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.button3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button4, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.button5, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // button5
            // 
            resources.ApplyResources(this.button5, "button5");
            this.button5.Name = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // tabPageURL
            // 
            this.tabPageURL.Controls.Add(this.tableUrls);
            resources.ApplyResources(this.tabPageURL, "tabPageURL");
            this.tabPageURL.Name = "tabPageURL";
            this.tabPageURL.UseVisualStyleBackColor = true;
            // 
            // tableUrls
            // 
            resources.ApplyResources(this.tableUrls, "tableUrls");
            this.tableUrls.Controls.Add(this.listUrls, 0, 1);
            this.tableUrls.Controls.Add(this.textUrlSearch, 1, 0);
            this.tableUrls.Controls.Add(this.label1, 0, 0);
            this.tableUrls.Name = "tableUrls";
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