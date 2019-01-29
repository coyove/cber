﻿using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Clipboarder
{
   class Helper
   {
      private const UInt32 sGenerator = 0xEDB88320;
      private static readonly UInt32[] mChecksumTable;

      public static Int32 UnixTimestamp()
      {
         return (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
      }

      public static string GetDefaultName()
      {
         return DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
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
   }

   class Database
   {
      public class Entry
      {
         public int Id { get; set; }
         public int Hits { get; set; }
         public ContentType Type { get; set; }
         public string Name { get; set; }
         public DateTime Time { get; set; }
         public object Content { get; set; }
      }

      public enum ContentType
      {
         RawText = 0,
         RawBinary,
         Image,
      }

      private IntPtr mDB;
      private string mPath = null;

      public static Database Open(string path)
      {
         bool newlyCreated = !System.IO.File.Exists(path);
         Database db = new Database();

         SQLite3.Open(path, out db.mDB);
         if (db.mDB == null)
            return null;

         if (newlyCreated)
         {
            IntPtr stmt = SQLite3.Prepare2(db.mDB, @";
CREATE TABLE IF NOT EXISTS data_table (
   id INTEGER PRIMARY KEY AUTOINCREMENT,
   ts INTEGER NOT NULL,
   name TEXT NOT NULL,
   type INTEGER NOT NULL,
   hash INTEGER NOT NULL,
   hits INTEGER DEFAULT 1,
   text_content TEXT,
   binary_content BLOB
);
CREATE INDEX data_table_name_idx ON data_table (name);
CREATE INDEX data_table_type_idx ON data_table (type);
CREATE INDEX data_table_hash_idx ON data_table (hash);
");
            SQLite3.Step(stmt);
            SQLite3.Finalize(stmt);
         }

         db.mPath = path;
         return db;
      }

      public void Close()
      {
         SQLite3.Close(mDB);
      }

      private int FindIDByHash(int hash)
      {
         int id = 0;
         IntPtr stmt = SQLite3.Prepare2(mDB, "SELECT id FROM data_table WHERE hash = ? ORDER BY ts ASC LIMIT 1;");
         SQLite3.BindInt(stmt, 1, hash);
         if (SQLite3.Step(stmt) == SQLite3.Result.Row)
         {
            id = SQLite3.ColumnInt(stmt, 0);
         }
         SQLite3.Finalize(stmt);
         return id;
      }

      public SQLite3.Result Insert(string name, string content)
      {
         int id = FindIDByHash(content.GetHashCode());
         IntPtr stmt = SQLite3.Prepare2(mDB, id > 0 ?
            "UPDATE data_table SET ts = ?, hits = hits + 1 WHERE id = ?;" :
            "INSERT INTO data_table (ts, name, type, hash, text_content) VALUES (?, ?, ?, ?, ?);");

         if (string.IsNullOrWhiteSpace(name))
            name = Helper.GetDefaultName();

         SQLite3.BindInt(stmt, 1, Helper.UnixTimestamp());
         if (id > 0)
         {
            SQLite3.BindInt(stmt, 2, id);
         }
         else
         {
            SQLite3.BindText(stmt, 2, name);
            SQLite3.BindInt(stmt, 3, 0);
            SQLite3.BindInt(stmt, 4, content.GetHashCode());
            SQLite3.BindText(stmt, 5, content);
         }

         var res = SQLite3.Step(stmt);
         SQLite3.Finalize(stmt);
         return res;
      }

      public SQLite3.Result Insert(string name, byte[] content, ContentType contentType)
      {
         int hash = (int)Helper.Crc32(content);
         int id = FindIDByHash(hash);
         IntPtr stmt = SQLite3.Prepare2(mDB, id > 0 ?
            "UPDATE data_table SET ts = ?, hits = hits + 1 WHERE id = ?;" :
            "INSERT INTO data_table (ts, name, type, hash, binary_content) VALUES (?, ?, ?, ?, ?);");
         if (string.IsNullOrWhiteSpace(name))
            name = Helper.GetDefaultName();

         SQLite3.BindInt(stmt, 1, Helper.UnixTimestamp());

         if (id > 0)
         {
            SQLite3.BindInt(stmt, 2, id);
         }
         else
         {
            SQLite3.BindText(stmt, 2, name);
            SQLite3.BindInt(stmt, 3, (int)contentType);
            SQLite3.BindInt(stmt, 4, hash);
            SQLite3.BindBlob(stmt, 5, content, content.Length, (IntPtr)(-1));
         }

         var res = SQLite3.Step(stmt);
         SQLite3.Finalize(stmt);
         return res;
      }

      public SQLite3.Result Rename(int id, string newName)
      {
         IntPtr stmt = SQLite3.Prepare2(mDB, "UPDATE data_table SET name = ? WHERE id = ?");
         SQLite3.BindText(stmt, 1, newName);
         SQLite3.BindInt(stmt, 2, id);
         var res = SQLite3.Step(stmt);
         SQLite3.Finalize(stmt);
         return res;
      }

      public Entry[] Paging(string where, string orderByFields, int start, int length)
      {
         if (string.IsNullOrWhiteSpace(where))
            where = "1 = 1";
         if (string.IsNullOrWhiteSpace(orderByFields))
            orderByFields = "ts DESC";

         List<Entry> entries = new List<Entry>(length);
         IntPtr stmt = SQLite3.Prepare2(mDB, string.Format(
            "SELECT id, ts, name, type, text_content, binary_content, hits FROM data_table WHERE {0} ORDER BY {1} LIMIT {2}, {3};",
            where, orderByFields, start, length));
         while (SQLite3.Step(stmt) == SQLite3.Result.Row)
         {
            Entry e = new Entry();
            e.Id = SQLite3.ColumnInt(stmt, 0);
            e.Time = new DateTime(1970, 1, 1).AddSeconds(SQLite3.ColumnInt(stmt, 1)).ToLocalTime();
            e.Name = SQLite3.ColumnString(stmt, 2);
            e.Type = (ContentType)(SQLite3.ColumnInt(stmt, 3));
            switch (e.Type) {
               case ContentType.RawText:
                  e.Content = SQLite3.ColumnString(stmt, 4);
                  break;
               case ContentType.RawBinary:
                  e.Content = SQLite3.ColumnByteArray(stmt, 5);
                  break;
               case ContentType.Image:
                  MemoryStream s = new MemoryStream(SQLite3.ColumnByteArray(stmt, 5));
                  e.Content = System.Drawing.Image.FromStream(s);
                  s.Dispose();
                  break;
            }
            e.Hits = SQLite3.ColumnInt(stmt, 6);
            entries.Add(e);
         }
         SQLite3.Finalize(stmt);
         return entries.ToArray();
      }
   }
}
