using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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

        private Dictionary<Database.Entry, Rectangle> mCopyHotarea = new Dictionary<Database.Entry, Rectangle>();

        private Dictionary<Database.Entry, Rectangle> mFavHotarea = new Dictionary<Database.Entry, Rectangle>();

        private struct EntryStatus {
            public Database.Entry Entry;
            public bool Hovered;
            public bool Clicked;
            public Rectangle Hotarea;
        }

        private EntryStatus mCurrentHoverEntry = new EntryStatus();

        private ToolStripDropDownButton mCodeMenu = new ToolStripDropDownButton();

        private Panel mPanel;

        private double mK;

        public Database mDB;

        public Func<Image, Image> EditImageCallback;

        public Action<Database.Entry> CopyCallback;

        public Action<Database.Entry> DeleteCallback;

        static int SmallEntryHeight = 200;
        static int BigEntryHeight = 300;
        static int SBW = 25;
        static int MinimalScrollbarHeight = 25;
        static Brush ScrollbarBrush = Brushes.Gray;
        static Brush LightLightGray = new SolidBrush(Color.FromArgb(0xee, 0xee, 0xee));
        static Brush BgBrush = new SolidBrush(Color.FromArgb(0xFF, 0xfC, 0xe3));
        static Font Monospace = new Font("Consolas", 12);
        static Size MonospaceSize = TextRenderer.MeasureText("A", Monospace);
        static ImageAttributes ButtonIA = new ImageAttributes();

        static EntryPanel()
        {
            ButtonIA.SetColorMatrix(new ColorMatrix(new float[][]
{
  new float[] {1, 0, 0, 0, 0},
  new float[] {0, 1, 0, 0, 0},
  new float[] {0, 0, 1, 0, 0},
  new float[] {0, 0, 0, 0.6f, 0},
  new float[] {0, 0, 0, 0, 1},
}
));
        }

        public EntryPanel() : base()
        {
            this.Padding = new Padding(0);
            this.DoubleBuffered = true;
            this.TabStop = false;

            ToolStripMenuItem plainText = new ToolStripMenuItem();
            plainText.CheckOnClick = true;
            plainText.Checked = true;
            plainText.Text = "Plain";
            plainText.Click += CodeHighlight_Click;
            mCodeMenu.DropDownItems.Add(plainText);
            foreach (string key in new string[] {
                "BAT", "Boo", "C++.NET", "C#", "Coco", "HTML", "JavaScript", "Java", "Patch", "PHP", "TeX", "VBNET", "XML",
            })
            {
                ToolStripMenuItem code = new ToolStripMenuItem();
                code.CheckOnClick = true;
                code.Text = key;
                code.Click += CodeHighlight_Click;
                mCodeMenu.DropDownItems.Add(code);
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
                    height += datum.IsBig ? BigEntryHeight : SmallEntryHeight;
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
            if (e.X >= this.Width - SBW)
            {
                sbDown = e.Location;
                sbValue = Value;
                sbMove = true;
                return;
            }

            foreach (var kv in mFavHotarea)
            {
                if (kv.Value.Contains(e.Location))
                {
                    kv.Key.IsFavorited = !kv.Key.IsFavorited;
                    mDB.Favorite(kv.Key.Id, kv.Key.IsFavorited);
                    Invalidate();
                    return;
                }
            }

            foreach (var kv in mUrlHotarea)
            {
                if (kv.Value.Contains(e.Location))
                {
                    Process.Start(kv.Key.SourceUrl);
                    return;
                }
            }

            if (mCurrentHoverEntry.Hotarea.Contains(e.Location))
            {
                mCurrentHoverEntry.Clicked = true;
                Invalidate();
                return;
            }

            this.Controls.Clear();
            foreach (var kv in mHotarea)
            {
                if (kv.Value.Contains(e.Location))
                {
                    mPanel = new Panel();
                    mPanel.Location = kv.Value.Location;
                    mPanel.Size = kv.Value.Size;
                    mPanel.TabStop = false;

                    var toolbar = new ToolStrip();
                    toolbar.Dock = DockStyle.Top;
                    toolbar.TabStop = false;

                    switch (kv.Key.Type)
                    {
                        case Database.ContentType.RawText:
                            var editor = new ICSharpCode.TextEditor.TextEditorControl();
                            editor.Text = kv.Key.Content as string;
                            editor.Dock = DockStyle.Fill;
                            editor.TabStop = false;
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
                            browser.TabStop = false;
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

                    toolbar.Items.Add(new ToolStripButton("Delete", 
                        Properties.Resources.mail_mark_not_junk,
                        (cv1, cv2) => DeleteCallback(kv.Key)));

                    toolbar.Items.Add(new ToolStripButton("Close",
                        Properties.Resources.process_stop,
                        (cv1, cv2) => this.Controls.Clear())
                    {
                        Alignment = ToolStripItemAlignment.Right,
                    });
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
                LimitValue();
                Invalidate();
            }

            mCurrentHoverEntry.Entry = null;
            if (this.Controls.Count == 0)
            {
                foreach (var kv in mCopyHotarea)
                {
                    if (e.Location.Y >= kv.Value.Y && e.Location.Y <= kv.Value.Y + kv.Value.Height)
                    {
                        mCurrentHoverEntry.Hovered = kv.Value.Contains(e.Location);
                        mCurrentHoverEntry.Hotarea = kv.Value;
                        mCurrentHoverEntry.Entry = kv.Key;
                        Invalidate(new Region(new RectangleF(0, 0, SBW, this.Height)));
                        return;
                    }
                }
            }
        }

        public void TryScrollTo(int value)
        {
            Invalidate();
            Value = value;
            LimitValue();
            Invalidate();
        }

        private void LimitValue()
        {
            if (Value > MaxValue) Value = MaxValue;
            if (Value < 0) Value = 0;
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

            if (mCurrentHoverEntry.Hotarea.Contains(e.Location))
            {
                CopyCallback(mCurrentHoverEntry.Entry);
                mCurrentHoverEntry.Clicked = false;
                Invalidate();
                return;
            }
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

            var size = TextRenderer.MeasureText(no, font, new Size(0, 0), TextFormatFlags.SingleLine);
            var width = this.Width - SBW - SBW;
            graphics.FillRectangle(color, SBW, top, width, size.Height);
            graphics.DrawString(no, new Font(font, FontStyle.Bold), Brushes.White, SBW, top);
            graphics.DrawString(now, font, Brushes.White,
                new Rectangle(SBW + size.Width + 5, top, width - 30 - size.Width, size.Height));

            Rectangle hotarea = default(Rectangle);
            if (!string.IsNullOrWhiteSpace(e.SourceUrl))
            {
                var urlTrueSize = TextRenderer.MeasureText(e.SourceUrl, font, new Size(0, 0), TextFormatFlags.SingleLine);
                var showFull = (width * 4 / 3 > urlTrueSize.Width);
                var url = showFull ? e.SourceUrl : Helper.GetHostFromUri(e.SourceUrl);
                var urlSize = showFull ? urlTrueSize : TextRenderer.MeasureText(url, font, new Size(0, 0));

                var left = this.Width - SBW - urlSize.Width;
                if (left < SBW) left = SBW;

                graphics.FillRectangle(Brushes.Teal, left, top + size.Height, urlSize.Width, urlSize.Height);
                graphics.DrawString(url, font, Brushes.White, left, top + size.Height);
                hotarea = new Rectangle(width - urlSize.Width, size.Height, urlSize.Width, urlSize.Height);
            }

            if (e.Content is Image)
            {
                Size imageSize = (e.Content as Image).Size;
                string sizeText = imageSize.Width.ToString() + "x" + imageSize.Height.ToString();
                var sizeTrueSize = TextRenderer.MeasureText(sizeText, font, new Size(0, 0), TextFormatFlags.SingleLine);

                graphics.FillRectangle(Brushes.LightGray, SBW, top + size.Height, sizeTrueSize.Width, sizeTrueSize.Height);
                graphics.DrawRectangle(Pens.Black, SBW, top + size.Height, sizeTrueSize.Width, sizeTrueSize.Height);
                graphics.DrawString(sizeText, font, Brushes.Black, SBW, top + size.Height);
            }

            graphics.DrawImage(Properties.Resources.shadow, SBW, top + size.Height, this.Width, 16);

            Rectangle favArea = new Rectangle(this.Width - SBW - 25, top + (size.Height - 22) / 2, 22, 22);
            graphics.DrawImage(e.IsFavorited ?
                Properties.Resources.favorite :
                Properties.Resources.unfavorite, favArea);
            mFavHotarea[e] = favArea;

            return hotarea;
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var container = e.Graphics.BeginContainer();
            e.Graphics.Clip = new Region(e.ClipRectangle);
            //e.Graphics.FillRectangle(Brushes.White, e.ClipRectangle);

            mHotarea.Clear();
            mUrlHotarea.Clear();
            mCopyHotarea.Clear();
            mFavHotarea.Clear();
            this.Controls.Clear();

            int top = -(int)(Value * mK);
            bool zebra = false;
            foreach (Database.Entry datum in mData)
            {
                zebra = !zebra;
                int entryHeight = datum.IsBig ? BigEntryHeight : SmallEntryHeight;
                int drawTop = top + MonospaceSize.Height;
                int drawHeight = entryHeight - MonospaceSize.Height;
                e.Graphics.FillRectangle(BgBrush, SBW, top, this.Width, entryHeight);

                if (drawTop + entryHeight >= 0 && drawTop <= this.Height)
                {
                    mHotarea[datum] = new Rectangle(SBW, drawTop, this.Width - SBW - SBW, drawHeight);
                    if (!string.IsNullOrWhiteSpace(datum.SourceUrl))
                        mUrlHotarea[datum] = new Rectangle(SBW, top + MonospaceSize.Height,
                            this.Width - SBW - SBW, MonospaceSize.Height);
                }

                switch (datum.Type)
                {
                    case Database.ContentType.Image:
                        Image img = datum.Content as Image;
                        double zoom = Helper.CalcFitZoom(img, this.Width, drawHeight);
                        int newWidth = (int)(img.Width * zoom), newHeight = (int)(img.Height * zoom);
                        e.Graphics.DrawImage(img,
                            (this.Width - SBW - SBW - newWidth) / 2 + SBW,
                            drawTop + (entryHeight - newHeight) / 2,
                            newWidth, newHeight);
                        break;
                    default:
                        int drawHeight2 = entryHeight - MonospaceSize.Height;
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
                            new Rectangle(5 + SBW, drawTop + 5, this.Width - SBW - 10, drawHeight2 - 10));
                        break;
                }
                switch (datum.Type)
                {
                    case Database.ContentType.HTML:
                DrawTitle(e.Graphics, datum, Monospace, top, Brushes.DarkSlateGray);
                        break;
                    case Database.ContentType.RawText:
                DrawTitle(e.Graphics, datum, Monospace, top, Brushes.Firebrick);
                        break;
                    case Database.ContentType.Image:
                DrawTitle(e.Graphics, datum, Monospace, top, Brushes.PaleVioletRed);
                        break;
                }

                Rectangle leftSideBar = new Rectangle(0, top, SBW, entryHeight);
                if (mCurrentHoverEntry.Entry == datum)
                {
                    e.Graphics.FillRectangle(Brushes.LightSlateGray, leftSideBar);
                    if (mCurrentHoverEntry.Clicked)
                        e.Graphics.FillRectangle(Brushes.DarkSlateGray, leftSideBar);

                    e.Graphics.DrawString("C", Monospace, Brushes.White, leftSideBar,
                        new StringFormat()
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center,
                        });
                }
                else
                {
                    e.Graphics.FillRectangle(zebra ? Brushes.LightGray : LightLightGray, leftSideBar);
                }
                mCopyHotarea[datum] = new Rectangle(0, top, SBW, entryHeight);

                top += entryHeight;
            }

            if (mData.Count == 0)
            {
                e.Graphics.DrawString(Properties.Resources.EmptyResults,
                    Monospace, Brushes.Black,
                    new Rectangle(SBW, 0, this.Width - SBW - SBW, this.Height), new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center,
                    });
                e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(0, 0, SBW, this.Height));
            }

            int diff = RealHeight - this.Height;
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(this.Width - SBW, 
                0, SBW, this.Height));
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
                LimitValue();

                e.Graphics.FillRectangle(ScrollbarBrush, new Rectangle(this.Width - SBW, 
                    (int)(Value), SBW, scrollbarHeight));
            }
            else
            {
                MaxValue = 0;
            }

            e.Graphics.EndContainer(container);
        }
    }
}
