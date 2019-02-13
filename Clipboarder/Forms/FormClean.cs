using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static Clipboarder.Database;

namespace Clipboarder
{
    public partial class FormClean : Form
    {
        public Database mDB;

        public FormClean()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormClean_Load(object sender, EventArgs e)
        {

        }

        private void buttonVacuum_Click(object sender, EventArgs e)
        {
            double before = (double)(new FileInfo(mDB.Path).Length) / 1024 / 1024;
            mDB.Vacuum();
            double after = (double)(new FileInfo(mDB.Path).Length) / 1024 / 1024;
            MessageBox.Show(string.Format("{0}MB -> {1}MB", before.ToString("0.00"), after.ToString("0.00")));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mDB.Delete(ContentType.Image);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mDB.Delete(ContentType.HTML);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mDB.Delete(ContentType.RawText);
        }

        private void buttonDeleteLargerThan_Click(object sender, EventArgs e)
        {
            mDB.DeleteLargerThan((int)numDeleteLargerThan.Value);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mDB.Delete(-1, true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mDB.Delete(-1);
        }

        private void buttonSearchDelete_Click(object sender, EventArgs e)
        {
            FormSearch frm = new FormSearch();
            frm.SwitchTab("time");
            frm.SearchAndDelete = true;
            frm.ShowDialog();
            if (string.IsNullOrWhiteSpace(frm.WhereClause)) return;
            mDB.Delete(frm.WhereClause);
        }
    }
}
