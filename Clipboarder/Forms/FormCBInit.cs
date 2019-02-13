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
        private PathLabel mPathDisplay;
        private void InitUi()
        {
            //mPathDisplay = new PathLabel();
            //mPathDisplay.Dock = DockStyle.Bottom;
            //mainData.BringToFront();
            //this.Controls.Add(mPathDisplay);

            notifyIcon.Icon = resx.SystrayIcon;
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem(listenToolStripMenuItem.Text, listenToolStripMenuItem_Click),
                new MenuItem(resx.OpenCBer, (v1, v2) => notifyIcon_MouseDoubleClick(v1, null)),
                new MenuItem(exitToolStripMenuItem.Text, exitToolStripMenuItem_Click),
            });

            EventHandler menuCheckingHandler = (ss, se) =>
            {
                (ss as MenuItem).Checked = !(ss as MenuItem).Checked;
                RefreshDataMainView();
            };
            showTextContents.Click += menuCheckingHandler;
            showImageContents.Click += menuCheckingHandler;
            showHTMLContents.Click += menuCheckingHandler;
            listenHTMLContents.Click += menuCheckingHandler;
            listenTextContents.Click +=menuCheckingHandler;
            listenImageContents.Click +=menuCheckingHandler;
            hideAfterCopyToolStripMenuItem.Click += menuCheckingHandler;

            EventHandler searchHandler = (ss, se) => PrepareSearch((ss as MenuItem).Tag.ToString());
            searchContent.Click += searchHandler;
            searchTimespan.Click += searchHandler;
            searchURL.Click += searchHandler;
        }
    }
}
