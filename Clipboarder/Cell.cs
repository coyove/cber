using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clipboarder
{
    public struct Title
    {
        public int No;
        public DateTime Time;
        public int Hits;
        public Size Size;
    }

    class CellHelper
    {
        public static Padding CalcTitlePadding(Padding inheritedPadding, Font font)
        {
            var size = TextRenderer.MeasureText("A", font, new Size(0, 0));
            return new Padding(inheritedPadding.Left,
                              inheritedPadding.Top + size.Height,
                              inheritedPadding.Right,
                              inheritedPadding.Bottom);
        }

        public static void DrawTitle(Graphics graphics, Title title, Font font, Rectangle cellBounds, Brush color)
        {
            string no = "No." + title.No.ToString();
            string now = title.Time.ToString();

            int secDiff = (int)DateTime.Now.Subtract(title.Time).TotalSeconds;
            if (secDiff < 60)
                now = "+" + secDiff.ToString() + "s " + now;
            else if (secDiff < 3600)
                now = "+" + (secDiff / 60).ToString() + "m" + (secDiff % 60).ToString() + "s " + now;
            else if (secDiff < 86400)
                now = "+" + (secDiff / 60).ToString() + "h" + (secDiff % 60).ToString() + "m " + now;

            if (title.Hits > 1)
                now += " (" + title.Hits.ToString() + ")";
            if (title.Size.Width > 0 && title.Size.Height > 0)
                now += " [" + title.Size.Width.ToString() + "x" + title.Size.Height.ToString() + "]";

            var size = TextRenderer.MeasureText(no, font, new Size(0, 0));
            System.Drawing.Drawing2D.GraphicsContainer container = graphics.BeginContainer();
            graphics.FillRectangle(color, cellBounds.Left, cellBounds.Top, cellBounds.Width, size.Height);
            graphics.DrawString(no, new Font(font, FontStyle.Bold), Brushes.White, cellBounds.Left, cellBounds.Top);
            graphics.DrawString(now, font, Brushes.White, cellBounds.Left + size.Width + 5, cellBounds.Top);
            graphics.EndContainer(container);
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
                this.Style.Padding = CellHelper.CalcTitlePadding(InheritedStyle.Padding, InheritedStyle.Font);
            }
        }

        private string _Url;
        private Rectangle mUrlHotArea;
        public string Url
        {
            get
            {
                return _Url;
            }
            set
            {
                if (_Url == null && !string.IsNullOrWhiteSpace(value))
                {
                    this._Url = value;
                    this.Style.Padding = CellHelper.CalcTitlePadding(InheritedStyle.Padding, InheritedStyle.Font);
                }
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

            CellHelper.DrawTitle(graphics, Title, InheritedStyle.Font, cellBounds, Brushes.Peru);
            if (!string.IsNullOrWhiteSpace(Url))
            {
                var size = TextRenderer.MeasureText("A", InheritedStyle.Font, new Size(0, 0));
                System.Drawing.Drawing2D.GraphicsContainer container = graphics.BeginContainer();
                graphics.FillRectangle(Brushes.Teal, cellBounds.Left, cellBounds.Top + size.Height, cellBounds.Width, size.Height);
                graphics.DrawString(Url, InheritedStyle.Font, Brushes.White, cellBounds.Left, cellBounds.Top + size.Height);
                graphics.EndContainer(container);
                mUrlHotArea = new Rectangle(0, size.Height, cellBounds.Width, size.Height);
            }
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
                this.Style.Padding = CellHelper.CalcTitlePadding(InheritedStyle.Padding, InheritedStyle.Font);
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
