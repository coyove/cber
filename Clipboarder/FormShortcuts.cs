﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clipboarder
{
    public partial class FormShortcuts : Form
    {
        public FormShortcuts()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormShortcuts_Load(object sender, EventArgs e)
        {
            TextboxShortcut box = new TextboxShortcut();
            panelMainWindow.Controls.Add(box);
            box.Dock = DockStyle.Fill;
        }
    }
}
