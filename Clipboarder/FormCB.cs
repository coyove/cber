using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Clipboarder
{
    public partial class FormCB : Form
    {
        private Database mDB;

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
            if (mListenDeactivated) return;

            IDataObject data = Clipboard.GetDataObject();
            if (mDB == null || data == null) return;

            object html = Clipboard.GetData(DataFormats.Html);
            if (html != null)
            {
                byte[] buf = Encoding.Default.GetBytes(html.ToString());
                string content = Encoding.UTF8.GetString(buf);
                if (content == "") return;
                string url = Helper.ExtractFieldFromHTMLClipboard(content, "SourceURL");

                if (Clipboard.ContainsImage())
                {
                    // Copy an image in browser?
                    if (string.IsNullOrWhiteSpace(url))
                        url = Helper.ExtractImgUrlFromHTML(content);
                    mDB.Insert(null, data.GetData(DataFormats.Bitmap, true) as Image, url);
                }
                else
                {
                    mDB.Insert(null, data.GetData(typeof(string))?.ToString(), content, url);
                }
            }
            else if (Clipboard.ContainsText())
            {
                string content = data.GetData(DataFormats.UnicodeText)?.ToString();
                if (content == null) return;
                //content = Encoding.UTF8.GetString(Encoding.Default.GetBytes(content));
                string url = Helper.GetHostFromUri(content) != "" ? content : "";
                if (content == "") return;
                mDB.Insert(null, content, url);
            }
            else if (Clipboard.ContainsImage())
            {
                mDB.Insert(null, data.GetData(DataFormats.Bitmap, true) as Image);
            }

            RefreshDataMainView();
        }

        private void NavBtn_Click(object sender, EventArgs e)
        {
            int p = (int)(sender as Button).Tag;
            (mainData.Tag as Page).Current = p;
            RefreshDataMainView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mRealExit = false;
            mNextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);
            mTimer = new System.Timers.Timer(2000);
            mTimer.AutoReset = true;
            mTimer.Enabled = true;
            mTimer.Elapsed += (v1, v2) => mainData.Invalidate();
            mainData.RowTemplate.MinimumHeight = 100;
            mainData.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(mainData, true, null);
            mainData.ShowCellToolTips = false;
            mainData.Tag = new Page() { Current = 1 };
            entryName.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            entryContent.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            entryContent.DefaultCellStyle.Font = new Font("Consolas", 12);
            notifyIcon.Icon = Properties.Resources.SystrayIcon;
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem("Listen", listenToolStripMenuItem_Click),
                new MenuItem("Open", (v1, v2) => notifyIcon_MouseDoubleClick(v1, null)),
                new MenuItem("Exit", exitToolStripMenuItem_Click),
            });

            System.IO.File.Delete("test.db");
            mDB = Database.Open("test.db");
            RefreshDataMainView();

            listenToolStripMenuItem_Click(listenToolStripMenuItem, null);

            RegisterShortcut("Win+Oemtilde", () => notifyIcon_MouseDoubleClick(null, null));
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mDB != null)
                mDB.Close();

            mTimer.Stop();
            mTimer.Dispose();
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
                if (!(ctrlp is Panel)) splitContainer.Panel2.Controls.Remove(ctrlp);
            }
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
            buttonSaveChange.Enabled = false;
            buttonEdit.Enabled = false;
            Control ctrl;
            switch (entry.Type)
            {
                case Database.ContentType.Image:
                    PictureBox viewer = new PictureBox();
                    viewer.Dock = DockStyle.Fill;
                    viewer.Image = entry.Content as Image;
                    viewer.SizeMode = PictureBoxSizeMode.Zoom;
                    splitContainer.Panel2.Controls.Add(viewer);
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

                    splitContainer.Panel2.Controls.Add(inner);
                    inner.SplitterDistance = inner.Width * 3 / 4;
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

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            IntPtr topWindow = WindowFromPoint(new Point(this.Location.X + 5, this.Location.Y + 5));
            if (topWindow == this.Handle)
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
    }
}
