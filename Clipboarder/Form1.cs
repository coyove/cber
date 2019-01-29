using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Clipboarder
{
   public partial class Form1 : Form
   {
      [DllImport("User32.dll")]
      protected static extern int SetClipboardViewer(int hWndNewViewer);

      [DllImport("User32.dll", CharSet = CharSet.Auto)]
      public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

      private Database mDB;

      private IntPtr mNextClipboardViewer;

      private bool mActivated;

      public Form1()
      {
         InitializeComponent();
      }

      protected override void WndProc(ref System.Windows.Forms.Message m)
      {
         // defined in winuser.h
         const int WM_DRAWCLIPBOARD = 0x308;
         const int WM_CHANGECBCHAIN = 0x030D;

         switch (m.Msg)
         {
            case WM_DRAWCLIPBOARD:
               OnClipboardChanged();
               SendMessage(mNextClipboardViewer, m.Msg, m.WParam, m.LParam);
               break;

            case WM_CHANGECBCHAIN:
               if (m.WParam == mNextClipboardViewer)
                  mNextClipboardViewer = m.LParam;
               else
                  SendMessage(mNextClipboardViewer, m.Msg, m.WParam, m.LParam);
               break;

            default:
               base.WndProc(ref m);
               break;
         }
      }

      private void OnClipboardChanged()
      {
         if (mActivated) return;

         IDataObject data = Clipboard.GetDataObject();
         if (mDB == null || data == null) return;

         if (Clipboard.ContainsText())
         {
            string content = data.GetData(typeof(string)).ToString();
            mDB.Insert(null, content);
         }
         else if (Clipboard.ContainsImage())
         {
            Image content = (Image)data.GetData(DataFormats.Bitmap, true);
            MemoryStream buf = new MemoryStream();
            content.Save(buf, System.Drawing.Imaging.ImageFormat.Png);
            mDB.Insert(null, buf.GetBuffer(), Database.ContentType.Image);
         }

         RefreshDataMainView();
      }

      private void RefreshDataMainView()
      {
         mainData.Rows.Clear();
         foreach (Database.Entry e in mDB.Paging(null, null, 0, 20))
         {
            int index = mainData.Rows.Add();
            mainData.Rows[index].Cells["entryName"].Value = e.Name;
            string title = e.Id.ToString() + "|" + e.Time.ToString() + (e.Hits > 1 ? "|" + e.Hits.ToString() : "");

            if (e.Content is string)
            {
               var cell = new TextTitleCell();
               mainData.Rows[index].Cells["entryContent"] = cell;
               cell.Title = title;
               cell.Value = e.Content.ToString();
            }
            else
            {
               var cell = new ImageTitleCell();
               mainData.Rows[index].Cells["entryContent"] = cell;
               cell.Title = title;
               cell.Value = e.Content;
            }
         }
      }

      private void Form1_Load(object sender, EventArgs e)
      {
         mActivated = true;
         mNextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);
         mainData.RowTemplate.MinimumHeight = 100;
         entryName.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
         entryContent.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
         entryContent.DefaultCellStyle.Font = new Font("Consolas", 12);

         System.IO.File.Delete("test.db");
         mDB = Database.Open("test.db");
      }

      private void Form1_FormClosed(object sender, FormClosedEventArgs e)
      {
         if (mDB != null)
            mDB.Close();
      }

      private void Form1_Activated(object sender, EventArgs e)
      {
         mActivated = true;
      }

      private void Form1_Deactivate(object sender, EventArgs e)
      {
         mActivated = false;
      }
   }
}
