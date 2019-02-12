using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clipboarder
{
    using resx = Properties.Resources;

    public partial class FormCB : Form
    {
        private void InitUi()
        {
            mBar = new ToolBar();
            mBar.Wrappable = false;
            mBar.TextAlign = ToolBarTextAlign.Right;
            mBar.ImageList = new ImageList();
            mBar.ImageList.Images.Add("s", resx.Find);
            mBar.ImageList.Images.Add("f", resx.Favorites_9002_24);
            mBar.ImageList.Images.Add("box", resx.Box_10401_24);
            mBar.ImageList.Images.Add("time", resx.clock);
            mBar.ImageList.Images.Add("home", resx.Home);
            mBar.ImageList.Images.Add("link", resx.Link);
            mBar.ImageList.Images.Add("text", resx.Note);
            mBar.ImageList.Images.Add("web", resx.Web);
            mBar.ImageList.Images.Add("image", resx.InsertPicture);
            mBar.ImageList.Images.Add("left", resx.DoubleLeftArrow);
            mBar.ImageList.Images.Add("right", resx.DoubleRightArrow);
            mBar.ImageList.Images.Add("page", resx.Document);
            mBar.ImageList.Images.Add("curpage", resx.PageNumber);
            mBar.ImageList.TransparentColor = Color.FromArgb(255, 0, 255);

            mBar.Buttons.Add(new ToolBarButton() { ImageKey = "home", Tag = "home" });
            mBar.Buttons.Add(new ToolBarButton() { ImageKey = "f", Tag = "favorites", ToolTipText = resx.ShowFavorites, Style = ToolBarButtonStyle.ToggleButton });
            mBar.Buttons.Add(new ToolBarButton() { ImageKey = "s", Tag = "searchname", ToolTipText = resx.Search });
            mBar.Buttons.Add(new ToolBarButton() { ImageKey = "time", Tag = "searchtime", ToolTipText = resx.SearchTimespan });
            mBar.Buttons.Add(new ToolBarButton() { ImageKey = "link", Tag = "searchurl", ToolTipText = resx.SearchURLs });
            mBar.Buttons.Add(new ToolBarButton() { Enabled = false, Tag = "" });
            mBar.Buttons.Add(mViewFilter[0] = new ToolBarButton() { ImageKey = "text", Tag = "showText", ToolTipText = resx.ShowText });
            mBar.Buttons.Add(mViewFilter[1] = new ToolBarButton() { ImageKey = "web", Tag = "showHTML", ToolTipText = resx.ShowHTML });
            mBar.Buttons.Add(mViewFilter[2] = new ToolBarButton() { ImageKey = "image", Tag = "showImage", ToolTipText = resx.ShowImage });
            mBar.ButtonClick += (tbBtn, tbEvent) =>
            {
                ToolBarButton btn = tbEvent.Button;
                if (btn.Tag.ToString().StartsWith("show"))
                {
                    btn.Pushed = !btn.Pushed;
                    RefreshDataMainView();
                    return;
                }
                switch (btn.Tag.ToString())
                {
                    case "searchname":
                    case "searchtime":
                    case "searchurl":
                        PrepareSearch(btn.Tag.ToString().Substring(6));
                        break;
                    case "favorites":
                        (mainData.Tag as Page).Where = btn.Pushed ? "AND favorited = 1" : "";
                        RefreshDataMainView();
                        break;
                    case "home":
                        foreach (ToolBarButton btn2 in mBar.Buttons)
                            if (!btn2.Tag.ToString().StartsWith("show"))
                                btn2.Pushed = false;
                        (mainData.Tag as Page).Where = "";
                        (mainData.Tag as Page).Current = 1;
                        RefreshDataMainView();
                        break;
                }
            };
            this.Controls.Add(mBar);

            mBarNav = new ToolBar();
            mBarNav.ImageList = mBar.ImageList;
            mBarNav.Dock = DockStyle.Bottom;
            mBarNav.ButtonClick += (tbBtn, tbEvent) =>
            {
                (mainData.Tag as Page).Current = (int)(tbEvent.Button.Tag ?? 0);
                RefreshDataMainView();
            };
            this.Controls.Add(mBarNav);

            mBarNav.BringToFront();
            mainData.BringToFront();

            statusInfo.Renderer = new StatusBarCustomRenderer();
            //statusDbPath.TextAlign = ContentAlignment.MiddleRight;

            notifyIcon.Icon = resx.SystrayIcon;
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem(listenToolStripMenuItem.Text, listenToolStripMenuItem_Click),
                new MenuItem(resx.OpenCBer, (v1, v2) => notifyIcon_MouseDoubleClick(v1, null)),
                new MenuItem(exitToolStripMenuItem.Text, exitToolStripMenuItem_Click),
            });
        }

        public class StatusBarCustomRenderer : ToolStripSystemRenderer
        {
            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                if (e.Item is ToolStripStatusLabel)
                {
                    TextRenderer.DrawText(e.Graphics, e.Text, e.TextFont, e.TextRectangle,
                        e.TextColor, Color.Transparent, e.TextFormat | TextFormatFlags.Right | TextFormatFlags.PathEllipsis);
                    return;
                }

                base.OnRenderItemText(e);
            }
        }
    }
}
