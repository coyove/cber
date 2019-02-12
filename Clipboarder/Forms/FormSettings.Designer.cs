namespace Clipboarder
{
    partial class FormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textEntriesPerPage = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textGSShow = new Clipboarder.Shortcut();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonBrowseIE = new System.Windows.Forms.Button();
            this.groupXPurge = new System.Windows.Forms.GroupBox();
            this.autoPurge0 = new System.Windows.Forms.RadioButton();
            this.textAutoPurgeX = new System.Windows.Forms.NumericUpDown();
            this.autoPurge2 = new System.Windows.Forms.RadioButton();
            this.autoPurge1 = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textRule = new ICSharpCode.TextEditor.TextEditorControl();
            this.checkRule = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.textEntriesPerPage)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupXPurge.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textAutoPurgeX)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textEntriesPerPage
            // 
            resources.ApplyResources(this.textEntriesPerPage, "textEntriesPerPage");
            this.textEntriesPerPage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.textEntriesPerPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.textEntriesPerPage.Name = "textEntriesPerPage";
            this.textEntriesPerPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.textGSShow);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // textGSShow
            // 
            resources.ApplyResources(this.textGSShow, "textGSShow");
            this.textGSShow.Name = "textGSShow";
            this.textGSShow.Paint += new System.Windows.Forms.PaintEventHandler(this.textGSShow_Paint);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.buttonBrowseIE);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textEntriesPerPage);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // buttonBrowseIE
            // 
            resources.ApplyResources(this.buttonBrowseIE, "buttonBrowseIE");
            this.buttonBrowseIE.Name = "buttonBrowseIE";
            this.buttonBrowseIE.UseVisualStyleBackColor = true;
            this.buttonBrowseIE.Click += new System.EventHandler(this.buttonBrowseIE_Click);
            // 
            // groupXPurge
            // 
            resources.ApplyResources(this.groupXPurge, "groupXPurge");
            this.groupXPurge.Controls.Add(this.autoPurge0);
            this.groupXPurge.Controls.Add(this.textAutoPurgeX);
            this.groupXPurge.Controls.Add(this.autoPurge2);
            this.groupXPurge.Controls.Add(this.autoPurge1);
            this.groupXPurge.Name = "groupXPurge";
            this.groupXPurge.TabStop = false;
            // 
            // autoPurge0
            // 
            resources.ApplyResources(this.autoPurge0, "autoPurge0");
            this.autoPurge0.Name = "autoPurge0";
            this.autoPurge0.TabStop = true;
            this.autoPurge0.Tag = "0";
            this.autoPurge0.UseVisualStyleBackColor = true;
            this.autoPurge0.CheckedChanged += new System.EventHandler(this.autoPurge0_CheckedChanged);
            // 
            // textAutoPurgeX
            // 
            resources.ApplyResources(this.textAutoPurgeX, "textAutoPurgeX");
            this.textAutoPurgeX.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.textAutoPurgeX.Name = "textAutoPurgeX";
            this.textAutoPurgeX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // autoPurge2
            // 
            resources.ApplyResources(this.autoPurge2, "autoPurge2");
            this.autoPurge2.Name = "autoPurge2";
            this.autoPurge2.TabStop = true;
            this.autoPurge2.Tag = "2";
            this.autoPurge2.UseVisualStyleBackColor = true;
            this.autoPurge2.CheckedChanged += new System.EventHandler(this.autoPurge0_CheckedChanged);
            // 
            // autoPurge1
            // 
            resources.ApplyResources(this.autoPurge1, "autoPurge1");
            this.autoPurge1.Name = "autoPurge1";
            this.autoPurge1.TabStop = true;
            this.autoPurge1.Tag = "1";
            this.autoPurge1.UseVisualStyleBackColor = true;
            this.autoPurge1.CheckedChanged += new System.EventHandler(this.autoPurge0_CheckedChanged);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Controls.Add(this.groupXPurge);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Controls.Add(this.textRule);
            this.tabPage2.Controls.Add(this.checkRule);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textRule
            // 
            resources.ApplyResources(this.textRule, "textRule");
            this.textRule.IsReadOnly = false;
            this.textRule.Name = "textRule";
            // 
            // checkRule
            // 
            resources.ApplyResources(this.checkRule, "checkRule");
            this.checkRule.Name = "checkRule";
            this.checkRule.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.button2, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.button1, 2, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // FormSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tableLayoutPanel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.FormShortcuts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEntriesPerPage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupXPurge.ResumeLayout(false);
            this.groupXPurge.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textAutoPurgeX)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown textEntriesPerPage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Shortcut textGSShow;
        private System.Windows.Forms.Button buttonBrowseIE;
        private System.Windows.Forms.GroupBox groupXPurge;
        private System.Windows.Forms.RadioButton autoPurge1;
        private System.Windows.Forms.NumericUpDown textAutoPurgeX;
        private System.Windows.Forms.RadioButton autoPurge2;
        private System.Windows.Forms.RadioButton autoPurge0;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox checkRule;
        private ICSharpCode.TextEditor.TextEditorControl textRule;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    }
}