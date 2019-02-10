using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Clipboarder
{
    public partial class FormSearch : Form
    {
        private List<string> mHosts = new List<string>();

        public string WhereClause { get; private set; }

        public bool BruteSearch { get; private set; }

        public bool SearchAndDelete = false;

        static Regex mReChinese = new Regex("[\u4e00-\u9fa5]");

        public FormSearch()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormUrls_Load(object sender, EventArgs e)
        {
            WhereClause = "";
            if (SearchAndDelete) this.Text = Properties.Resources.SearchAndDeleteTitle;
        }

        public void SwitchTab(string name)
        {
            switch (name)
            {
                case "url":
                    tab.SelectTab(tab.TabPages.IndexOf(tabPageURL));
                    break;
                case "time":
                    tab.SelectTab(tab.TabPages.IndexOf(tabPageTime));
                    break;
                case "name":
                    tab.SelectTab(tab.TabPages.IndexOf(tabPageName));
                    break;
            }

            tab_SelectedIndexChanged(null, null);
        }

        private void LoadUrlHosts()
        {
            listUrls.Items.Clear();
            HashSet<string> hosts = new HashSet<string>();
            foreach (string url in FormCB.mDB.Urls())
            {
                string host = Helper.GetHostFromUri(url);
                if (string.IsNullOrWhiteSpace(host)) continue;
                if (hosts.Contains(host)) continue;
                hosts.Add(host);
                mHosts.Add(host);
                listUrls.Items.Add(host);
            }
        }

        private void LoadTimeRange()
        {
            var range = FormCB.mDB.TimeRange();
            dateTimeStartPicker.Value = range.Item1;
            dateTimeEndPicker.Tag = range.Item2;
            dateTimeEndPicker.Value = DateTime.Now;
        }

        private void textQuery_TextChanged(object sender, EventArgs e)
        {
            listUrls.Items.Clear();
            foreach (string host in mHosts)
            {
                if (textUrlSearch.Text == "" || host.Contains(textUrlSearch.Text))
                    listUrls.Items.Add(host);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SearchAndDelete)
            {
                if (MessageBox.Show("All search results will be deleted directly without any further confirmations, are you sure?",
                    Application.ProductName, MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }

            StringBuilder res = new StringBuilder();
            if (tab.SelectedTab == tabPageURL)
            {
                    string urlWithoutScheme = null;
                    try {
                        var u = new Uri(textUrlSearch.Text);
                        urlWithoutScheme = u.Host + u.PathAndQuery;
                    }
                    catch (Exception) { }

                if (urlWithoutScheme == null && listUrls.SelectedIndices.Count == 0)
                {
                    Close();
                    return;
                }

                res.Append("AND (1 = 0");
                if (urlWithoutScheme != null)
                {
                    urlWithoutScheme = urlWithoutScheme.Replace("'", "''");
                    res.AppendFormat(@" OR source_url LIKE 'http://{0}%' OR source_url LIKE 'https://{0}%'", urlWithoutScheme);
                }

                foreach (int index in listUrls.SelectedIndices)
                {
                    res.AppendFormat(@" OR source_url LIKE 'http://{0}%' OR source_url LIKE 'https://{0}%'", listUrls.Items[index].ToString());
                }

                res.Append(")");
            }
            if (tab.SelectedTab == tabPageTime)
            {
                var u = new DateTime(1970, 1, 1);
                res.AppendFormat("AND ts >= {0} AND ts <= {1}",
                    (int)dateTimeStartPicker.Value.ToUniversalTime().Subtract(u).TotalSeconds,
                    (int)dateTimeEndPicker.Value.ToUniversalTime().Subtract(u).TotalSeconds
                    );
            }
            if (tab.SelectedTab == tabPageName)
            {
                if (string.IsNullOrWhiteSpace(textSearchName.Text))
                {
                    Close();
                    return;
                }

                if (mReChinese.IsMatch(textSearchName.Text))
                {
                    res.Append(textSearchName.Text);
                    BruteSearch = true;
                }
                else
                {
                    string name = textSearchName.Text.Replace("'", "''");
                    res.AppendFormat("AND text_content LIKE '%{0}%'", name);
                }
                //res.Append(textSearchName.Text);
            }
            if (!BruteSearch && checkSearchInFavs.Checked)
                res.Append(" AND favorited = 1");
            WhereClause = (res.ToString());
            Close();
        }

        private void tab_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tab.SelectedTab == tabPageURL)
                {
                    LoadUrlHosts();
                    textUrlSearch.Select();
                }
                if (tab.SelectedTab == tabPageName)
                {
                    textSearchName.Select();
                }
                if (tab.SelectedTab == tabPageTime)
                {
                    LoadTimeRange();
                    dateTimeEndPicker.Select();
                }
            }
            catch (Exception) { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dateTimeStartPicker.Value = dateTimeEndPicker.Value.Subtract(new TimeSpan(24, 0, 0));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DateTime end = dateTimeEndPicker.Value;
            dateTimeStartPicker.Value = new DateTime(
                end.Year, end.Month, end.Day, 0, 0, 0, DateTimeKind.Local);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dateTimeEndPicker.Value = (DateTime)dateTimeEndPicker.Tag;
        }

        private void textSearchName_TextChanged(object sender, EventArgs e)
        {
            labelSearchChinese.Visible = (mReChinese.IsMatch(textSearchName.Text));
        }
    }
}
