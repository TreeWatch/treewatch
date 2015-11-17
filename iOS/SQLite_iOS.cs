using TreeWatch;
using System;
using System.IO;
using Xamarin.Forms;
using TreeWatch.iOS;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;

[assembly: Dependency (typeof(SQLite_iOS))]

namespace TreeWatch.iOS
{

	public class SQLite_iOS : ISQLite
	{
		public SQLiteConnection GetConnection ()
		{
			const string sqliteFilename = "TreeWatchDB.db3";
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine (libraryPath, sqliteFilename);

			var conn = new SQLiteConnection (new SQLitePlatformIOS (), path);
			return conn;
		}
	}
}