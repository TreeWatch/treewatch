using System;
using Xamarin.Forms;
using TreeWatch.Droid;
using System.IO;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;


[assembly: Dependency (typeof(SQLite_Android))]

namespace TreeWatch.Droid
{
	public class SQLite_Android: ISQLite
	{
		public SQLiteConnection GetConnection ()
		{
			const string fileName = "TreeWatchDB.db3";
			var documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var path = Path.Combine (documentsPath, fileName);

			var platform = new SQLitePlatformAndroid ();
			var connection = new SQLiteConnection (platform, path);

			return connection;
		}
	}
}

