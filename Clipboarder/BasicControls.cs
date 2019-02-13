using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clipboarder
{
    class BasicControls : Panel
    {
        private int mZoom = 100;

        private Point mOffset = default(Point);

        public int Step = 10;

        public Image Image;

        public BasicControls() : base()
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
            DoubleBuffered = true;
            TabStop = false;

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

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.Select();
            this.Focus();
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

    class Shortcut : Panel
    {
        private CheckBox cbShift;
        private CheckBox cbAlt;
        private CheckBox cbCtrl;
        private ComboBox box;

        public Shortcut() : base()
        {
            TableLayoutPanel table = new TableLayoutPanel();
            table.Dock = DockStyle.Fill;
            table.ColumnCount = 4;
            table.RowCount = 1;
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));

            box = new ComboBox();
            this.Controls.Add(box);

            cbCtrl = new CheckBox();
            cbCtrl.Text = "Ctrl";
            cbCtrl.AutoSize = true;
            // this.Controls.Add(cbCtrl);
            table.Controls.Add(cbCtrl, 0, 0);

            cbAlt = new CheckBox();
            cbAlt.Text = "Alt";
            cbAlt.AutoSize = true;
            //this.Controls.Add(cbAlt);
            table.Controls.Add(cbAlt, 1, 0);

            cbShift = new CheckBox();
            cbShift.Text = "Shift";
            cbShift.AutoSize = true;
            //this.Controls.Add(cbShift);
            table.Controls.Add(cbShift, 2, 0);

            box.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            foreach (string name in Enum.GetNames(typeof(Keys)))
            {
                box.Items.Add(name);
            }
            table.Controls.Add(box, 3, 0);

            this.Controls.Add(table);
        }

        public override string Text
        {
            get
            {
                if (string.IsNullOrWhiteSpace(box.Text)) return "";
                List<string> res = new List<string>(4);
                if (cbCtrl.Checked) res.Add("Ctrl");
                if (cbAlt.Checked) res.Add("Alt");
                if (cbShift.Checked) res.Add("Shift");

                string key = null;
                try { key = ((Keys)Enum.Parse(typeof(Keys), box.Text)).ToString(); } catch (Exception) { }
                if (string.IsNullOrWhiteSpace(key)) return "";
                res.Add(key);
                return string.Join("+", res);
            }
            set
            {
                foreach (string key in value.Split(new char[] { '+' }))
                {
                    switch (key.ToLower())
                    {
                        case "alt":
                            cbAlt.Checked = true;
                            break;
                        case "ctrl":
                            cbCtrl.Checked = true;
                            break;
                        case "shift":
                            cbShift.Checked = true;
                            break;
                        default:
                            try { box.Text = ((Keys)Enum.Parse(typeof(Keys), key)).ToString(); } catch (Exception) { }
                            break;
                    }
                }
            }
        }
    }


    class PathLabel : Label
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            TextRenderer.DrawText(e.Graphics, this.Text, this.Font,
                new Rectangle(0, 0, this.Width, this.Height),
                this.ForeColor, this.BackColor,
                TextFormatFlags.Right | TextFormatFlags.PathEllipsis | TextFormatFlags.VerticalCenter);
        }
    }
}
