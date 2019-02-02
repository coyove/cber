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
        public static Database mDB;

        private IntPtr mNextClipboardViewer;

        private System.Timers.Timer mTimer;

        private bool mListenDeactivated;

        private bool mRealExit;

        public FormCB()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const int WM_DRAWCLIPBOARD = 0x308;
            const int WM_CHANGECBCHAIN = 0x030D;
            const int WM_HOTKEY = 0x0312;

            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    OnClipboardChanged();
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
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private void OnClipboardChanged()
        {
            if (mListenDeactivated || IsTopWindow()) return;

            IDataObject data = Clipboard.GetDataObject();
            if (mDB == null || data == null) return;

            object html = Clipboard.GetData(DataFormats.Html);
            bool finished = false;
            if (html != null)
            {
                if (!listenHTMLContents.Checked) return;
                byte[] buf = Encoding.Default.GetBytes(html.ToString());
                string content = Encoding.UTF8.GetString(buf);
                if (content == "") return;
                string url = Helper.ExtractFieldFromHTMLClipboard(content, "SourceURL");

                if (Clipboard.ContainsImage())
                {
                    // Copy an image in browser?
                    if (string.IsNullOrWhiteSpace(url))
                        url = Helper.ExtractImgUrlFromHTML(content);
                    finished = Dot(() =>
                        mDB.Insert(null, data.GetData(DataFormats.Bitmap, true) as Image, url));
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

                    finished = plainScript ?
                        Dot(() => mDB.Insert(null, data.GetData(typeof(string))?.ToString(), url)) :
                        Dot(() => mDB.Insert(null, data.GetData(typeof(string))?.ToString(), content, url));
                }
            }
            else if (Clipboard.ContainsText())
            {
                if (!listenTextContents.Checked) return;
                string content = data.GetData(DataFormats.UnicodeText)?.ToString();
                if (content == null) return;
                //content = Encoding.UTF8.GetString(Encoding.Default.GetBytes(content));
                string url = Helper.GetHostFromUri(content) != "" ? content : "";
                if (content == "") return;
                finished = Dot(() =>
                    mDB.Insert(null, content, url));
            }
            else if (Clipboard.ContainsImage())
            {
                if (!listenImageContents.Checked) return;
                finished = Dot(() =>
                    mDB.Insert(null, data.GetData(DataFormats.Bitmap, true) as Image));
            }

            if (finished)
            {
                RefreshDataMainView();
            }
            else
            {
                statusMessage.Text = Properties.Resources.statusOperationTimedout;
            }
        }

        private void NavBtn_Click(object sender, EventArgs e)
        {
            int p = (int)(sender as ToolStripButton).Tag;
            (mainData.Tag as Page).Current = p;
            RefreshDataMainView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSettings();
            mRealExit = false;
            mNextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);
            mTimer = new System.Timers.Timer(2000);
            mTimer.AutoReset = true;
            mTimer.Enabled = true;
            mTimer.Elapsed += (v1, v2) =>
            {
                mainData.Invalidate();
                foreach (var t in mPendingThreads)
                {
                    DateTime dummy;
                    if (!t.Key.IsAlive)
                        mPendingThreads.TryRemove(t.Key, out dummy);
                    else if (DateTime.Now.Subtract(t.Value).TotalSeconds > 2)
                        t.Key.Abort();
                }
            };
            mainData.RowTemplate.MinimumHeight = 100;
            mainData.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(mainData, true, null);
            mainData.ShowCellToolTips = false;
            mainData.Tag = new Page() { Current = 1 };
            entryName.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            entryContent.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            entryContent.DefaultCellStyle.Font = new Font("Consolas", 12);
            notifyIcon.Icon = Properties.Resources.SystrayIcon;
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem(listenToolStripMenuItem.Text, listenToolStripMenuItem_Click),
                new MenuItem("Open", (v1, v2) => notifyIcon_MouseDoubleClick(v1, null)),
                new MenuItem(exitToolStripMenuItem.Text, exitToolStripMenuItem_Click),
            });

            
            foreach (DictionaryEntry res in Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.InvariantCulture, false, true))
            {
                if (res.Key.ToString().EndsWith("_Mode"))
                {
                    ToolStripMenuItem code = new ToolStripMenuItem();
                    code.CheckOnClick = true;
                    code.Text = res.Key.ToString();
                    code.Text = code.Text.Substring(0, code.Text.Length - 5);
                    code.Click += plainTextToolStripMenuItem_Click;
                    buttonCodeDropdown.DropDownItems.Add(code);
                }
            }

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
                MessageBox.Show(string.Format(Properties.Resources.databaseNotAvailable, dbPath), 
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                exitToolStripMenuItem_Click(null, null);
                return;
            }

            RefreshDataMainView();

            listenToolStripMenuItem_Click(listenToolStripMenuItem, null);

            RegisterShortcut(Properties.Settings.Default.GSShow, 
                () => notifyIcon_MouseDoubleClick(null, null));
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mDB != null)
                mDB.Close();

            mTimer.Stop();
            mTimer.Dispose();
            SaveSettings();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ClearPanel2(bool preview = true)
        {
            for (int i = splitContainer.Panel2.Controls.Count - 1; i >= 0; i--)
            {
                var ctrlp = splitContainer.Panel2.Controls[i];
                if (ctrlp != toolbarEdit) splitContainer.Panel2.Controls.Remove(ctrlp);
            }
            buttonHTMLToText.Enabled = buttonCodeDropdown.Enabled = buttonSaveChange.Enabled = buttonEdit.Enabled = false;
            if (!preview) return;
            Label lbl = new Label();
            lbl.Dock = DockStyle.Fill;
            lbl.Text = "Preview";
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            splitContainer.Panel2.Controls.Add(lbl);
        }

        private void mainData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= mainData.Rows.Count || e.RowIndex < 0) return;

            ClearPanel2(false);
            var entry = mainData.Rows[e.RowIndex].Tag as Database.Entry;
            mLastIndex = e.RowIndex;
            Control ctrl;
            switch (entry.Type)
            {
                case Database.ContentType.Image:
                    var viewer = new ImageViewer();
                    viewer.Dock = DockStyle.Fill;
                    viewer.Image = entry.Content as Image;
                    viewer.SizeMode = PictureBoxSizeMode.Zoom;
                    splitContainer.Panel2.Controls.Add(viewer);
                    viewer.CalcFitZoom();
                    buttonEdit.Enabled = true;
                    buttonEdit.Tag = entry;
                    ctrl = viewer;
                    break;
                case Database.ContentType.HTML:
                    SplitContainer inner = new SplitContainer();
                    inner.Orientation = (Orientation)Math.Abs((int)splitContainer.Orientation - 1);
                    inner.Dock = DockStyle.Fill;

                    WebBrowser web = new WebBrowser();
                    web.Dock = DockStyle.Fill;
                    web.DocumentText = Helper.ExtractHTMLFromClipboard(entry.Content.ToString());
                    web.Navigating += (v1, v2) => { v2.Cancel = true; };
                    inner.Panel1.Controls.Add(web);

                    TextBox webText = new TextBox();
                    webText.BorderStyle = BorderStyle.None;
                    webText.ReadOnly = true;
                    webText.Multiline = true;
                    webText.Text = entry.Html;
                    webText.Dock = DockStyle.Fill;
                    webText.ScrollBars = ScrollBars.Vertical;
                    webText.Font = mainData.DefaultCellStyle.Font;
                    inner.Panel2.Controls.Add(webText);

                    buttonHTMLToText.Enabled = true;
                    buttonHTMLToText.Tag = entry;

                    splitContainer.Panel2.Controls.Add(inner);
                    inner.SplitterDistance = inner.Width / 2;
                    ctrl = inner;
                    break;
                default:
                    var text = new ICSharpCode.TextEditor.TextEditorControl();
                    text.Dock = DockStyle.Fill;
                    text.Text = entry.Content.ToString();
                    splitContainer.Panel2.Controls.Add(text);
                    ctrl = text;
                    buttonSaveChange.Enabled = true;
                    buttonSaveChange.Tag = new object[] { entry, text };
                    buttonCodeDropdown.Enabled = true;
                    foreach (var item in buttonCodeDropdown.DropDownItems)
                        if ((item as ToolStripMenuItem).Checked)
                            plainTextToolStripMenuItem_Click(item, null); 
                    break;
            }
            ctrl.BringToFront();
            splitContainer.Panel2.Tag = ctrl;

            if (!(mainData.Columns[e.ColumnIndex] is DataGridViewButtonColumn)) return;

            mListenDeactivated = true;
            Helper.SetClipboard(entry);
            mListenDeactivated = false;

            if (hideAfterCopyToolStripMenuItem.Checked)
                Hide();
        }

        private void mainData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void mainData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= mainData.Rows.Count || e.RowIndex < 0) return;
            string newName = mainData.Rows[e.RowIndex].Cells["entryName"].Value.ToString();
            mDB.Rename((mainData.Rows[e.RowIndex].Tag as Database.Entry).Id, newName);
        }

        private void mainData_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            mDB.Delete((e.Row.Tag as Database.Entry).Id);
        }

        private void buttonSaveChange_Click(object sender, EventArgs e)
        {
            var tags = buttonSaveChange.Tag as object[];
            mDB.UpdateContent((tags[0] as Database.Entry).Id, (tags[1] as ICSharpCode.TextEditor.TextEditorControl).Text);
            RefreshDataMainView();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                mListenDeactivated = true;

                var img = (buttonEdit.Tag as Database.Entry).Content as Image;
                string fn = (Path.GetTempFileName()) + ".png";
                img.Save(fn);

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = Properties.Settings.Default.ExternalImageEditor;
                psi.Arguments = fn;
                var p = Process.Start(psi);
                p.WaitForExit();

                img = Image.FromFile(fn);
                (splitContainer.Panel2.Tag as PictureBox).Image = img;
                (buttonEdit.Tag as Database.Entry).Content = img;
                mDB.UpdateContent((buttonEdit.Tag as Database.Entry).Id, img);

                RefreshDataMainView();
            }
            catch (Exception ex)
            {
                statusMessage.Text = ex.Message;
            }
            finally
            {
                mListenDeactivated = false;
            }
        }

        private void horizontalVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer.Orientation = splitContainer.Orientation == Orientation.Vertical ?
                Orientation.Horizontal :
                Orientation.Vertical;
            foreach (var ctrl in splitContainer.Panel2.Controls)
            {
                if (ctrl is SplitContainer)
                {
                    var sc = (ctrl as SplitContainer);
                    sc.Orientation = (Orientation)Math.Abs((int)splitContainer.Orientation - 1);
                    sc.SplitterDistance = sc.Width / 2;
                }
            }
        }

        private void stayOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = stayOnTopToolStripMenuItem.Checked;
        }

        private void listenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender != listenToolStripMenuItem)
                listenToolStripMenuItem.Checked = !listenToolStripMenuItem.Checked;
            mListenDeactivated = !listenToolStripMenuItem.Checked;
            notifyIcon.ContextMenu.MenuItems[0].Checked = !mListenDeactivated;
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
                    MessageBox.Show(string.Format(Properties.Resources.databaseNotAvailable, openDb.FileName),
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
            mListenDeactivated = true;
            frm.ShowDialog();
            mListenDeactivated = false;
        }

        private void plainTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in buttonCodeDropdown.DropDownItems)
                (item as ToolStripMenuItem).Checked = false;

            var m = (sender as ToolStripMenuItem);
            m.Checked = true;

            foreach (var ctrl in splitContainer.Panel2.Controls)
            {
                if (ctrl is ICSharpCode.TextEditor.TextEditorControl)
                {
                    (ctrl as ICSharpCode.TextEditor.TextEditorControl).SetHighlighting(
                        m.Tag?.ToString() == "Plain" ? "" : m.Text
                    );
                    break;
                }
            }
        }

        private void buttonUrls_Click(object sender, EventArgs e)
        {
            FormSearch frm = new FormSearch();
            mListenDeactivated = true;
            frm.SwitchTab((sender as ToolStripButton).Tag as string);
            frm.ShowDialog();
            mListenDeactivated = false;
            (mainData.Tag as Page).Where = frm.WhereClause;
            RefreshDataMainView();
        }

        private void buttonClearWhere_Click(object sender, EventArgs e)
        {
            (mainData.Tag as Page).Where = "";
            RefreshDataMainView();
        }

        private void mainData_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
        }

        private void mainData_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (mainData.SelectedRows.Count == 0)
            RefreshDataMainView();
        }

        private void buttonHTMLToText_Click(object sender, EventArgs e)
        {
            mDB.UpdateContent((buttonHTMLToText.Tag as Database.Entry).Id, Database.ContentType.RawText);
            RefreshDataMainView();
        }
    }
}
