using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clipboarder
{
    class ImageViewer : PictureBox
    {
        private int mZoom = 100;

        private Point mOffset = default(Point);

        public int Step = 10;

        public ImageViewer() : base()
        {
            MouseWheel += (s, e) =>
            {
                if (Image == null) return;
                mZoom += Math.Sign(e.Delta) * Step;
                if (mZoom < 10) mZoom = 10;
                if (mZoom > 1600) mZoom = 1600;
                Invalidate();
            };

            Cursor = Cursors.SizeAll;

            bool grab = false;
            Point grabBegin = default(Point), oldOffset = mOffset;
            MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    CalcFitZoom();
                    mOffset = new Point(0, 0);
                    Invalidate();
                    return;
                }

                grab = true;
                oldOffset = mOffset;
                grabBegin = e.Location;
            };

            MouseMove += (s, e) =>
            {
                if (!grab) return;
                int dx = e.X - grabBegin.X, dy = e.Y - grabBegin.Y;
                mOffset = new Point(oldOffset.X + dx, oldOffset.Y + dy);
                Invalidate();
            };

            MouseUp += (s, e) =>
            {
                grab = false;
            };
        }

        public void CalcFitZoom()
        {
            if (Image == null) return;

            int w = Image.Width, h = Image.Height;
            int pw = this.Width, ph = this.Height;

            if (w <= pw && h <= ph) return;
            if (w > pw && h <= ph) mZoom = pw * 100 / w;
            if (w <= pw && h > ph) mZoom = ph * 100 / h;

            if (w > pw && h > ph)
            {
                mZoom = ph * 100 / h;
                if (w * mZoom / 100 > pw)
                    mZoom = pw * 100 / w;
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            var container = pe.Graphics.BeginContainer();
            int cx = this.Width / 2, cy = this.Height / 2;
            int w = Image.Width * mZoom / 100, h = Image.Height * mZoom / 100;
            int x = cx - w / 2 + mOffset.X, y = cy - h / 2 + mOffset.Y;

            pe.Graphics.Clip = new Region(new RectangleF(0, 0, this.Width, this.Height));
            pe.Graphics.DrawImage(Image, x, y, w, h);
            pe.Graphics.DrawRectangle(Pens.Black, x - 2, y - 2, w + 4, h + 4);
            pe.Graphics.EndContainer(container);
        }
    }
}
