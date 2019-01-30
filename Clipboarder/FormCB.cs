using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        class Page
        {
            public int Current;
        }

        [DllImport("User32.dll")]
        protected static extern int SetClipboardViewer(int hWndNewViewer);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        private Database mDB;

        private IntPtr mNextClipboardViewer;

        private System.Timers.Timer mTimer;

        private bool mActivated;

        private bool mClearingRows;

        public FormCB()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            // defined in winuser.h
            const int WM_DRAWCLIPBOARD = 0x308;
            const int WM_CHANGECBCHAIN = 0x030D;

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

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private void OnClipboardChanged()
        {
            if (mActivated) return;

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

        private void RefreshDataMainView()
        {
            mClearingRows = true;
            mainData.Rows.Clear();
            mClearingRows = false;

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
                    cell.Title = new Title { No = e.Id, Hits = e.Hits, Time = e.Time, Size = ((Image)e.Content).Size };
                    cell.Value = e.Content;
                }
            }

            var size = TextRenderer.MeasureText("A", panelNav.Font, new Size(0, 0));
            var buttonSize = new Size(size.Width * 3, size.Height * 2);
            panelNav.Controls.Clear();
            panelNav.Height = size.Height * 2 + 10;
            Button btnFirst = new Button();
            btnFirst.Size = buttonSize;
            btnFirst.Location = new Point(5, 5);
            btnFirst.Text = "<<";
            btnFirst.Tag = 1;
            btnFirst.Click += NavBtn_Click;
            panelNav.Controls.Add(btnFirst);;
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
        }

        private void NavBtn_Click(object sender, EventArgs e)
        {
            int p = (int)(sender as Button).Tag;
            (mainData.Tag as Page).Current = p;
            RefreshDataMainView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mClearingRows = false;
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

            System.IO.File.Delete("test.db");
            mDB = Database.Open("test.db");
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

        private void mainData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= mainData.Rows.Count || e.RowIndex < 0) return;

            for (int i = splitContainer.Panel2.Controls.Count - 1; i >= 0; i--) {
                var ctrlp = splitContainer.Panel2.Controls[i];
                if (!(ctrlp is Panel)) splitContainer.Panel2.Controls.Remove(ctrlp);
            }

            var entry = mainData.Rows[e.RowIndex].Tag as Database.Entry;
            buttonSaveChange.Enabled = false;
            Control ctrl;
            switch (entry.Type)
            {
                case Database.ContentType.Image:
                    PictureBox viewer = new PictureBox();
                    viewer.Dock = DockStyle.Fill;
                    viewer.Image = entry.Content as Image;
                    viewer.SizeMode = PictureBoxSizeMode.Zoom;
                    splitContainer.Panel2.Controls.Add(viewer);
                    ctrl = viewer;
                    break;
                case Database.ContentType.HTML:
                    WebBrowser web = new WebBrowser();
                    web.Dock = DockStyle.Fill;
                    web.DocumentText = Helper.ExtractHTMLFromClipboard(entry.Content.ToString());
                    splitContainer.Panel2.Controls.Add(web);
                    ctrl = web;
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

            if (!(mainData.Columns[e.ColumnIndex] is DataGridViewButtonColumn)) return;

            mActivated = true;
            switch (entry.Type)
            {
                case Database.ContentType.HTML:
                    DataObject obj = new DataObject();
                    obj.SetData(DataFormats.Html, entry.Content);
                    obj.SetData(DataFormats.StringFormat, entry.Html);
                    Clipboard.SetDataObject(obj, true);
                    break;
                case Database.ContentType.Image:
                    Clipboard.SetData(DataFormats.Bitmap, entry.Content);
                    break;
                default:
                    Clipboard.SetData(DataFormats.StringFormat, entry.Content);
                    break;
            }
            mActivated = false;
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
    }
}
