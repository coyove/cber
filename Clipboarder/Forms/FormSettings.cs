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
            textExImageEditor.Text = Properties.Settings.Default.ExternalImageEditor;
            textEntriesPerPage.Value = Properties.Settings.Default.EntriesPerPage;
            textDbOpTimeout.Value = Properties.Settings.Default.DbOpTimeout;
            (groupXPurge.Controls.Find("autoPurge" + Properties.Settings.Default.XPurge.ToString(), false).First() as RadioButton).Checked = true;
            textAutoPurgeX.Value = Properties.Settings.Default.XPurgeValue;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textGSShow.Text != "")
                Properties.Settings.Default.GSShow = textGSShow.Text;

            Properties.Settings.Default.ExternalImageEditor = textExImageEditor.Text;
            Properties.Settings.Default.EntriesPerPage = (int)textEntriesPerPage.Value;
            Properties.Settings.Default.DbOpTimeout = (int)textDbOpTimeout.Value;
            Properties.Settings.Default.XPurgeValue = (int)textAutoPurgeX.Value;
            for (int i = 0; i < 3; i++)
                if ((groupXPurge.Controls.Find("autoPurge" + i, false).First() as RadioButton).Checked)
                    Properties.Settings.Default.XPurge = i;

            Properties.Settings.Default.Save();
            Close();
        }

        private void buttonBrowseIE_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDb = new OpenFileDialog();
            openDb.Filter = "*.exe|*.exe";

            if (openDb.ShowDialog() == DialogResult.OK)
            {
                textExImageEditor.Text = openDb.FileName;
            }
        }

        private void textGSShow_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
