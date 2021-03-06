﻿using System;
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
using System.Threading;
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

        private Rectangle mNextPageHotarea;

        private struct EntryStatus {
            public Database.Entry Entry;
            public bool Hovered;
            public bool Clicked;
            public bool NextPageHovered;
            public Rectangle Hotarea;

            public void Reset()
            {
                Hovered = Clicked = NextPageHovered = false;
                Entry = null;
            }
        }

        private EntryStatus mCurrentHoverEntry = new EntryStatus();

        private ContextMenu mCodeMenu = new ContextMenu();

        private Panel mPanel;

        private ImageList mImageList;

        private double mK;

        public Database mDB;

        public Func<Image, Image> EditImageCallback;

        public Action<Database.Entry> CopyCallback;

        public Action<Database.Entry> DeleteCallback;

        public Action NextPageCallbak;

        static int StdEntryHeight = 150;
        static int SBW = 15;
        static int MinimalScrollbarHeight = 25;
        static Brush ScrollbarBrush = Brushes.Gray;
        static Brush DarkDarkGray = new SolidBrush(Color.FromArgb(0x40, 0x40, 0x40));
        static Brush LightLightGray = new SolidBrush(Color.FromArgb(0xee, 0xee, 0xee));
        static Brush BgBrush = new SolidBrush(Color.FromArgb(0xFF, 0xfC, 0xe3));
        static Font Monospace = new Font("Consolas", 12);
        static Font MonospaceSmall = new Font("Consolas", 10);
        static Size MonospaceSize = TextRenderer.MeasureText("A", Monospace);
        static StringFormat CenterTextFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        public EntryPanel() : base()
        {
            this.Padding = new Padding(0);
            this.DoubleBuffered = true;
            this.TabStop = false;
            this.ResizeRedraw = true;

            foreach (string key in new string[] {
                "Default", "BAT", "Boo", "C++.NET", "C#", "Coco", "HTML", "JavaScript", "Java", "Patch", "PHP", "TeX", "VBNET", "XML",
            })
            {
                MenuItem code = new MenuItem();
                code.Text = key;
                code.Click += CodeHighlight_Click;
                mCodeMenu.MenuItems.Add(code);
            }

            mImageList = new ImageList();
            mImageList.TransparentColor = Color.FromArgb(255, 0, 255);
            mImageList.Images.Add("close", Properties.Resources.Close_6519_24);
            mImageList.Images.Add("delete", Properties.Resources.Delete);
            mImageList.Images.Add("image", Properties.Resources.InsertPicture);
            mImageList.Images.Add("html", Properties.Resources.VSProject_html);
            mImageList.Images.Add("save", Properties.Resources.Save);
        }

        private void CodeHighlight_Click(object sender, EventArgs e)
        {
            foreach (var item in mCodeMenu.MenuItems)
                (item as MenuItem).Checked = false;

            var m = (sender as MenuItem);
            m.Checked = true;

            foreach (var ctrl in mPanel.Controls)
            {
                if (ctrl is ICSharpCode.TextEditor.TextEditorControl)
                {
                    (ctrl as ICSharpCode.TextEditor.TextEditorControl).SetHighlighting(m.Text);
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
                    height += (int)(datum.IsBig * StdEntryHeight);
                }
                height += MonospaceSize.Height * 2;
                return height;
            }
        }

        public int Page;

        public int TotalPages;

        public void Add(Database.Entry datum)
        {
            mData.Add(datum);
        }

        public void Clear()
        {
            Value = 0;
            mData.Clear();
            ClearControls();
            Invalidate();
        }

        private void ClearControls()
        {
            for (int i = this.Controls.Count - 1; i >= 0; i--)
                this.Controls[i].Dispose();
            this.Controls.Clear();
        }

        public int Value = 0;

        public int MaxValue { get; private set; }

        private Point sbDown = default(Point);
        private int sbValue = 0;
        private bool sbMove = false;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this.Controls.Count > 0)
            {
                ClearControls();
                this.Refresh();
            }

            if (mNextPageHotarea.Contains(e.Location))
            {
                NextPageCallbak();
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

            if (e.X >= this.Width - SBW * 2)
            {
                sbDown = e.Location;
                sbValue = Value;
                sbMove = true;
                return;
            }

            if (mCurrentHoverEntry.Hotarea.Contains(e.Location))
            {
                mCurrentHoverEntry.Clicked = true;
                Invalidate();
                return;
            }

            foreach (var kv in mUrlHotarea)
            {
                if (kv.Value.Contains(e.Location))
                {
                    try
                    {
                        Process.Start(kv.Key.SourceUrl);
                        return;
                    }
                    catch (Exception) { }
                }
            }

            ClearControls();
            foreach (var kv in mHotarea)
            {
                if (kv.Value.Contains(e.Location))
                {
                    mPanel = new Panel();
                    mPanel.Location = kv.Value.Location;
                    mPanel.Size = kv.Value.Size;
                    mPanel.TabStop = false;

                    var toolbar = new ToolBar();
                    toolbar.Dock = DockStyle.Top;
                    toolbar.TextAlign = ToolBarTextAlign.Right;
                    toolbar.ImageList = mImageList;

                    switch (kv.Key.Type)
                    {
                        case Database.ContentType.RawText:
                            if (Properties.Settings.Default.UsePlainTextBox)
                            {
                                var editor = new RichTextBox();
                                editor.KeyDown += (s, re) =>
                                {
                                    if (re.Control && re.KeyCode == Keys.V)
                                    {
                                        editor.Paste(DataFormats.GetFormat(DataFormats.UnicodeText));
                                        re.Handled = true;
                                    }
                                };
                                editor.Dock = DockStyle.Fill;
                                editor.Font = Monospace;
                                editor.BorderStyle = BorderStyle.None;
                                editor.Text = kv.Key.Content as string;
                                toolbar.Tag = editor;
                                mPanel.Controls.Add(editor);
                            }
                            else
                            {
                                var editor = new ICSharpCode.TextEditor.TextEditorControl();
                                editor.Text = kv.Key.Content as string;
                                editor.Dock = DockStyle.Fill;
                                editor.ContextMenu = mCodeMenu;
                                mPanel.Controls.Add(editor);
                                toolbar.Tag = editor;
                                foreach (MenuItem m in mCodeMenu.MenuItems)
                                    if (m.Checked) editor.SetHighlighting(m.Text);
                            }
                            toolbar.Buttons.Add(new ToolBarButton("Save") { ImageKey = "save", Tag = "save" });
                            break;
                        case Database.ContentType.HTML:
                            var browser = new WebBrowser();
                            browser.Dock = DockStyle.Fill;
                            browser.DocumentText = Helper.ExtractHTMLFromClipboard(kv.Key.Content.ToString());
                            browser.Navigating += (v1, v2) => { v2.Cancel = true; };
                            browser.ScriptErrorsSuppressed = true;
                            browser.TabStop = false;
                            mPanel.Controls.Add(browser);
                            toolbar.Buttons.Add(new ToolBarButton("HTML to Text") { ImageKey = "html", Tag = "htt" });
                            break;
                        case Database.ContentType.Image:
                            var viewer = new BasicControls();
                            viewer.Dock = DockStyle.Fill;
                            viewer.Image = kv.Key.Content as Image;
                            mPanel.Controls.Add(viewer);
                            viewer.CalcFitZoom();
                            toolbar.Buttons.Add(new ToolBarButton("Edit") { ImageKey = "image", Tag = "editimage" });
                            break;
                    }

                    toolbar.Buttons.Add(new ToolBarButton("Delete") { ImageKey = "delete", Tag = "delete" });
                    toolbar.Buttons.Add(new ToolBarButton("Close") { ImageKey = "close", Tag = "close" });
                    toolbar.ButtonClick += (s, ev) =>
                    {
                        switch (ev.Button.Tag.ToString())
                        {
                            case "close":
                                ClearControls();
                                Invalidate();
                                break;
                            case "delete":
                                DeleteCallback(kv.Key);
                                break;
                            case "editimage":
                                kv.Key.Content = EditImageCallback(kv.Key.Content as Image);
                                mDB.UpdateContent(kv.Key.Id, kv.Key.Content as Image);
                                ClearControls();
                                Invalidate();
                                break;
                            case "htt":
                                mDB.HTMLToText(kv.Key.Id);
                                kv.Key.Type = Database.ContentType.RawText;
                                kv.Key.Content = kv.Key.Html;
                                ClearControls();
                                this.Invalidate();
                                break;
                            case "save":
                                var editor = toolbar.Tag as ICSharpCode.TextEditor.TextEditorControl;
                                var content = editor != null ? editor.Text : (toolbar.Tag as RichTextBox).Text;
                                mDB.UpdateContent(kv.Key.Id, content);
                                kv.Key.Content = content;
                                ClearControls();
                                this.Invalidate();
                                break;
                        }
                    };
                    //mPanel.Controls.Add(toolbar);
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

            mCurrentHoverEntry.Reset();
            if (mNextPageHotarea.Contains(e.Location))
            {
                mCurrentHoverEntry.NextPageHovered = true;
            }
            Invalidate(mNextPageHotarea);

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
            ClearControls();
            Value = value;
            LimitValue();
            Invalidate();
        }

        private void LimitValue()
        {
            if (Value > MaxValue) Value = MaxValue;
            if (Value < 0) Value = 0;
        }

        public void ScrollBit(bool down)
        {
            if (this.Controls.Count > 0) return;
            int delta = this.Height / 15;
            Value += down ? delta : -delta;
            if (Value > MaxValue) Value = MaxValue;
            if (Value < 0) Value = 0;
            Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            ScrollBit(e.Delta <= 0);
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

        public bool TryShortcut(Keys key)
        {
            if (this.Controls.Count > 0) return false;
            int i = Properties.Settings.Default.PanelShortcutsMap.IndexOf(key.ToString());
            if (i < mData.Count && i >= 0)
            {
                mCurrentHoverEntry.Entry = mData[i];
                mCurrentHoverEntry.Hovered = mCurrentHoverEntry.Clicked = true;
                CopyCallback(mData[i]);
                Refresh();
                new Thread(() =>
                {
                    Thread.Sleep(500);
                    mCurrentHoverEntry.Reset();
                    this.Invoke(new Action(() => Refresh()));
                }).Start();
                return true;
            }
            return false;
        }

        public Rectangle DrawTitle(int index, Graphics graphics, Database.Entry e, Font font, int top, Brush color)
        {
            string now = e.Time.ToString();

            int secDiff = (int)DateTime.Now.Subtract(e.Time).TotalSeconds;
            if (secDiff < 60)
                now = secDiff.ToString() + "s " + now;
            else if (secDiff < 3600)
                now = (secDiff / 60).ToString() + "m" + (secDiff % 60).ToString() + "s " + now;
            else if (secDiff < 86400)
                now = (secDiff / 3600).ToString() + "h" + ((secDiff - secDiff / 3600 * 3600) / 60).ToString() + "m " + now;

            if (e.Hits > 1)
                now += " (" + e.Hits.ToString() + ")";

            now = e.Id.ToString() + ". " + now;
            var width = this.Width - SBW - SBW;
            var height = MonospaceSize.Height;

            graphics.FillRectangle(color, new Rectangle(SBW, top, width, height));

            if (index < Properties.Settings.Default.PanelShortcutsMap.Count)
            {
                graphics.DrawString(now, font, Brushes.White, new Rectangle(SBW + MonospaceSize.Width + 5, top, width - 40, height));
                graphics.FillRectangle(DarkDarkGray, new Rectangle(SBW, top, MonospaceSize.Width, height));
                graphics.FillPolygon(DarkDarkGray, new PointF[] {
                    new PointF(SBW + MonospaceSize.Width, top),
                    new PointF(SBW + MonospaceSize.Width + 4, top + height / 2),
                    new PointF(SBW + MonospaceSize.Width, top + height),
                    new PointF(SBW + MonospaceSize.Width - 4, top + height / 2),
                });

                string scKey = Properties.Settings.Default.PanelShortcutsMap[index];
                graphics.DrawString(scKey.Length > 1 ? scKey.Last().ToString() : scKey,
                    font, Brushes.White, new Rectangle(SBW, top, MonospaceSize.Width, height), CenterTextFormat);
            }
            else
            {
                graphics.DrawString(now, font, Brushes.White, new Rectangle(SBW, top, width - 30, height));
            }

            Rectangle hotarea = default(Rectangle);
            if (!string.IsNullOrWhiteSpace(e.SourceUrl))
            {
                var urlTrueSize = TextRenderer.MeasureText(e.SourceUrl, font, new Size(0, 0), TextFormatFlags.SingleLine);
                var showFull = (width * 4 / 3 > urlTrueSize.Width);
                var url = showFull ? e.SourceUrl : Helper.GetHostFromUri(e.SourceUrl);
                var urlSize = showFull ? urlTrueSize : TextRenderer.MeasureText(url, font, new Size(0, 0));

                var left = this.Width - SBW - urlSize.Width;
                if (left < SBW) left = SBW;

                graphics.FillRectangle(Brushes.Teal, left, top + height, urlSize.Width, urlSize.Height);
                graphics.DrawString(url, font, Brushes.White, left, top + height);
                hotarea = new Rectangle(width - urlSize.Width, height, urlSize.Width, urlSize.Height);
            }

            if (e.Content is Image)
            {
                Size imageSize = (e.Content as Image).Size;
                string sizeText = string.Format("{0}x{1} {2:0.00}MB", imageSize.Width, imageSize.Height, (double)e.Size / 1024 / 1024);
                var sizeTrueSize = TextRenderer.MeasureText(sizeText, font, new Size(0, 0), TextFormatFlags.SingleLine);

                graphics.FillRectangle(Brushes.LightGray, SBW, top + height, sizeTrueSize.Width, sizeTrueSize.Height);
                graphics.DrawRectangle(Pens.Black, SBW, top + height, sizeTrueSize.Width, sizeTrueSize.Height);
                graphics.DrawString(sizeText, font, Brushes.Black, SBW, top + height);
            }

            graphics.DrawImage(Properties.Resources.shadow, SBW, top + height, this.Width, 16);

            int favSize = MonospaceSize.Height - 4;
            Rectangle favArea = new Rectangle(this.Width - SBW - favSize - 3, top + (height - favSize) / 2, favSize, favSize);
            graphics.DrawImage(e.IsFavorited ?
                Properties.Resources.favorite :
                Properties.Resources.unfavorite, favArea);
            mFavHotarea[e] = favArea;

            return hotarea;
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            ClearControls();
            Refresh();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.Select();
            this.Focus();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var container = e.Graphics.BeginContainer();
            e.Graphics.Clip = new Region(e.ClipRectangle);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            mHotarea.Clear();
            mUrlHotarea.Clear();
            mCopyHotarea.Clear();
            mFavHotarea.Clear();

            int top = -(int)(Value * mK), i = -1;
            bool zebra = false;
            foreach (Database.Entry datum in mData)
            {
                zebra = !zebra;
                i++;
                int entryHeight = (int)(datum.IsBig * StdEntryHeight);
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

                #region Draw entry content
                switch (datum.Type)
                {
                    case Database.ContentType.Image:
                        Image img = datum.Content as Image;
                        double zoom = Helper.CalcFitZoom(img, this.Width, drawHeight);
                        int newWidth = (int)(img.Width * zoom), newHeight = (int)(img.Height * zoom);
                        e.Graphics.DrawImage(img,
                            (this.Width - SBW - SBW - newWidth) / 2 + SBW,
                            drawTop + (entryHeight - MonospaceSize.Height - newHeight) / 2,
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
                            new Rectangle(5 + SBW, drawTop + 5, this.Width - SBW - SBW - 10, drawHeight2 - 10));
                        break;
                }
                #endregion

                #region Draw entry title
                switch (datum.Type)
                {
                    case Database.ContentType.HTML:
                        DrawTitle(i, e.Graphics, datum, Monospace, top, Brushes.Peru);
                        break;
                    case Database.ContentType.RawText:
                        DrawTitle(i, e.Graphics, datum, Monospace, top, Brushes.Firebrick);
                        break;
                    case Database.ContentType.Image:
                        DrawTitle(i, e.Graphics, datum, Monospace, top, Brushes.PaleVioletRed);
                        break;
                }
                #endregion

                #region Draw the copy button at left side
                Rectangle leftSideBar = new Rectangle(0, top, SBW, entryHeight);
                if (mCurrentHoverEntry.Entry == datum)
                {
                    e.Graphics.FillRectangle(Brushes.LightSlateGray, leftSideBar);
                    if (mCurrentHoverEntry.Clicked)
                        e.Graphics.FillRectangle(Brushes.DarkSlateGray, leftSideBar);

                    e.Graphics.DrawString("C", Monospace, Brushes.White, leftSideBar, CenterTextFormat);
                }
                else
                {
                    e.Graphics.FillRectangle(zebra ? Brushes.LightGray : LightLightGray, leftSideBar);
                }
                mCopyHotarea[datum] = new Rectangle(0, top, SBW * 3, entryHeight);
                #endregion

                top += entryHeight;
            }

            #region Draw scroll bar
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
            #endregion

            #region Draw next page marker
            mNextPageHotarea = new Rectangle(SBW, top, this.Width - SBW - SBW, MonospaceSize.Height * 2);
            e.Graphics.DrawString(TotalPages == 0 ? "- / -" : string.Format("{0} / {1}", Page, TotalPages), 
                MonospaceSmall, Brushes.Black, mNextPageHotarea, CenterTextFormat);
            if (Page != TotalPages)
            {
                int top2 = top + MonospaceSize.Height * 3 / 2;
                PointF[] triangle = new PointF[] {
                new PointF(this.Width / 2 - 5, top2),
                new PointF(this.Width / 2,  top2 + 5),
                new PointF(this.Width / 2 + 5,  top2)
                };
                if (mCurrentHoverEntry.NextPageHovered)
                {
                    e.Graphics.FillEllipse(Brushes.DarkSlateGray,
                        new Rectangle(this.Width / 2 - 8, top2 + 1 - 8, 16, 16));
                    e.Graphics.FillPolygon(Brushes.White, triangle);
                }
                else
                {
                    e.Graphics.FillPolygon(Brushes.Black, triangle);
                }
            }
            #endregion

            if (top < this.Height)
            {
                e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(0, top, SBW, this.Height));
            }

            e.Graphics.EndContainer(container);
        }
    }
}
