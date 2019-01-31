using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clipboarder
{
    public partial class FormCB : Form
    {
        private void RefreshDataMainView()
        {
            mainData.Rows.Clear();
            panelNav.Controls.Clear();
            ClearPanel2();

            int epp = Properties.Settings.Default.EntriesPerPage;
            int currentPage = (mainData.Tag as Page).Current;
            int totalEntries = mDB.TotalEntries();
            int pages = (int)Math.Ceiling((double)totalEntries / (double)epp);
            if (pages == 0) return;
            if (currentPage > pages)
            {
                currentPage = pages;
                (mainData.Tag as Page).Current = currentPage;
            }
            foreach (Database.Entry e in mDB.Paging(null, null, (currentPage - 1) * epp, epp))
            {
                int index = mainData.Rows.Add();
                mainData.Rows[index].Tag = e;
                mainData.Rows[index].Cells["entryName"].Value = e.Name;
                mainData.Rows[index].Cells["entryUse"].Value = "C";

                if (e.Content is string)
                {
                    var cell = new TextTitleCell();
                    mainData.Rows[index].Cells["entryContent"] = cell;
                    cell.ReadOnly = true;
                    cell.Title = new Title { No = e.Id, Hits = e.Hits, Time = e.Time, Url = e.SourceUrl };
                    var text = e.Content.ToString();
                    if (e.Type == Database.ContentType.HTML)
                    {
                        if (!string.IsNullOrWhiteSpace(e.Html))
                        {
                            text = e.Html;
                        }
                        else
                        {
                            text = Helper.ExtractHTMLFromClipboard(text);
                            text = Helper.ExtractTextFromHTML(text);
                        }
                    }
                    if (text.Length > 1024)
                        text = text.Substring(0, 1024) + "...";
                    cell.Value = text;
                }
                else
                {
                    var cell = new ImageTitleCell();
                    mainData.Rows[index].Cells["entryContent"] = cell;
                    Image img = (Image)e.Content;
                    cell.Title = new Title { No = e.Id, Hits = e.Hits, Time = e.Time, Size = img.Size, Url = e.SourceUrl };
                    cell.Value = e.Content;
                    if (img.Size.Width > mainData.RowTemplate.MinimumHeight || img.Size.Height > mainData.RowTemplate.MinimumHeight)
                        mainData.Rows[index].Height = mainData.RowTemplate.MinimumHeight * 2;
                }
            }

            var size = TextRenderer.MeasureText("A", panelNav.Font, new Size(0, 0));
            var buttonSize = new Size(size.Width * 3, size.Height * 2);
            panelNav.Height = size.Height * 2 + 10;
            Button btnFirst = new Button();
            btnFirst.Size = buttonSize;
            btnFirst.Location = new Point(5, 5);
            btnFirst.Text = "<<";
            btnFirst.Tag = 1;
            btnFirst.Click += NavBtn_Click;
            panelNav.Controls.Add(btnFirst); ;
            int left = 5 + buttonSize.Width + 5;
            var startEnd = Helper.SlidingWindow(pages, 5, currentPage);
            for (int i = startEnd.Item1; i <= startEnd.Item2; i++)
            {
                if (i < 1 || i > pages) continue;
                Button btn = new Button();
                btn.Size = buttonSize;
                btn.Location = new Point(left, 5);
                btn.Text = i.ToString();
                btn.Tag = i;
                btn.Click += NavBtn_Click;
                btn.Enabled = i != currentPage;
                panelNav.Controls.Add(btn);
                left += 5 + buttonSize.Width;
            }
            Button btnLast = new Button();
            btnLast.Size = buttonSize;
            btnLast.Location = new Point(left, 5);
            btnLast.Text = ">>";
            btnLast.Tag = pages;
            btnLast.Click += NavBtn_Click;
            panelNav.Controls.Add(btnLast);
            panelNav.Visible = true;

            statusTotalEntries.Text = string.Format(Properties.Resources.statusEntriesPages, pages, totalEntries);
        }
    }
}
