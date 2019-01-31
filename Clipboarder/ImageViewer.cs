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

            bool grab = false;
            Point grabBegin = default(Point);
            Point oldOffset = mOffset;
            MouseDown += (s, e) =>
            {
                grab = true;
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

        protected override void OnPaint(PaintEventArgs pe)
        {
            var container = pe.Graphics.BeginContainer();
            int cx = this.Width / 2, cy = this.Height / 2;
            int w = Image.Width * mZoom / 100, h = Image.Height * mZoom / 100;
            pe.Graphics.Clip = new System.Drawing.Region(new System.Drawing.RectangleF(0, 0, this.Width, this.Height));
            pe.Graphics.DrawImage(Image,
                cx - w / 2 + mOffset.X,
                cy - h / 2 + mOffset.Y,
                w, h);
            pe.Graphics.EndContainer(container);
        }
    }
}
