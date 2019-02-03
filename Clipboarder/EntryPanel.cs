using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Clipboarder
{
    class EntryPanel: Panel
    {
        private List<Database.Entry> mData = new List<Database.Entry>();

        private Dictionary<Database.Entry, Rectangle> mHotarea = new Dictionary<Database.Entry, Rectangle>();

        private Dictionary<Database.Entry, Rectangle> mUrlHotarea = new Dictionary<Database.Entry, Rectangle>();

        private ToolStripDropDownButton mCodeMenu = new ToolStripDropDownButton();

        private Panel mPanel;

        private double mK;

        public Database mDB;

        public Func<Image, Image> EditImageCallback;

        static int TextEntryHeight = 200;
        static int ImageEntryHeight = 200;
        static int ScrollbarWidth = 15;
        static int MinimalScrollbarHeight = 25;
        static Brush ScrollbarBrush = Brushes.Gray;
        static Font Monospace = new Font("Consolas", 12);
        static Size MonospaceSize = TextRenderer.MeasureText("A", Monospace);

        public EntryPanel() : base()
        {
            this.Padding = new Padding(0);
            this.DoubleBuffered = true;
            ToolStripMenuItem plainText = new ToolStripMenuItem();
            plainText.CheckOnClick = true;
            plainText.Checked = true;
            plainText.Text = "Plain";
            plainText.Click += CodeHighlight_Click;
            mCodeMenu.DropDownItems.Add(plainText);
            foreach (DictionaryEntry res in Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, true))
            {
                if (res.Key.ToString().EndsWith("_Mode"))
                {
                    ToolStripMenuItem code = new ToolStripMenuItem();
                    code.CheckOnClick = true;
                    code.Text = res.Key.ToString();
                    code.Text = code.Text.Substring(0, code.Text.Length - 5);
                    code.Click += CodeHighlight_Click;
                    mCodeMenu.DropDownItems.Add(code);
                }
            }
            mCodeMenu.Text = "Highlight";
            mCodeMenu.Image = Properties.Resources.text_x_script;
        }

        private void CodeHighlight_Click(object sender, EventArgs e)
        {
            foreach (var item in mCodeMenu.DropDownItems)
                (item as ToolStripMenuItem).Checked = false;

            var m = (sender as ToolStripMenuItem);
            m.Checked = true;

            foreach (var ctrl in mPanel.Controls)
            {
                if (ctrl is ICSharpCode.TextEditor.TextEditorControl)
                {
                    (ctrl as ICSharpCode.TextEditor.TextEditorControl).SetHighlighting(
                        m.Text == "Plain" ? "" : m.Text
                    );
                    break;
                }
            }
        }

        public int RealHeight
        {
            get
            {
                int height = 0;
                foreach (var datum in mData)
                {
                    height += datum.Content is Image ? ImageEntryHeight : TextEntryHeight;
                }
                return height;
            }
        }

        public void Add(Database.Entry datum)
        {
            mData.Add(datum);
        }

        public void Clear()
        {
            Value = 0;
            mData.Clear();
            Invalidate();
        }

        public int Value = 0;

        public int MaxValue { get; private set; }

        private Point sbDown = default(Point);
        private int sbValue = 0;
        private bool sbMove = false;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.X >= this.Width - ScrollbarWidth)
            {
                sbDown = e.Location;
                sbValue = Value;
                sbMove = true;
                return;
            }

            foreach (var kv in mUrlHotarea)
            {
                if (kv.Value.Contains(e.Location))
                {
                    Process.Start(kv.Key.SourceUrl);
                    return;
                }
            }

            this.Controls.Clear();
            foreach (var kv in mHotarea)
            {
                if (kv.Value.Contains(e.Location))
                {
                    mPanel = new Panel();
                    mPanel.Location = kv.Value.Location;
                    mPanel.Size = kv.Value.Size;

                    var toolbar = new ToolStrip();
                        toolbar.Dock = DockStyle.Top;

                    switch (kv.Key.Type)
                    {
                        case Database.ContentType.RawText:
                            var editor = new ICSharpCode.TextEditor.TextEditorControl();
                            editor.Text = kv.Key.Content as string;
                            editor.Dock = DockStyle.Fill;
                            mPanel.Controls.Add(editor);

                            toolbar.Items.Add(new ToolStripButton("Save",
                                Properties.Resources.document_save,
                                (sv1, sv2) =>
                                {
                                    mDB.UpdateContent(kv.Key.Id, editor.Text);
                                    kv.Key.Content = editor.Text;
                                    this.Controls.Clear();
                                    this.Invalidate();
                                }));
                            toolbar.Items.Add(mCodeMenu);
                            foreach (var item in mCodeMenu.DropDownItems)
                                if ((item as ToolStripMenuItem).Checked)
                                    CodeHighlight_Click(item, null);
                            break;
                        case Database.ContentType.HTML:
                            var browser = new WebBrowser();
                            browser.Dock = DockStyle.Fill;
                            browser.DocumentText = Helper.ExtractHTMLFromClipboard(kv.Key.Content.ToString());
                            browser.Navigating += (v1, v2) => { v2.Cancel = true; };
                            browser.ScriptErrorsSuppressed = true;
                            mPanel.Controls.Add(browser);

                            toolbar.Items.Add(new ToolStripButton("HTML to Text",
                                Properties.Resources.text_html,
                                (httv1, httv2) =>
                                {
                                    mDB.UpdateContent(kv.Key.Id, Database.ContentType.RawText);
                                    kv.Key.Type = Database.ContentType.RawText;
                                    this.Controls.Clear();
                                    this.Invalidate();
                                }));
                            break;
                        case Database.ContentType.Image:
                            var viewer = new ImageViewer();
                            viewer.Dock = DockStyle.Fill;
                            viewer.Image = kv.Key.Content as Image;
                            mPanel.Controls.Add(viewer);
                            viewer.CalcFitZoom();
                            toolbar.Items.Add(new ToolStripButton("Edit",
                                Properties.Resources.image_x_generic,
                                (eiv1, eiv2) =>
                                {
                                    kv.Key.Content = EditImageCallback(kv.Key.Content as Image);
                                    mDB.UpdateContent(kv.Key.Id, kv.Key.Content as Image);
                                    this.Controls.Clear();
                                    this.Invalidate();
                                }));
                            break;
                    }

                    toolbar.Items.Add(new ToolStripButton("Cancel", 
                        Properties.Resources.process_stop,
                        (cv1, cv2) => this.Controls.Clear()));
                    mPanel.Controls.Add(toolbar);
                    this.Controls.Add(mPanel);
                    break;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (sbMove)
            {
                Value = sbValue + (e.Y - sbDown.Y);
                if (Value  > MaxValue) Value = MaxValue;
                if (Value < 0) Value = 0;
                Invalidate();
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (this.Controls.Count > 0) return;
            int delta = this.Height / 15;
            Value += e.Delta > 0 ? -delta : delta;
            if (Value > MaxValue) Value = MaxValue;
            if (Value < 0) Value = 0;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            sbMove = false;
        }

        public Rectangle DrawTitle(Graphics graphics, Database.Entry e, Font font, int top, Brush color)
        {
            string no = "No." + e.Id.ToString();
            string now = e.Time.ToString();

            int secDiff = (int)DateTime.Now.Subtract(e.Time).TotalSeconds;
            if (secDiff < 60)
                now = "+" + secDiff.ToString() + "s " + now;
            else if (secDiff < 3600)
                now = "+" + (secDiff / 60).ToString() + "m" + (secDiff % 60).ToString() + "s " + now;
            else if (secDiff < 86400)
                now = "+" + (secDiff / 3600).ToString() + "h" + ((secDiff - secDiff / 3600 * 3600) / 60).ToString() + "m " + now;

            if (e.Hits > 1)
                now += " (" + e.Hits.ToString() + ")";

            now = e.Name + " " + now;

            var size = TextRenderer.MeasureText(no, font, new Size(0, 0), TextFormatFlags.SingleLine);
            var width = this.Width - ScrollbarWidth;
            graphics.FillRectangle(color, 0, top, width, size.Height);
            graphics.DrawString(no, new Font(font, FontStyle.Bold), Brushes.White, 0, top);
            graphics.DrawString(now, font, Brushes.White, size.Width + 5, top);

            Rectangle hotarea = default(Rectangle);
            if (!string.IsNullOrWhiteSpace(e.SourceUrl))
            {
                var urlTrueSize = TextRenderer.MeasureText(e.SourceUrl, font, new Size(0, 0), TextFormatFlags.SingleLine);
                var showFull = (width * 4 / 3 > urlTrueSize.Width);
                var url = showFull ? e.SourceUrl : Helper.GetHostFromUri(e.SourceUrl);
                var urlSize = showFull ? urlTrueSize : TextRenderer.MeasureText(url, font, new Size(0, 0));

                var left = width - urlSize.Width;
                if (left < 0) left = 0;

                graphics.FillRectangle(Brushes.Teal, left, top + size.Height, urlSize.Width, urlSize.Height);
                graphics.DrawString(url, font, Brushes.White, left, top + size.Height);
                hotarea = new Rectangle(width - urlSize.Width, size.Height, urlSize.Width, urlSize.Height);
            }

            if (e.Content is Image)
            {
                Size imageSize = (e.Content as Image).Size;
                string sizeText = imageSize.Width.ToString() + "x" + imageSize.Height.ToString();
                var sizeTrueSize = TextRenderer.MeasureText(sizeText, font, new Size(0, 0), TextFormatFlags.SingleLine);

                graphics.FillRectangle(Brushes.LightGray, 0, top + size.Height, sizeTrueSize.Width, sizeTrueSize.Height);
                graphics.DrawRectangle(Pens.Black, 0, top + size.Height, sizeTrueSize.Width, sizeTrueSize.Height);
                graphics.DrawString(sizeText, font, Brushes.Black, 0, top + size.Height);
            }

            return hotarea;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // System.Diagnostics.Debug.Print(e.ClipRectangle.ToString());
            var container = e.Graphics.BeginContainer();
            e.Graphics.Clip = new Region(e.ClipRectangle);
            e.Graphics.FillRectangle(Brushes.White, e.ClipRectangle);

            mHotarea.Clear();
            mUrlHotarea.Clear();
            this.Controls.Clear();

            int top = -(int)(Value * mK);
            foreach (Database.Entry datum in mData)
            {
                //e.Graphics.DrawString(datum.No.ToString(), Monospace, Brushes.Black, new Point(0, top));
                int drawTop = top + MonospaceSize.Height;
                int drawHeight = ImageEntryHeight - MonospaceSize.Height;
                if (drawTop >= 0 && drawTop <= this.Height)
                {
                    mHotarea[datum] = new Rectangle(0, drawTop, this.Width - ScrollbarWidth, drawHeight);
                    if (!string.IsNullOrWhiteSpace(datum.SourceUrl))
                        mUrlHotarea[datum] = new Rectangle(0, top + MonospaceSize.Height,
                            this.Width - ScrollbarWidth, MonospaceSize.Height);
                }
                switch (datum.Type)
                {
                    case Database.ContentType.Image:
                        Image img = datum.Content as Image;
                        double zoom = Helper.CalcFitZoom(img, this.Width, drawHeight);
                        e.Graphics.DrawImage(img, 0, drawTop, (int)(img.Width * zoom), (int)(img.Height * zoom));
                        break;
                    default:
                        int drawHeight2 = TextEntryHeight - MonospaceSize.Height;
                        string content = datum.Type == Database.ContentType.HTML ?
                            datum.Html:
                            datum.Content as string;
                        if (content.Length > 1024) content = content.Substring(0, 1024);
                        if (!content.Contains("\n") && !string.IsNullOrWhiteSpace(datum.SourceUrl))
                        {
                            // Prevent URL from overlapping the text
                            drawTop += MonospaceSize.Height;
                            drawHeight2 -= MonospaceSize.Height;
                        }
                        e.Graphics.DrawString(content, Monospace, Brushes.Black,
                            new Rectangle(0, drawTop, this.Width, drawHeight2));
                        break;
                }
                switch (datum.Type)
                {
                    case Database.ContentType.HTML:
                DrawTitle(e.Graphics, datum, Monospace, top, Brushes.DarkSlateGray);
                        break;
                    case Database.ContentType.RawText:
                DrawTitle(e.Graphics, datum, Monospace, top, Brushes.Peru);
                        break;
                    case Database.ContentType.Image:
                DrawTitle(e.Graphics, datum, Monospace, top, Brushes.PaleVioletRed);
                        break;
                }


                top += datum.Content is Image ? ImageEntryHeight : TextEntryHeight;
            }

            int diff = RealHeight - this.Height;
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(this.Width - ScrollbarWidth, 
                0, ScrollbarWidth, this.Height));
            if (diff > 0)
            {
                mK = (double)RealHeight / (double)this.Height;
                int scrollbarHeight = (int)(this.Height / mK);
                if (scrollbarHeight < MinimalScrollbarHeight)
                {
                    scrollbarHeight = MinimalScrollbarHeight;
                    mK = (double)this.Height / scrollbarHeight;
                }
                MaxValue = (int)(diff / mK);

                e.Graphics.FillRectangle(ScrollbarBrush, new Rectangle(this.Width - ScrollbarWidth, 
                    (int)(Value), ScrollbarWidth, scrollbarHeight));
            }
            else
            {
                MaxValue = 0;
            }

            e.Graphics.EndContainer(container);
        }
    }
}
