using System;
using System.IO;

using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;

using TreeWatch.Droid;

using Xamarin.Forms;


[assembly: Dependency(typeof(SQLiteAndroid))]

namespace TreeWatch.Droid
{
    public class SQLiteAndroid: ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            const string fileName = "TreeWatchDB.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);

            var platform = new SQLitePlatformAndroid();
            var connection = new SQLiteConnection(platform, path);

            return connection;
        }
    }
}

