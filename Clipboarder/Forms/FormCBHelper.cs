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
            public string Where = "";
            public bool BruteSearch = false;
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
        private static extern IntPtr GetParent(IntPtr hwnd);

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
            where.Append((mainData.Tag as Page).Where);
            return where.ToString();
        }

        private void RefreshDataMainView()
        {
            mainData.Clear();

            for (int i = toolStripNav.Items.Count - 1; i >= 0; i--)
                if ((toolStripNav.Items[i] as ToolStripButton)?.Name.StartsWith("nav") == true)
                    toolStripNav.Items.RemoveAt(i);

            string where = CalcWhere();
            int epp = Properties.Settings.Default.EntriesPerPage;
            int currentPage = (mainData.Tag as Page).Current;
            int totalEntries = mDB.TotalEntries(where, (mainData.Tag as Page).BruteSearch);
            int pages = (int)Math.Ceiling((double)totalEntries / (double)epp);

            buttonClearWhere.Visible = (mainData.Tag as Page).Where != "";
            statusDbPath.Text = string.Format("{0} ({2}/{1}MB)", mDB.Path,
                ((double)new System.IO.FileInfo(mDB.Path).Length / 1024 / 1024).ToString("0.00"), totalEntries);

            buttonLastPage.Text = string.Format(Properties.Resources.Pages, pages);
            if (pages == 0) return;
            if (currentPage < 1) currentPage = 1;
            if (currentPage > pages) currentPage = pages;
            (mainData.Tag as Page).Current = currentPage;

            foreach (Database.Entry e in mDB.Paging(where.ToString(), 
                null, 
                (currentPage - 1) * epp, 
                epp,
                (mainData.Tag as Page).BruteSearch))
            {
                mainData.Add(e);
            }
            mainData.Refresh();

            buttonFirstPage.Tag = 1;
            var startEnd = Helper.SlidingWindow(pages, 5, currentPage);
            for (int i = startEnd.Item1; i <= startEnd.Item2; i++)
            {
                if (i < 1 || i > pages) continue;
                ToolStripButton button = new ToolStripButton();
                button.Text = " " + i.ToString() + " ";
                button.Enabled = i != currentPage;
                button.Name = "nav" + i.ToString();
                button.Tag = i;
                button.Click += NavBtn_Click;
                toolStripNav.Items.Insert(toolStripNav.Items.IndexOf(buttonLastPage), button);
            }
            buttonLastPage.Tag = pages;
        }
    }
}