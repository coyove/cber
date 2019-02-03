using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace Clipboarder
{
    class Helper
    {
        private const UInt32 sGenerator = 0xEDB88320;
        private static readonly UInt32[] mChecksumTable;

        public static Tuple<int, int> SlidingWindow(int length, int window, int index)
        {
            int w = (window - 1) / 2;
            if (length <= window)
                return new Tuple<int, int>(1, length);
            if (index <= w)
                index = w + 1;
            if (index > length - w)
                index = length - w;
            return new Tuple<int, int>(index - w, index + w);
        }

        public static string GetHostFromUri(string uri)
        {
            try
            {
                return new Uri(uri).Host;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string ExtractFieldFromHTMLClipboard(string content, string name)
        {
            foreach (string line in content.Substring(0, content.Length < 1024 ? content.Length : 1024)
                                .Split(new char[] { '\n' }))
            {
                if (line.StartsWith(name + ":"))
                    return line.Substring(1 + name.Length);
            }
            return "";
        }

        public static string ExtractHTMLFromClipboard(string text)
        {
            int startHTML = 0;
            int.TryParse(Helper.ExtractFieldFromHTMLClipboard(text, "StartHTML"), out startHTML);
            return text.Substring(startHTML);
        }

        public static string ExtractImgUrlFromHTML(string html)
        {
            var m = new Regex(@"<img.+src=['""](\S+)['""]").Match(html);
            return (m.Groups.Count == 2) ? m.Groups[1].Value : "";
        }

        // TODO: HTML parser
        public static string ExtractTextFromHTML(string html, int maxLength = 1024)
        {
            if (string.IsNullOrWhiteSpace(html)) return html;

            StringBuilder res = new StringBuilder();
            StringReader buf = new StringReader(html);
            bool flag = false;
            while (buf.Peek() >= 0)
            {
                char ch = (char)buf.Read();
                if (ch == '<' || ch == '>')
                {
                    flag = ch == '<';
                    continue;
                }

                if (!flag && !"\n\r".Contains(ch))
                    res.Append(ch);

                if (res.Length > maxLength)
                    break;
            }
            return res.ToString();
        }

        public static Int32 UnixTimestamp()
        {
            return (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static string GetDefaultName()
        {
            //return DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            return "<U>";
        }

        static Helper()
        {
            mChecksumTable = Enumerable.Range(0, 256).Select(i =>
            {
                var tableEntry = (uint)i;
                for (var j = 0; j < 8; ++j)
                {
                    tableEntry = ((tableEntry & 1) != 0)
                     ? (sGenerator ^ (tableEntry >> 1))
                     : (tableEntry >> 1);
                }
                return tableEntry;
            }).ToArray();
        }

        public static UInt32 Crc32(byte[] buf)
        {
            UInt32 hash = 0xFFFFFFFF;
            foreach (byte b in buf)
            {
                hash = mChecksumTable[(hash & 0xFF) ^ b] ^ (hash >> 8);
            }
            return hash;
        }

        public static void SetClipboard(Database.Entry entry)
        {
            switch (entry.Type)
            {
                case Database.ContentType.HTML:
                    DataObject obj = new DataObject();
                    obj.SetData(DataFormats.Html, entry.Content);
                    obj.SetData(DataFormats.StringFormat, entry.Html);
                    Clipboard.SetDataObject(obj, true);
                    break;
                case Database.ContentType.Image:
                    Clipboard.SetData(DataFormats.Bitmap, entry.Content);
                    break;
                default:
                    Clipboard.SetData(DataFormats.StringFormat, entry.Content);
                    break;
            }
        }

        public static double CalcFitZoom(Image img, int width, int height)
        {
            int w = img.Width, h = img.Height;
            int pw = width, ph = height;

            if (w <= pw && h <= ph) return 1;
            if (w > pw && h <= ph) return (double)pw / w;
            if (w <= pw && h > ph) return (double)ph / h;
            if (w > pw && h > ph)
            {
                double mZoom = (double)ph / h;
                if (w * mZoom > pw)
                    mZoom = (double)pw  / w;
                return mZoom;
            }

            throw new InvalidOperationException("not possible");
        }
    }
}
