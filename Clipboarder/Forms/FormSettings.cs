using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clipboarder
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormShortcuts_Load(object sender, EventArgs e)
        {
            textGSShow.Text = Properties.Settings.Default.GSShow;
            buttonBrowseIE.Tag = Properties.Settings.Default.ExternalImageEditor;
            buttonBrowseIE.Text = System.IO.Path.GetFileName(buttonBrowseIE.Tag.ToString());
            textEntriesPerPage.Value = Properties.Settings.Default.EntriesPerPage;

            int xpurge = Properties.Settings.Default.XPurge;
            var xpurgeButton = (groupXPurge.Controls.Find("autoPurge" + xpurge, false).First() as RadioButton);
            xpurgeButton.Checked = true;
            textAutoPurgeX.Visible = xpurge > 0;
            textAutoPurgeX.Top = xpurgeButton.Top;
            textAutoPurgeX.Value = Properties.Settings.Default.XPurgeValue;

            checkRule.Checked = Properties.Settings.Default.RuleEnabled;
            textRule.Text = Properties.Settings.Default.RuleScript;
            textRule.SetHighlighting("C#");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textGSShow.Text != "")
                Properties.Settings.Default.GSShow = textGSShow.Text;

            Properties.Settings.Default.ExternalImageEditor = buttonBrowseIE.Tag.ToString();
            Properties.Settings.Default.EntriesPerPage = (int)textEntriesPerPage.Value;
            Properties.Settings.Default.XPurgeValue = (int)textAutoPurgeX.Value;
            for (int i = 0; i < 3; i++)
                if ((groupXPurge.Controls.Find("autoPurge" + i, false).First() as RadioButton).Checked)
                    Properties.Settings.Default.XPurge = i;

            Properties.Settings.Default.RuleScript = textRule.Text;
            Properties.Settings.Default.RuleEnabled = checkRule.Checked;

            Properties.Settings.Default.Save();
            Close();
        }

        private void buttonBrowseIE_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDb = new OpenFileDialog();
            openDb.Filter = "*.exe|*.exe";

            if (openDb.ShowDialog() == DialogResult.OK)
            {
                buttonBrowseIE.Tag = openDb.FileName;
                buttonBrowseIE.Text = System.IO.Path.GetFileName(openDb.FileName);
            }
        }

        private void textGSShow_Paint(object sender, PaintEventArgs e)
        {

        }

        private void autoPurge0_CheckedChanged(object sender, EventArgs e)
        {
            int xpurge = int.Parse((sender as Control).Tag.ToString());
            textAutoPurgeX.Visible = xpurge > 0;
            textAutoPurgeX.Top = (sender as Control).Top;
        }
    }
}
