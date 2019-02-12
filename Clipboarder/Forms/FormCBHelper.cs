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

        private void UnregisterShortcuts()
        {
            for (int i = mShortcutsMap.Count; i > 0; i--)
            {
                UnregisterHotKey(this.Handle, i);
            }
        }

        private void LoadSettings()
        {
            listenTextContents.Checked = listenImageContents.Checked = listenHTMLContents.Checked = false;
            foreach (string show in 
                Properties.Settings.Default.ShowContents.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var btn = mViewFilter.FirstOrDefault(v => v.Tag.ToString() == "show" + show);
                if (btn != null) btn.Pushed = true;
            }

            foreach (string listen in 
                Properties.Settings.Default.ListenContents.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                (cberMenu.DropDownItems.Find("listen" + listen + "Contents", true).First() as ToolStripMenuItem).Checked = true;
            }
            hideAfterCopyToolStripMenuItem.Checked = Properties.Settings.Default.HideAfterCopy;
            stayOnTopToolStripMenuItem.Checked = Properties.Settings.Default.StayOnTop;
            stayOnTopToolStripMenuItem_Click(null, null);
            this.Location = Properties.Settings.Default.FormLocation;
            this.Size = Properties.Settings.Default.FormSize;
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.StayOnTop = stayOnTopToolStripMenuItem.Checked;
            Properties.Settings.Default.HideAfterCopy = hideAfterCopyToolStripMenuItem.Checked;
            Properties.Settings.Default.ShowContents = "";
            Properties.Settings.Default.ListenContents = "";
            foreach (string show in new string[] { "Text", "Image", "HTML" })
            {
                if (mViewFilter.First(v => v.Tag.ToString() == "show" + show).Pushed)
                    Properties.Settings.Default.ShowContents += show + ",";
                if ((cberMenu.DropDownItems.Find("listen" + show + "Contents", true).First() as ToolStripMenuItem).Checked)
                    Properties.Settings.Default.ListenContents += show + ",";
            }
            Properties.Settings.Default.FormLocation = this.Location;
            Properties.Settings.Default.FormSize = this.Size;
            Properties.Settings.Default.Save();
        }

        private string CalcWhere()
        {
            StringBuilder where = new StringBuilder();
            if (!mViewFilter[0].Pushed)
                where.Append(" AND type != " + (int)Database.ContentType.RawText);
            if (!mViewFilter[2].Pushed)
                where.Append(" AND type != " + (int)Database.ContentType.Image);
            if (!mViewFilter[1].Pushed)
                where.Append(" AND type != " + (int)Database.ContentType.HTML);
            where.Append((mainData.Tag as Page).Where);
            return where.ToString();
        }

        private void RefreshDataMainView()
        {
            mainData.Clear();

            for (int i = mBarNav.Buttons.Count - 1; i >= 0; i--)
            {
                if (mBarNav.Buttons[i].Name.StartsWith("nav"))
                {
                    var btn = mBarNav.Buttons[i];
                    mBarNav.Buttons.RemoveAt(i);
                    btn.Dispose();
                }
            }

            string where = CalcWhere();
            int epp = Properties.Settings.Default.EntriesPerPage;
            int currentPage = (mainData.Tag as Page).Current;
            int totalEntries = mDB.TotalEntries(where);
            int pages = (int)Math.Ceiling((double)totalEntries / (double)epp);

            statusDbPath.Text = string.Format("{3} ({0}/{1}MB)",
                totalEntries, ((double)new System.IO.FileInfo(mDB.Path).Length / 1024 / 1024).ToString("0.00"), pages, mDB.Path);

            if (pages == 0) return;
            if (currentPage < 1) currentPage = 1;
            if (currentPage > pages) currentPage = pages;
            (mainData.Tag as Page).Current = currentPage;

            foreach (Database.Entry e in mDB.Paging(where.ToString(), null, (currentPage - 1) * epp, epp))
            {
                mainData.Add(e);
            }
            mainData.Refresh();

            var startEnd = Helper.SlidingWindow(pages, 5, currentPage);
            Action<int, string> addButton = (i, key) =>
            {
                ToolBarButton button = new ToolBarButton();
                button.Text = "[ " + i.ToString() + " ]";
                button.Pushed = i == currentPage;
                if (button.Pushed && key == "page") key = "curpage";
                button.Name = "nav" + i.ToString();
                button.Tag = i;
                button.ImageKey = key;
                mBarNav.Buttons.Add(button);
            };

            if (startEnd.Item1 != 1) addButton(1, "left");
            for (int i = startEnd.Item1; i <= startEnd.Item2; i++) addButton(i, "page");
            if (startEnd.Item2 != pages) addButton(pages, "right");
        }
    }
}