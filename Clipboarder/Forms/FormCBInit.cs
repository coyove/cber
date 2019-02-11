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
            mBar.ImageList = new ImageList();
            mBar.ImageList.Images.Add("s", Properties.Resources.Find);
            mBar.ImageList.Images.Add("f", Properties.Resources.Favorites_9002_24);
            mBar.ImageList.Images.Add("box", Properties.Resources.Box_10401_24);
            mBar.ImageList.Images.Add("time", Properties.Resources.Timer_709_24);
            mBar.ImageList.Images.Add("text", Properties.Resources.Note);
            mBar.ImageList.Images.Add("web", Properties.Resources.Web);
            mBar.ImageList.Images.Add("image", Properties.Resources.InsertPicture);
            mBar.ImageList.TransparentColor = Color.FromArgb(255, 0, 255);
            mBar.Buttons.Add(new ToolBarButton(resx.ShowAll) { ImageKey = "time", Tag = "time" });
            mBar.Buttons.Add(new ToolBarButton(resx.Favorites) { ImageKey = "f", Tag = "favorites", Style = ToolBarButtonStyle.ToggleButton });
            mBar.Buttons.Add(new ToolBarButton(resx.Search) { ImageKey = "s", Tag = "searchname" });
            mBar.Buttons.Add(new ToolBarButton(resx.Timespan) { ImageKey = "s", Tag = "searchtime" });
            mBar.Buttons.Add(new ToolBarButton(resx.ByURLs) { ImageKey = "s", Tag = "searchurl" });
            mBar.ButtonClick += (tbBtn, tbEvent) =>
            {
                ToolBarButton btn = tbEvent.Button;
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
                    case "time":
                        foreach (ToolBarButton btn2 in mBar.Buttons) btn2.Pushed = false;
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
            mBarNav.Buttons.Add(mViewFilter[0] = new ToolBarButton("Text") { ImageKey = "text", Tag = "showText" });
            mBarNav.Buttons.Add(mViewFilter[1] = new ToolBarButton("HTML") { ImageKey = "web", Tag = "showHTML" });
            mBarNav.Buttons.Add(mViewFilter[2] = new ToolBarButton("Image") { ImageKey = "image", Tag = "showImage" });
            mBarNav.Buttons.Add(new ToolBarButton() { Style = ToolBarButtonStyle.Separator });
            mBarNav.ButtonClick += (tbBtn, tbEvent) =>
            {
                if (tbEvent.Button.Tag.ToString().StartsWith("show"))
                {
                    tbEvent.Button.Pushed = !tbEvent.Button.Pushed;
                }
                else
                {
                    (mainData.Tag as Page).Current = (int)(tbEvent.Button.Tag ?? 0);
                }
                RefreshDataMainView();
            };
            this.Controls.Add(mBarNav);
            mBarNav.BringToFront();
            mainData.BringToFront();
        }
    }
}
