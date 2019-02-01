using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Clipboarder
{
    public partial class FormCB : Form
    {
        public class Page
        {
            public int Current;
        }

        [DllImport("user32.dll")]
        private static extern int SetClipboardViewer(int hWndNewViewer);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(System.Drawing.Point p);

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private Dictionary<UInt64, Action> mShortcutsMap = new Dictionary<ulong, Action>();

        private ConcurrentDictionary<Thread, DateTime> mPendingThreads = new ConcurrentDictionary<Thread, DateTime>();

        private void RegisterShortcut(string keys, Action a)
        {
            uint modifier = 0;
            int k = 0;
            foreach (string key in keys.Split(new char[] { '+' })) {
                switch (key.ToLower())
                {
                    case "alt":
                        modifier |= 1;
                        break;
                    case "ctrl":
                        modifier |= 2;
                        break;
                    case "shift":
                        modifier |= 4;
                        break;
                    case "win":
                        modifier |= 8;
                        break;
                    default:
                        try { k = (int)(Keys)Enum.Parse(typeof(Keys), key); } catch (Exception) { }
                        break;
                }
            }
            if (k == 0) return;
            int count = mShortcutsMap.Count;
            if (RegisterHotKey(Handle, count + 1, modifier, (uint)k))
            {
                UInt64 k64 = ((UInt64)modifier << 32) | (UInt64)(uint)k;
                mShortcutsMap[k64] = a;
            }
        }

        private bool Dot(Action a)
        {
            Thread t = new Thread(new ThreadStart(a));
            var now = mPendingThreads[t] = DateTime.Now;
            t.Start();
            bool res = t.Join(Properties.Settings.Default.DbOpTimeout);
            if (res) mPendingThreads.TryRemove(t, out now);
            return res;
        }

        private void UnregisterShortcuts()
        {
            for (int i = mShortcutsMap.Count; i > 0; i--)
            {
                UnregisterHotKey(this.Handle, i);
            }
        }

        private void LoadSettings()
        {
            showTextContents.Checked = showImageContents.Checked = showHTMLContents.Checked = false;
            listenTextContents.Checked = listenImageContents.Checked = listenHTMLContents.Checked = false;
            foreach (string show in 
                Properties.Settings.Default.ShowContents.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                (menuMain.Items.Find("show" + show + "Contents", true).First() as ToolStripMenuItem).Checked = true;
            }
            foreach (string listen in 
                Properties.Settings.Default.ListenContents.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                (menuMain.Items.Find("listen" + listen + "Contents", true).First() as ToolStripMenuItem).Checked = true;
            }
            hideAfterCopyToolStripMenuItem.Checked = Properties.Settings.Default.HideAfterCopy;
            stayOnTopToolStripMenuItem.Checked = Properties.Settings.Default.StayOnTop;
            stayOnTopToolStripMenuItem_Click(null, null);
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.StayOnTop = stayOnTopToolStripMenuItem.Checked;
            Properties.Settings.Default.HideAfterCopy = hideAfterCopyToolStripMenuItem.Checked;
            Properties.Settings.Default.ShowContents = "";
            Properties.Settings.Default.ListenContents = "";
            foreach (string show in new string[] { "Text", "Image", "HTML" })
            {
                if ((menuMain.Items.Find("show" + show + "Contents", true).First() as ToolStripMenuItem).Checked)
                    Properties.Settings.Default.ShowContents += show + ",";
                if ((menuMain.Items.Find("listen" + show + "Contents", true).First() as ToolStripMenuItem).Checked)
                    Properties.Settings.Default.ListenContents += show + ",";
            }
            Properties.Settings.Default.Save();
        }

        private string CalcWhere()
        {
            StringBuilder where = new StringBuilder();
            if (!showTextContents.Checked)
                where.Append("AND type != " + (int)Database.ContentType.RawText);
            if (!showImageContents.Checked)
                where.Append("AND type != " + (int)Database.ContentType.Image);
            if (!showHTMLContents.Checked)
                where.Append("AND type != " + (int)Database.ContentType.HTML);
            return where.ToString();
        }

        private void RefreshDataMainView()
        {
            mainData.Rows.Clear();
            panelNav.Controls.Clear();
            ClearPanel2();

            string where = CalcWhere();
            int epp = Properties.Settings.Default.EntriesPerPage;
            int currentPage = (mainData.Tag as Page).Current;
            int totalEntries = mDB.TotalEntries(where);
            int pages = (int)Math.Ceiling((double)totalEntries / (double)epp);

            statusTotalEntries.Text = string.Format("{0}P / {1}E", pages, totalEntries);
            statusDbPath.Text = mDB.Path + 
                " (" + ((double) new System.IO.FileInfo(mDB.Path).Length / 1024 / 1024).ToString("0.00") + "MB)";

            if (pages == 0) return;
            if (currentPage > pages)
            {
                currentPage = pages;
                (mainData.Tag as Page).Current = currentPage;
            }

            foreach (Database.Entry e in mDB.Paging(where.ToString(), null, (currentPage - 1) * epp, epp))
            {
                int index = mainData.Rows.Add();
                mainData.Rows[index].Tag = e;
                mainData.Rows[index].Cells["entryName"].Value = e.Name;
                mainData.Rows[index].Cells["entryUse"].Value = "Copy";

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

        }
    }
}