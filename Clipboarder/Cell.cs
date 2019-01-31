﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clipboarder
{
    public class Title
    {
        public int No;
        public DateTime Time;
        public int Hits;
        public Size Size;
        public string Url;
    }

    class CellHelper
    {
        public static Padding CalcTitlePadding(Title title, Padding inheritedPadding, Font font)
        {
            var size = TextRenderer.MeasureText("A", font, new Size(0, 0));
            return new Padding(inheritedPadding.Left,
                              inheritedPadding.Top + size.Height * (string.IsNullOrWhiteSpace(title?.Url) ? 1 : 2),
                              inheritedPadding.Right,
                              inheritedPadding.Bottom);
        }

        public static Rectangle DrawTitle(Graphics graphics, Title title, Font font, Rectangle cellBounds, Brush color)
        {
            string no = "No." + title.No.ToString();
            string now = title.Time.ToString();

            int secDiff = (int)DateTime.Now.Subtract(title.Time).TotalSeconds;
            if (secDiff < 60)
                now = "+" + secDiff.ToString() + "s " + now;
            else if (secDiff < 3600)
                now = "+" + (secDiff / 60).ToString() + "m" + (secDiff % 60).ToString() + "s " + now;
            else if (secDiff < 86400)
                now = "+" + (secDiff / 3600).ToString() + "h" + ((secDiff - secDiff / 3600 * 3600) / 60).ToString() + "m " + now;

            if (title.Hits > 1)
                now += " (" + title.Hits.ToString() + ")";
            if (title.Size.Width > 0 && title.Size.Height > 0)
                now += " [" + title.Size.Width.ToString() + "x" + title.Size.Height.ToString() + "]";

            System.Drawing.Drawing2D.GraphicsContainer container = graphics.BeginContainer();
            var size = TextRenderer.MeasureText(no, font, new Size(0, 0));
            graphics.FillRectangle(color, cellBounds.Left, cellBounds.Top, cellBounds.Width, size.Height);
            graphics.DrawString(no, new Font(font, FontStyle.Bold), Brushes.White, cellBounds.Left, cellBounds.Top);
            graphics.DrawString(now, font, Brushes.White, cellBounds.Left + size.Width + 5, cellBounds.Top);

            Rectangle hotarea = default(Rectangle);
            if (!string.IsNullOrWhiteSpace(title.Url))
            {
                var url = Helper.GetHostFromUri(title.Url);
                var urlSize = TextRenderer.MeasureText(url, font, new Size(0, 0));
                var left = cellBounds.Left + cellBounds.Width - urlSize.Width;
                if (left < cellBounds.Left) left = cellBounds.Left;

                graphics.FillRectangle(Brushes.Teal, left, cellBounds.Top + size.Height, urlSize.Width, urlSize.Height);
                graphics.DrawString(url, font, Brushes.White, left, cellBounds.Top + size.Height);
                hotarea = new Rectangle(cellBounds.Width - urlSize.Width, size.Height, urlSize.Width, urlSize.Height);
            }

            graphics.EndContainer(container);
            return hotarea;
        }
    }

    public class TextTitleCell : DataGridViewTextBoxCell
    {
        public override object Clone()
        {
            TextTitleCell c = base.Clone() as TextTitleCell;
            c.Title = Title;
            return c;
        }

        private Title _Title;
        public Title Title
        {
            get
            {
                return _Title;
            }
            set
            {
                this._Title = value;
                this.Style.Padding = CellHelper.CalcTitlePadding(value, InheritedStyle.Padding, InheritedStyle.Font);
            }
        }

        private Rectangle mUrlHotArea;

        protected override void Paint(Graphics graphics, Rectangle clipBounds,
        Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
        object value, object formattedValue, string errorText,
        DataGridViewCellStyle cellStyle,
        DataGridViewAdvancedBorderStyle advancedBorderStyle,
        DataGridViewPaintParts paintParts)
        {
            // Paint the base content
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
               value, formattedValue, errorText, cellStyle,
               advancedBorderStyle, paintParts);

            mUrlHotArea = CellHelper.DrawTitle(graphics, Title, InheritedStyle.Font, cellBounds, Brushes.Peru);
        }

        protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (mUrlHotArea == null) return;
            if (mUrlHotArea.Contains(e.Location))
            {
                Cursor.Current = Cursors.Hand;
            }
        }
    }

    public class ImageTitleCell : DataGridViewImageCell
    {
        public ImageTitleCell() : base()
        {
            this.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        public override object Clone()
        {
            ImageTitleCell c = base.Clone() as ImageTitleCell;
            c.Title = Title;
            return c;
        }

        private Title _Title;
        public Title Title
        {
            get
            {
                return _Title;
            }
            set
            {
                this._Title = value;
                this.Style.Padding = CellHelper.CalcTitlePadding(null, InheritedStyle.Padding, InheritedStyle.Font);
            }
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds,
        Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
        object value, object formattedValue, string errorText,
        DataGridViewCellStyle cellStyle,
        DataGridViewAdvancedBorderStyle advancedBorderStyle,
        DataGridViewPaintParts paintParts)
        {
            // Paint the base content
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
               value, formattedValue, errorText, cellStyle,
               advancedBorderStyle, paintParts);

            CellHelper.DrawTitle(graphics, Title, InheritedStyle.Font, cellBounds, Brushes.PaleVioletRed);
        }
    }
}
