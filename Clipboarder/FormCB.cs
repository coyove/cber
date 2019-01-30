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
                mDB.Insert(null, content, url, Database.ContentType.HTML);
            }
            else if (Clipboard.ContainsText())
            {
                string content = data.GetData(typeof(string)).ToString();
                string url = Helper.GetHostFromUri(content) != "" ? content : "";
                if (content == "") return;
                mDB.Insert(null, content, url);
            }
            else if (Clipboard.ContainsImage())
            {
                Image content = (Image)data.GetData(DataFormats.Bitmap, true);
                MemoryStream buf = new MemoryStream();
                content.Save(buf, System.Drawing.Imaging.ImageFormat.Png);
                mDB.Insert(null, buf.GetBuffer(), Database.ContentType.Image);
            }

            RefreshDataMainView();
        }

        private void RefreshDataMainView()
        {
            mainData.Rows.Clear();
            foreach (Database.Entry e in mDB.Paging(null, null, 0, 20))
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
                    cell.Title = new Title { No = e.Id, Hits = e.Hits, Time = e.Time };
                    cell.Url = Helper.GetHostFromUri(e.SourceUrl);
                    var text = e.Content.ToString();
                    if (e.Type == Database.ContentType.HTML)
                    {
                        text = Helper.ExtractHTMLFromClipboard(text);
                        text = Helper.ExtractTextFromHTML(text);
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mNextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);
            mTimer = new System.Timers.Timer(2000);
            mTimer.AutoReset = true;
            mTimer.Enabled = true;
            mTimer.Elapsed += (v1, v2) => mainData.Invalidate();
            mainData.RowTemplate.MinimumHeight = 100;
            mainData.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(mainData, true, null);
            mainData.ShowCellToolTips = false;
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

            splitContainer.Panel2.Controls.Clear();
            var entry = mainData.Rows[e.RowIndex].Tag as Database.Entry;
            switch (entry.Type)
            {
                case Database.ContentType.Image:
                    PictureBox viewer = new PictureBox();
                    viewer.Dock = DockStyle.Fill;
                    viewer.Image = entry.Content as Image;
                    viewer.SizeMode = PictureBoxSizeMode.Zoom;
                    splitContainer.Panel2.Controls.Add(viewer);
                    break;
                case Database.ContentType.HTML:
                    WebBrowser web = new WebBrowser();
                    web.Dock = DockStyle.Fill;
                    web.DocumentText = Helper.ExtractHTMLFromClipboard(entry.Content.ToString());
                    splitContainer.Panel2.Controls.Add(web);
                    break;
                default:
                    var text = new ICSharpCode.TextEditor.TextEditorControl();
                    text.Dock = DockStyle.Fill;
                    text.Text = entry.Content.ToString();
                    splitContainer.Panel2.Controls.Add(text);
                    break;
            }

            if (!(mainData.Columns[e.ColumnIndex] is DataGridViewButtonColumn)) return;

            mActivated = true;
            switch (entry.Type)
            {
                case Database.ContentType.HTML:
                    Clipboard.SetData(DataFormats.Html, entry.Content);
                    break;
            }
            mActivated = false;
        }
    }
}
