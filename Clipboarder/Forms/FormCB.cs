using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace Clipboarder
{
    public partial class FormCB : Form
    {
        public static Action<string> Log = (msg) => File.AppendAllText("cber.log", DateTime.Now.ToString() + " " + msg + Environment.NewLine);

        public static Database mDB;

        public static ScriptEngine mRule;

        public ToolBar mBar;
        public ToolBar mBarNav;
        public ToolBarButton[] mViewFilter = new ToolBarButton[3];

        private IntPtr mNextClipboardViewer;

        private int mListenDeactivated = 0;

        private bool mRealExit;

        public FormCB()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Down) mainData.ScrollBit(true);
            if (keyData == Keys.Up) mainData.ScrollBit(false);
            if (mainData.TryShortcut(keyData)) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const int WM_DRAWCLIPBOARD = 0x308;
            const int WM_CHANGECBCHAIN = 0x030D;
            const int WM_HOTKEY = 0x0312;

            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    try
                    {
                        Thread t = new Thread(() =>
                        {
                            if (Interlocked.CompareExchange(ref mListenDeactivated, 1, 0) == 1) return;
                            this.Invoke(new Action(() =>
                                                {
                                                    this.Enabled = false;
                                                    this.Text += " (Busy)";
                                                    OnClipboardChanged();
                                                    this.Enabled = true;
                                                    this.Text = this.Text.Substring(0, this.Text.Length - 7);
                                                }));
                            mListenDeactivated = 0;
                        });
                        t.Start();
                    }
                    catch (Exception e)
                    {
                        Log(e.ToString());
                    }
                    SendMessage(mNextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;
                case WM_CHANGECBCHAIN:
                    if (m.WParam == mNextClipboardViewer)
                        mNextClipboardViewer = m.LParam;
                    else
                        SendMessage(mNextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;
                case WM_HOTKEY:
                    Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                    uint modifier = (uint)m.LParam & 0xFFFF;
                    UInt64 k64 = ((UInt64)modifier << 32) | (UInt64)key;
                    if (mShortcutsMap.ContainsKey(k64)) mShortcutsMap[k64]();
                    break;
            }
            base.WndProc(ref m);
            //System.Diagnostics.Debug.Print(m.Msg.ToString("x"));
        }

        private char TryInsert(ref Image image, ref string url)
        {
            if (mRule == null) return '\0';
            string rawText = null, html = null;
            return mRule.Exec(ref rawText, ref html, ref image, ref url);
        }

        private char TryInsert(ref string rawText, ref string url)
        {
            if (mRule == null) return '\0';
            string html = null;
            Image image = null;
            return mRule.Exec(ref rawText, ref html, ref image, ref url);
        }

        private char TryInsert(ref string rawText, ref string html, ref string url)
        {
            if (mRule == null) return '\0';
            Image image = null;
            return mRule.Exec(ref rawText, ref html, ref image, ref url);
        }

        private void OnClipboardChanged()
        {
            if (IsTopWindow() && !stayOnTopToolStripMenuItem.Checked) return;

            IDataObject data = Clipboard.GetDataObject();
            if (mDB == null || data == null) return;

            char action = '\0';

            object html = Clipboard.GetData(DataFormats.Html);
            if (html != null)
            {
                if (!listenHTMLContents.Checked) return;
                string content = ClipboardWorkaround.GetHtml();
                if (content == "") return;
                string url = Helper.ExtractFieldFromHTMLClipboard(content, "SourceURL");

                if (Clipboard.ContainsImage())
                {
                    // Copy an image in browser?
                    if (string.IsNullOrWhiteSpace(url))
                        url = Helper.ExtractImgUrlFromHTML(content);

                    Image img = data.GetData(DataFormats.Bitmap, true) as Image;
                    if ((action = TryInsert(ref img, ref url)) == 'd') return;
                    mDB.Insert(img, url);
                }
                else
                {
                    bool plainScript = false;
                    try
                    {
                        Uri uri = new Uri(url);
                        plainScript = (uri.AbsolutePath.EndsWith(".js") || uri.AbsolutePath.EndsWith(".css"));
                    }
                    catch (Exception) { }

                    string rawText = data.GetData(DataFormats.UnicodeText)?.ToString();
                    if (plainScript)
                    {
                        if ((action = TryInsert(ref rawText, ref url)) == 'd') return;
                        mDB.Insert(null, rawText, url);
                    }
                    else
                    {
                        if ((action = TryInsert(ref rawText, ref content, ref url)) == 'd') return;
                        mDB.Insert(rawText, content, url);
                    }
                }
            }
            else if (Clipboard.ContainsText())
            {
                if (!listenTextContents.Checked) return;
                string content = data.GetData(DataFormats.UnicodeText)?.ToString();
                if (content == null) return;
                string url = Helper.GetHostFromUri(content) != "" ? content : "";

                if ((action = TryInsert(ref content, ref url)) == 'd') return;
                mDB.Insert(content, url);
            }
            else if (Clipboard.ContainsImage())
            {
                if (!listenImageContents.Checked) return;
                var img = data.GetData(DataFormats.Bitmap, true) as Image;
                string url = null;
                if ((action = TryInsert(ref img, ref url)) == 'd') return;
                mDB.Insert(img);
            }

            if (action == 'f')
            {
                mDB.Favorite(mDB.LastRowId(), true);
            }

            RefreshDataMainView();
            mDB.KeepEntriesUnder(
                (Database.AutoDeletionPolicy)Properties.Settings.Default.XPurge,
                Properties.Settings.Default.XPurgeValue);
        }

        private void NavBtn_Click(object sender, EventArgs e)
        {
            int p = (int)((sender as ToolStripButton).Tag ?? 0);
            (mainData.Tag as Page).Current = p;
            RefreshDataMainView();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitUi();
            LoadSettings();

            if (Properties.Settings.Default.RuleEnabled)
            {
                mRule = new ScriptEngine();
                mRule.Compile(Properties.Settings.Default.RuleScript);
            }

            mRealExit = false;
            mNextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);
            mainData.Tag = new Page() { Current = 1 };
            notifyIcon.Icon = Properties.Resources.SystrayIcon;
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem(listenToolStripMenuItem.Text, listenToolStripMenuItem_Click),
                new MenuItem("Open", (v1, v2) => notifyIcon_MouseDoubleClick(v1, null)),
                new MenuItem(exitToolStripMenuItem.Text, exitToolStripMenuItem_Click),
            });

            string dbPath = Properties.Settings.Default.DbPath == "." ?
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "cber.db") :
                Properties.Settings.Default.DbPath;
            //System.IO.File.Delete("test.db");
            try
            {
                mDB = Database.Open(dbPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                exitToolStripMenuItem_Click(null, null);
                return;
            }
            if (mDB == null)
            {
                MessageBox.Show(string.Format(Properties.Resources.DatabaseNotAvailable, dbPath),
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                exitToolStripMenuItem_Click(null, null);
                return;
            }

            mainData.mDB = mDB;
            mainData.EditImageCallback = EditImage;
            mainData.CopyCallback = (ce) =>
            {
                mListenDeactivated = 1;
                Helper.SetClipboard(ce);
                mListenDeactivated = 0;
                if (hideAfterCopyToolStripMenuItem.Checked) Hide();
            };
            mainData.DeleteCallback = (de) =>
            {
                int old = mainData.Value;
                mDB.Delete(de.Id);
                RefreshDataMainView();
                mainData.TryScrollTo(old);
            };
            RefreshDataMainView();

            listenToolStripMenuItem_Click(listenToolStripMenuItem, null);

            RegisterShortcut(Properties.Settings.Default.GSShow,
                () => notifyIcon_MouseDoubleClick(null, null));
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mDB != null)
                mDB.Close();

            SaveSettings();
        }

        private Image EditImage(Image img)
        {
            try
            {
                mListenDeactivated = 1;

                string fn = (Path.GetTempFileName()) + ".png";
                img.Save(fn);

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = Properties.Settings.Default.ExternalImageEditor;
                psi.Arguments = fn;
                var p = Process.Start(psi);
                p.WaitForExit();

                img = Image.FromFile(fn);
            }
            catch (Exception ex)
            {
                Log(ex.ToString());
            }
            finally
            {
                mListenDeactivated = 0;
            }
            return img;
        }

        private void stayOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = stayOnTopToolStripMenuItem.Checked;
        }

        private void listenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender != listenToolStripMenuItem)
                listenToolStripMenuItem.Checked = !listenToolStripMenuItem.Checked;
            mListenDeactivated = listenToolStripMenuItem.Checked ? 0 : 1;
            notifyIcon.ContextMenu.MenuItems[0].Checked = mListenDeactivated == 0;
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshDataMainView();
        }

        public bool IsTopWindow()
        {
            // Use 5 points to identify, work in most cases
            int x = this.Location.X, y = this.Location.Y;
            Func<IntPtr, bool> test = (h) =>
            {
                do if (h == this.Handle) return true;
                while ((h = GetParent(h)) != IntPtr.Zero);
                return false;
            };
            IntPtr w1 = WindowFromPoint(new Point(x + 5, y + 5));
            IntPtr w2 = WindowFromPoint(new Point(x + this.Width - 5, y + 5));
            IntPtr w3 = WindowFromPoint(new Point(x + 5, y + this.Height - 5));
            IntPtr w4 = WindowFromPoint(new Point(x + this.Width - 5, y + this.Height - 5));
            IntPtr w5 = WindowFromPoint(new Point(x + this.Width / 2, y + this.Height / 2));
            return test(w1) && test(w2) && test(w3) && test(w4) && test(w5);
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (IsTopWindow())
            {
                this.Hide();
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
                this.Show();
                SetForegroundWindow(this.Handle);
                this.WindowState = FormWindowState.Normal;
                this.BringToFront();
            }
        }

        private void FormCB_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mRealExit = true;
            Close();
        }

        private void FormCB_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!mRealExit)
            {
                Hide();
                e.Cancel = true;
                return;
            }

            UnregisterShortcuts();
        }

        private void clearEntriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Properties.Resources.DeleteAllConfirm,
                Application.ProductName, MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            mDB.Delete(-1);
            RefreshDataMainView();
        }

        private void showTextContentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshDataMainView();
        }

        private void statusDbPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDb = new OpenFileDialog();
            openDb.Filter = "*.db|*.db";

            if (openDb.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    mDB = Database.Open(openDb.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (mDB == null)
                {
                    MessageBox.Show(string.Format(Properties.Resources.DatabaseNotAvailable, openDb.FileName),
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Properties.Settings.Default.DbPath = openDb.FileName;
                Properties.Settings.Default.Save();
                RefreshDataMainView();
            }
        }

        private void openDatabaseLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Path.GetDirectoryName(mDB.Path));
        }

        private void shortcutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSettings frm = new FormSettings();
            mListenDeactivated = 1;
            frm.ShowDialog();
            mListenDeactivated = 0;
        }

        private void PrepareSearch(string t)
        {
            foreach (ToolBarButton btn in mBar.Buttons)
            {
                if (btn.Tag.ToString() == "search" + t)
                    btn.Pushed = true;
            }
            FormSearch frm = new FormSearch();
            mListenDeactivated = 1;
            frm.SwitchTab(t);
            frm.ShowDialog();
            mListenDeactivated = 0;
            (mainData.Tag as Page).Where = frm.WhereClause;
            if (string.IsNullOrWhiteSpace(frm.WhereClause))
            {
                foreach (ToolBarButton btn in mBar.Buttons) btn.Pushed = false;
            }
            frm.Dispose();
            RefreshDataMainView();
        }

        private void buttonClearWhere_Click(object sender, EventArgs e)
        {
            (mainData.Tag as Page).Where = "";
            //buttonFavorites.Checked = false;
            RefreshDataMainView();
        }

        private void searchDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSearch frm = new FormSearch();
            mListenDeactivated = 1;
            frm.SwitchTab("time");
            frm.SearchAndDelete = true;
            frm.ShowDialog();
            mListenDeactivated = 0;
            if (string.IsNullOrWhiteSpace(frm.WhereClause)) return;
            mDB.Delete(frm.WhereClause);
            RefreshDataMainView();
        }
    }
}
