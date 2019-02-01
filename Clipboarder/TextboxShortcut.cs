using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clipboarder
{
    class TextboxShortcut : TextBox
    {
        private bool mCtrl = false;
        private bool mAlt = false;
        private bool mShift = false;
        private bool mWin = false;
        private Keys mKey;

        public TextboxShortcut() : base()
        {
            this.ReadOnly = true;
        }

        private void ShowKeys()
        {
            List<string> modifiers = new List<string>(3);
            if (mCtrl) modifiers.Add("Ctrl");
            if (mAlt) modifiers.Add("Alt");
            if (mShift) modifiers.Add("Shift");
            if (mWin) modifiers.Add("Win");
            modifiers.Add(mKey.ToString());
            this.Text = string.Join("+" , modifiers);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (!mCtrl ) mCtrl = e.Modifiers.HasFlag(Keys.Control);
            if (!mAlt ) mAlt = e.Modifiers.HasFlag(Keys.Alt);
            if (!mShift ) mShift = e.Modifiers.HasFlag(Keys.Shift);
            if (!mWin ) mWin = e.KeyCode.HasFlag(Keys.LWin);
            mKey = e.KeyCode;
            ShowKeys();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            mCtrl = mAlt = mShift = mWin = false;
            mKey = Keys.None;
        }
    }
}
