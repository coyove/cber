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
                switch (show)
                {
                    case "Text": showTextContents.Checked = true; break;
                    case "Image":showImageContents.Checked = true; break;
                    case "HTML": showHTMLContents.Checked = true; break;
                }
            }

            foreach (string listen in 
                Properties.Settings.Default.ListenContents.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                switch (listen)
                {
                    case "Text": listenTextContents.Checked = true; break;
                    case "Image":listenImageContents.Checked = true; break;
                    case "HTML": listenHTMLContents.Checked = true; break;
                }
            }
            hideAfterCopyToolStripMenuItem.Checked = Properties.Settings.Default.HideAfterCopy;
            stayOnTopToolStripMenuItem.Checked = !Properties.Settings.Default.StayOnTop;
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
                if (showTextContents.Checked) Properties.Settings.Default.ShowContents += "Text,";
                if (showImageContents.Checked) Properties.Settings.Default.ShowContents += "Image,";
                if (showHTMLContents.Checked) Properties.Settings.Default.ShowContents += "HTML,";
                if (listenTextContents.Checked) Properties.Settings.Default.ListenContents += "Text,";
                if (listenImageContents.Checked) Properties.Settings.Default.ListenContents += "Image,";
                if (listenHTMLContents.Checked) Properties.Settings.Default.ListenContents += "HTML,";
            }
            Properties.Settings.Default.FormLocation = this.Location;
            Properties.Settings.Default.FormSize = this.Size;
            Properties.Settings.Default.Save();
        }

        private string CalcWhere()
        {
            StringBuilder where = new StringBuilder();
            if (!showTextContents.Checked)
                where.Append(" AND type != " + (int)Database.ContentType.RawText);
            if (!showImageContents.Checked)
                where.Append(" AND type != " + (int)Database.ContentType.Image);
            if (!showHTMLContents.Checked)
                where.Append(" AND type != " + (int)Database.ContentType.HTML);
            where.Append((mainData.Tag as Page).Where);
            return where.ToString();
        }

        private void RefreshDataMainView()
        {
            var clearButton = this.Controls.Find("ClearWhere", true).FirstOrDefault() as Button;
            if (clearButton != null)
            {
                clearButton.Parent.Controls.Remove(clearButton);
                clearButton.Dispose();
            }
            for (int i = menuPages.MenuItems.Count - 1; i >= 0; i--)
            {
                var m = menuPages.MenuItems[i];
                menuPages.MenuItems.RemoveAt(i);
                m.Dispose();
            }

            mainData.Clear();
            mainData.mDB = mDB;

            string where = CalcWhere();
            if (!string.IsNullOrWhiteSpace((mainData.Tag as Page).Where))
            {
                Button clearAll = new Button();
                clearAll.Name = "ClearWhere";
                clearAll.Text = "Show All";
                clearAll.Dock = DockStyle.Top;
                clearAll.Click += (s, e) =>
                {
                    (mainData.Tag as Page).Where = "";
                    RefreshDataMainView();
                };
                this.Controls.Add(clearAll);
                mainData.BringToFront();
            }

            int epp = Properties.Settings.Default.EntriesPerPage;
            int currentPage = (mainData.Tag as Page).Current;
            int totalEntries = mDB.TotalEntries(where);
            int pages = (int)Math.Ceiling((double)totalEntries / (double)epp);

            string status = string.Format(" ({2}:{0}:{1}MB)",
                totalEntries,
                ((double)new System.IO.FileInfo(mDB.Path).Length / 1024 / 1024).ToString("0.00"),
                System.IO.Path.GetFileName(mDB.Path));

            if (pages == 0)
            {
                this.Text = Application.ProductName + status;
                return;
            }

            if (currentPage < 1) currentPage = 1;
            if (currentPage > pages) currentPage = pages;
            (mainData.Tag as Page).Current = currentPage;
            this.Text = Application.ProductName + 
                " [" + string.Format(Properties.Resources.PageNumberFormat, currentPage) + "]" + status;

            foreach (Database.Entry e in mDB.Paging(
                where.ToString(), 
                showAscendingOrder.Checked ? "ts ASC, id ASC" : "ts DESC, id DESC", 
                (currentPage - 1) * epp,
                epp))
            {
                mainData.Add(e);
            }
            mainData.Refresh();

            var startEnd = Helper.SlidingWindow(pages, 10, currentPage);
            Action<int, string> addButton = (i, key) =>
            {
                MenuItem button = new MenuItem();
                button.Text = string.Format(Properties.Resources.PageNumberFormat, i);
                button.Checked = i == currentPage;
                button.RadioCheck = true;
                button.Tag = i;
                button.Click += (s, e) =>
                {
                    (mainData.Tag as Page).Current = i;
                    RefreshDataMainView();
                };
                if (i >= 1 && i <= 12)
                    button.Shortcut = System.Windows.Forms.Shortcut.F1 + i - 1;
                menuPages.MenuItems.Add(button);
            };

            if (startEnd.Item1 != 1) addButton(1, "left");
            for (int i = startEnd.Item1; i <= startEnd.Item2; i++) addButton(i, "page");
            if (startEnd.Item2 != pages) addButton(pages, "right");

        }
    }
}