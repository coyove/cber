using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clipboarder
{
    class ClipboardWorkaround
    {
        [DllImport("user32.dll")]
        private static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        private static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        private static extern IntPtr GetClipboardData(uint type);

        [DllImport("user32.dll")]
        private static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

        [DllImport("user32.dll")]
        private static extern uint RegisterClipboardFormatW([MarshalAs (UnmanagedType.LPWStr)] string lpszFormat);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("kernel32.dll")]
        private static extern bool GlobalUnlock(IntPtr hMem);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GlobalAlloc(uint uFlags, uint dwBytes);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GlobalFree(IntPtr hMem);

        private static uint HtmlFormat = RegisterClipboardFormatW(DataFormats.Html);

        private static string GetHtmlNET()
        {
            string content = Clipboard.GetData(DataFormats.Html)?.ToString();
            if (string.IsNullOrEmpty(content)) return "";

            byte[] buf = Encoding.Default.GetBytes(content);
            return Encoding.UTF8.GetString(buf);
        }

        public static string GetHtml()
        {
            if (!OpenClipboard(IntPtr.Zero))
                return GetHtmlNET();

            IntPtr handle = GetClipboardData(HtmlFormat);
            IntPtr ptr = GlobalLock(handle);
            if (ptr == IntPtr.Zero)
                return GetHtmlNET();

            int i = 0;
            MemoryStream buf = new MemoryStream();
            while (true)
            {
                byte b = Marshal.ReadByte(ptr, i);
                if (b == 0) break;
                buf.WriteByte(b);
                i++;
            }

            string content = Encoding.UTF8.GetString(buf.GetBuffer());

            GlobalUnlock(handle);
            CloseClipboard();

            return content;
        }

        public static void SetHtml(string htmlContent, string pureText)
        {
            SetText(HtmlFormat, htmlContent);
            SetText(13 /*UnicodeText*/, pureText);
        }

        private static void SetText(uint format, string content)
        {
            MemoryStream buf = new MemoryStream();
            byte[] p = format == HtmlFormat ?
                Encoding.UTF8.GetBytes(content) :
                Encoding.Unicode.GetBytes(content);
            buf.Write(p, 0, p.Length);
            buf.WriteByte(0);

            IntPtr handle = GlobalAlloc(2, (uint)buf.Length);
            IntPtr ptr = GlobalLock(handle);
            if (ptr == IntPtr.Zero) return;

            p = buf.ToArray();
            Marshal.Copy(p, 0, ptr, p.Length);
            GlobalUnlock(handle);

            if (!OpenClipboard(IntPtr.Zero))
                return;
            SetClipboardData(format, ptr);
            CloseClipboard();

            // let system clipboard handles them
            //GlobalFree(ptr);
        }
    }
}
