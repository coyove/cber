//
// Copyright (c) 2009-2019 Krueger Systems, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Clipboarder
{
	public class SQLiteException : Exception
	{
		public SQLite3.Result Result { get; private set; }

		protected SQLiteException (SQLite3.Result r, string message) : base (message)
		{
			Result = r;
		}

		public static SQLiteException New (SQLite3.Result r, string message)
		{
			return new SQLiteException (r, message);
		}
	}

   public static class SQLite3
	{
		public enum Result : int
		{
			OK = 0,
			Error = 1,
			Internal = 2,
			Perm = 3,
			Abort = 4,
			Busy = 5,
			Locked = 6,
			NoMem = 7,
			ReadOnly = 8,
			Interrupt = 9,
			IOError = 10,
			Corrupt = 11,
			NotFound = 12,
			Full = 13,
			CannotOpen = 14,
			LockErr = 15,
			Empty = 16,
			SchemaChngd = 17,
			TooBig = 18,
			Constraint = 19,
			Mismatch = 20,
			Misuse = 21,
			NotImplementedLFS = 22,
			AccessDenied = 23,
			Format = 24,
			Range = 25,
			NonDBFile = 26,
			Notice = 27,
			Warning = 28,
			Row = 100,
			Done = 101
		}

		public enum ExtendedResult : int
		{
			IOErrorRead = (Result.IOError | (1 << 8)),
			IOErrorShortRead = (Result.IOError | (2 << 8)),
			IOErrorWrite = (Result.IOError | (3 << 8)),
			IOErrorFsync = (Result.IOError | (4 << 8)),
			IOErrorDirFSync = (Result.IOError | (5 << 8)),
			IOErrorTruncate = (Result.IOError | (6 << 8)),
			IOErrorFStat = (Result.IOError | (7 << 8)),
			IOErrorUnlock = (Result.IOError | (8 << 8)),
			IOErrorRdlock = (Result.IOError | (9 << 8)),
			IOErrorDelete = (Result.IOError | (10 << 8)),
			IOErrorBlocked = (Result.IOError | (11 << 8)),
			IOErrorNoMem = (Result.IOError | (12 << 8)),
			IOErrorAccess = (Result.IOError | (13 << 8)),
			IOErrorCheckReservedLock = (Result.IOError | (14 << 8)),
			IOErrorLock = (Result.IOError | (15 << 8)),
			IOErrorClose = (Result.IOError | (16 << 8)),
			IOErrorDirClose = (Result.IOError | (17 << 8)),
			IOErrorSHMOpen = (Result.IOError | (18 << 8)),
			IOErrorSHMSize = (Result.IOError | (19 << 8)),
			IOErrorSHMLock = (Result.IOError | (20 << 8)),
			IOErrorSHMMap = (Result.IOError | (21 << 8)),
			IOErrorSeek = (Result.IOError | (22 << 8)),
			IOErrorDeleteNoEnt = (Result.IOError | (23 << 8)),
			IOErrorMMap = (Result.IOError | (24 << 8)),
			LockedSharedcache = (Result.Locked | (1 << 8)),
			BusyRecovery = (Result.Busy | (1 << 8)),
			CannottOpenNoTempDir = (Result.CannotOpen | (1 << 8)),
			CannotOpenIsDir = (Result.CannotOpen | (2 << 8)),
			CannotOpenFullPath = (Result.CannotOpen | (3 << 8)),
			CorruptVTab = (Result.Corrupt | (1 << 8)),
			ReadonlyRecovery = (Result.ReadOnly | (1 << 8)),
			ReadonlyCannotLock = (Result.ReadOnly | (2 << 8)),
			ReadonlyRollback = (Result.ReadOnly | (3 << 8)),
			AbortRollback = (Result.Abort | (2 << 8)),
			ConstraintCheck = (Result.Constraint | (1 << 8)),
			ConstraintCommitHook = (Result.Constraint | (2 << 8)),
			ConstraintForeignKey = (Result.Constraint | (3 << 8)),
			ConstraintFunction = (Result.Constraint | (4 << 8)),
			ConstraintNotNull = (Result.Constraint | (5 << 8)),
			ConstraintPrimaryKey = (Result.Constraint | (6 << 8)),
			ConstraintTrigger = (Result.Constraint | (7 << 8)),
			ConstraintUnique = (Result.Constraint | (8 << 8)),
			ConstraintVTab = (Result.Constraint | (9 << 8)),
			NoticeRecoverWAL = (Result.Notice | (1 << 8)),
			NoticeRecoverRollback = (Result.Notice | (2 << 8))
		}


		public enum ConfigOption : int
		{
			SingleThread = 1,
			MultiThread = 2,
			Serialized = 3
		}

		const string LibraryPath = "sqlite3";

		[DllImport(LibraryPath, EntryPoint = "sqlite3_threadsafe", CallingConvention=CallingConvention.Cdecl)]
		public static extern int Threadsafe ();

		[DllImport(LibraryPath, EntryPoint = "sqlite3_open", CallingConvention=CallingConvention.Cdecl)]
		public static extern Result Open ([MarshalAs(UnmanagedType.LPStr)] string filename, out IntPtr db);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_open_v2", CallingConvention=CallingConvention.Cdecl)]
		public static extern Result Open ([MarshalAs(UnmanagedType.LPStr)] string filename, out IntPtr db, int flags, [MarshalAs (UnmanagedType.LPStr)] string zvfs);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_open_v2", CallingConvention = CallingConvention.Cdecl)]
		public static extern Result Open(byte[] filename, out IntPtr db, int flags, [MarshalAs (UnmanagedType.LPStr)] string zvfs);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_open16", CallingConvention = CallingConvention.Cdecl)]
		public static extern Result Open16([MarshalAs(UnmanagedType.LPWStr)] string filename, out IntPtr db);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_enable_load_extension", CallingConvention=CallingConvention.Cdecl)]
		public static extern Result EnableLoadExtension (IntPtr db, int onoff);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_close", CallingConvention=CallingConvention.Cdecl)]
		public static extern Result Close (IntPtr db);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_close_v2", CallingConvention = CallingConvention.Cdecl)]
		public static extern Result Close2(IntPtr db);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_initialize", CallingConvention=CallingConvention.Cdecl)]
		public static extern Result Initialize();

		[DllImport(LibraryPath, EntryPoint = "sqlite3_shutdown", CallingConvention=CallingConvention.Cdecl)]
		public static extern Result Shutdown();

		[DllImport(LibraryPath, EntryPoint = "sqlite3_config", CallingConvention=CallingConvention.Cdecl)]
		public static extern Result Config (ConfigOption option);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_win32_set_directory", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Unicode)]
		public static extern int SetDirectory (uint directoryType, string directoryPath);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_busy_timeout", CallingConvention=CallingConvention.Cdecl)]
		public static extern Result BusyTimeout (IntPtr db, int milliseconds);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_changes", CallingConvention=CallingConvention.Cdecl)]
		public static extern int Changes (IntPtr db);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_prepare_v2", CallingConvention=CallingConvention.Cdecl)]
		public static extern Result Prepare2 (IntPtr db, [MarshalAs(UnmanagedType.LPStr)] string sql, int numBytes, out IntPtr stmt, IntPtr pzTail);

		public static IntPtr Prepare2 (IntPtr db, string query)
		{
			IntPtr stmt;
         var r = Prepare2 (db, query, System.Text.UTF8Encoding.UTF8.GetByteCount (query), out stmt, IntPtr.Zero);
			if (r != Result.OK) {
				throw SQLiteException.New (r, GetErrmsg (db));
			}
			return stmt;
		}

		[DllImport(LibraryPath, EntryPoint = "sqlite3_step", CallingConvention=CallingConvention.Cdecl)]
		public static extern Result Step (IntPtr stmt);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_reset", CallingConvention=CallingConvention.Cdecl)]
		public static extern Result Reset (IntPtr stmt);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_finalize", CallingConvention=CallingConvention.Cdecl)]
		public static extern Result Finalize (IntPtr stmt);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_last_insert_rowid", CallingConvention=CallingConvention.Cdecl)]
		public static extern long LastInsertRowid (IntPtr db);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_errmsg16", CallingConvention=CallingConvention.Cdecl)]
		public static extern IntPtr Errmsg (IntPtr db);

		public static string GetErrmsg (IntPtr db)
		{
			return Marshal.PtrToStringUni (Errmsg (db));
		}

		[DllImport(LibraryPath, EntryPoint = "sqlite3_bind_parameter_index", CallingConvention=CallingConvention.Cdecl)]
		public static extern int BindParameterIndex (IntPtr stmt, [MarshalAs(UnmanagedType.LPStr)] string name);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_bind_null", CallingConvention=CallingConvention.Cdecl)]
		public static extern int BindNull (IntPtr stmt, int index);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_bind_int", CallingConvention=CallingConvention.Cdecl)]
		public static extern int BindInt (IntPtr stmt, int index, int val);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_bind_int64", CallingConvention=CallingConvention.Cdecl)]
		public static extern int BindInt64 (IntPtr stmt, int index, long val);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_bind_double", CallingConvention=CallingConvention.Cdecl)]
		public static extern int BindDouble (IntPtr stmt, int index, double val);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_bind_text16", CallingConvention=CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int BindText (IntPtr stmt, int index, [MarshalAs(UnmanagedType.LPWStr)] string val, int n, IntPtr free);

		public static int BindText (IntPtr stmt, int index, string val)
      {
         // Transient string
         return BindText(stmt, index, val, System.Text.UnicodeEncoding.Unicode.GetByteCount(val), (IntPtr)(-1));
      }

		[DllImport(LibraryPath, EntryPoint = "sqlite3_bind_blob", CallingConvention=CallingConvention.Cdecl)]
		public static extern int BindBlob (IntPtr stmt, int index, byte[] val, int n, IntPtr free);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_column_count", CallingConvention=CallingConvention.Cdecl)]
		public static extern int ColumnCount (IntPtr stmt);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_column_name", CallingConvention=CallingConvention.Cdecl)]
		public static extern IntPtr ColumnName (IntPtr stmt, int index);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_column_name16", CallingConvention=CallingConvention.Cdecl)]
		static extern IntPtr ColumnName16Internal (IntPtr stmt, int index);
		public static string ColumnName16(IntPtr stmt, int index)
		{
			return Marshal.PtrToStringUni(ColumnName16Internal(stmt, index));
		}

		[DllImport(LibraryPath, EntryPoint = "sqlite3_column_type", CallingConvention=CallingConvention.Cdecl)]
		public static extern ColType ColumnType (IntPtr stmt, int index);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_column_int", CallingConvention=CallingConvention.Cdecl)]
		public static extern int ColumnInt (IntPtr stmt, int index);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_column_int64", CallingConvention=CallingConvention.Cdecl)]
		public static extern long ColumnInt64 (IntPtr stmt, int index);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_column_double", CallingConvention=CallingConvention.Cdecl)]
		public static extern double ColumnDouble (IntPtr stmt, int index);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_column_text", CallingConvention=CallingConvention.Cdecl)]
		public static extern IntPtr ColumnText (IntPtr stmt, int index);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_column_text16", CallingConvention=CallingConvention.Cdecl)]
		public static extern IntPtr ColumnText16 (IntPtr stmt, int index);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_column_blob", CallingConvention=CallingConvention.Cdecl)]
		public static extern IntPtr ColumnBlob (IntPtr stmt, int index);

		[DllImport(LibraryPath, EntryPoint = "sqlite3_column_bytes", CallingConvention=CallingConvention.Cdecl)]
		public static extern int ColumnBytes (IntPtr stmt, int index);

		public static string ColumnString (IntPtr stmt, int index)
		{
			return Marshal.PtrToStringUni (SQLite3.ColumnText16 (stmt, index));
		}

		public static byte[] ColumnByteArray (IntPtr stmt, int index)
		{
			int length = ColumnBytes (stmt, index);
			var result = new byte[length];
			if (length > 0)
				Marshal.Copy (ColumnBlob (stmt, index), result, 0, length);
			return result;
		}

		[DllImport (LibraryPath, EntryPoint = "sqlite3_errcode", CallingConvention = CallingConvention.Cdecl)]
		public static extern Result GetResult (IntPtr db);

		[DllImport (LibraryPath, EntryPoint = "sqlite3_extended_errcode", CallingConvention = CallingConvention.Cdecl)]
		public static extern ExtendedResult ExtendedErrCode (IntPtr db);

		[DllImport (LibraryPath, EntryPoint = "sqlite3_libversion_number", CallingConvention = CallingConvention.Cdecl)]
		public static extern int LibVersionNumber ();

		[DllImport (LibraryPath, EntryPoint = "sqlite3_backup_init", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr BackupInit (IntPtr destDb, [MarshalAs (UnmanagedType.LPStr)] string destName, IntPtr sourceDb, [MarshalAs (UnmanagedType.LPStr)] string sourceName);

		[DllImport (LibraryPath, EntryPoint = "sqlite3_backup_step", CallingConvention = CallingConvention.Cdecl)]
		public static extern Result BackupStep (IntPtr backup, int numPages);

		[DllImport (LibraryPath, EntryPoint = "sqlite3_backup_finish", CallingConvention = CallingConvention.Cdecl)]
		public static extern Result BackupFinish (IntPtr backup);

		public enum ColType : int
		{
			Integer = 1,
			Float = 2,
			Text = 3,
			Blob = 4,
			Null = 5
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
            public string SourceUrl { get; set; }
            public DateTime Time { get; set; }
            public object Content { get; set; }

            public string Html { get; set; }
        }

        public enum ContentType
        {
            RawText = 0,
            RawBinary,
            Image,
            HTML,
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
   source_url TEXT,
   type INTEGER NOT NULL,
   hash INTEGER NOT NULL,
   hits INTEGER DEFAULT 1,
   text_content TEXT,
   html_content TEXT,
   binary_content BLOB
);
CREATE INDEX data_table_name_idx ON data_table (name);
CREATE INDEX data_table_url_idx ON data_table (source_url);
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

        public SQLite3.Result Insert(string name, string contentHtml, string content, string sourceUrl)
        {
            int id = FindIDByHash(content.GetHashCode());
            IntPtr stmt = SQLite3.Prepare2(mDB, id > 0 ?
               "UPDATE data_table SET ts = ?, hits = hits + 1 WHERE id = ?;" :
               "INSERT INTO data_table (ts, name, type, hash, html_content, text_content, source_url) VALUES (?, ?, ?, ?, ?, ?, ?);");

            if (string.IsNullOrWhiteSpace(name))
                name = Helper.GetDefaultName();

            if (contentHtml == null)
                contentHtml = "";

            SQLite3.BindInt(stmt, 1, Helper.UnixTimestamp());
            if (id > 0)
            {
                SQLite3.BindInt(stmt, 2, id);
            }
            else
            {
                SQLite3.BindText(stmt, 2, name);
                SQLite3.BindInt(stmt, 3, (int)ContentType.HTML);
                SQLite3.BindInt(stmt, 4, content.GetHashCode());
                SQLite3.BindText(stmt, 5, contentHtml);
                SQLite3.BindText(stmt, 6, content);
                SQLite3.BindText(stmt, 7, sourceUrl);
            }

            var res = SQLite3.Step(stmt);
            SQLite3.Finalize(stmt);
            return res;
        }

        public SQLite3.Result Insert(string name, string content, string sourceUrl = "", ContentType ct = ContentType.RawText)
        {
            int id = FindIDByHash(content.GetHashCode());
            IntPtr stmt = SQLite3.Prepare2(mDB, id > 0 ?
               "UPDATE data_table SET ts = ?, hits = hits + 1 WHERE id = ?;" :
               "INSERT INTO data_table (ts, name, type, hash, text_content, source_url) VALUES (?, ?, ?, ?, ?, ?);");

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
                SQLite3.BindInt(stmt, 3, (int)ct);
                SQLite3.BindInt(stmt, 4, content.GetHashCode());
                SQLite3.BindText(stmt, 5, content);
                SQLite3.BindText(stmt, 6, sourceUrl);
            }

            var res = SQLite3.Step(stmt);
            SQLite3.Finalize(stmt);
            return res;
        }

        public SQLite3.Result Insert(string name, System.Drawing.Image content, string sourceUrl = "")
        {
            if (content == null) return SQLite3.Result.Abort;
            MemoryStream buf = new MemoryStream();
            content.Save(buf, System.Drawing.Imaging.ImageFormat.Png);
            return Insert(null, buf.GetBuffer(), Database.ContentType.Image, sourceUrl);
        }

        public SQLite3.Result Insert(string name, byte[] content, ContentType contentType, string sourceUrl = "")
        {
            int hash = (int)Helper.Crc32(content);
            int id = FindIDByHash(hash);
            IntPtr stmt = SQLite3.Prepare2(mDB, id > 0 ?
               "UPDATE data_table SET ts = ?, hits = hits + 1 WHERE id = ?;" :
               "INSERT INTO data_table (ts, name, type, hash, binary_content, source_url) VALUES (?, ?, ?, ?, ?, ?);");
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
                SQLite3.BindText(stmt, 6, sourceUrl);
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

        public SQLite3.Result UpdateContent(int id, string newContent)
        {
            IntPtr stmt = SQLite3.Prepare2(mDB, "UPDATE data_table SET text_content = ?, source_url = ? WHERE id = ?");
            SQLite3.BindText(stmt, 1, newContent);
            SQLite3.BindText(stmt, 2, Helper.GetHostFromUri(newContent) != "" ? newContent : "");
            SQLite3.BindInt(stmt, 3, id);
            var res = SQLite3.Step(stmt);
            SQLite3.Finalize(stmt);
            return res;
        }

        public SQLite3.Result UpdateContent(int id, System.Drawing.Image img)
        {
            MemoryStream buf = new MemoryStream();
            img.Save(buf, System.Drawing.Imaging.ImageFormat.Png);

            IntPtr stmt = SQLite3.Prepare2(mDB, "UPDATE data_table SET binary_content = ? WHERE id = ?");
            SQLite3.BindBlob(stmt, 1, buf.GetBuffer(), (int)buf.Length, (IntPtr)(-1));
            SQLite3.BindInt(stmt, 2, id);
            var res = SQLite3.Step(stmt);
            SQLite3.Finalize(stmt);
            return res;
        }

        public SQLite3.Result Delete(int id)
        {
            IntPtr stmt = SQLite3.Prepare2(mDB, id == -1 ?
                "DELETE FROM data_table;" :
                "DELETE FROM data_table WHERE id = ?");
            if (id > -1)
                SQLite3.BindInt(stmt, 1, id);
            var res = SQLite3.Step(stmt);
            SQLite3.Finalize(stmt);
            return res;
        }

        public int TotalEntries(string where = "1 = 1")
        {
            IntPtr stmt = SQLite3.Prepare2(mDB, "SELECT COUNT(id) FROM data_table WHERE " + where + ";");
            int count = 0;
            if (SQLite3.Step(stmt) == SQLite3.Result.Row)
                count = SQLite3.ColumnInt(stmt, 0);
            SQLite3.Finalize(stmt);
            return count;
        }

        public Entry[] Paging(string where, string orderByFields, int start, int length)
        {
            if (string.IsNullOrWhiteSpace(where))
                where = "1 = 1";
            if (string.IsNullOrWhiteSpace(orderByFields))
                orderByFields = "ts DESC, id DESC";

            List<Entry> entries = new List<Entry>(length);
            IntPtr stmt = SQLite3.Prepare2(mDB, string.Format(
               "SELECT id, ts, name, type, text_content, binary_content, hits, source_url, html_content FROM data_table WHERE {0} ORDER BY {1} LIMIT {2}, {3};",
               where, orderByFields, start, length));
            while (SQLite3.Step(stmt) == SQLite3.Result.Row)
            {
                Entry e = new Entry();
                e.Id = SQLite3.ColumnInt(stmt, 0);
                e.Time = new DateTime(1970, 1, 1).AddSeconds(SQLite3.ColumnInt(stmt, 1)).ToLocalTime();
                e.Name = SQLite3.ColumnString(stmt, 2);
                e.Type = (ContentType)(SQLite3.ColumnInt(stmt, 3));
                switch (e.Type)
                {
                    case ContentType.RawText:
                    case ContentType.HTML:
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
                e.SourceUrl = SQLite3.ColumnString(stmt, 7);
                e.Html = SQLite3.ColumnString(stmt, 8);
                entries.Add(e);
            }
            SQLite3.Finalize(stmt);
            return entries.ToArray();
        }
    }
}
