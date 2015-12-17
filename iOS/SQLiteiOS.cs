using System;
using System.IO;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;
using TreeWatch;
using TreeWatch.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteIOS))]

// Analysis disable once InconsistentNaming
namespace TreeWatch.iOS
{
    /// <summary>
    /// SQLite iOS implementation.
    /// </summary>
    public class SQLiteIOS : ISQLite
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns>The connection.</returns>
        public SQLiteConnection GetConnection()
        {
            const string SqliteFilename = "TreeWatchDB.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, SqliteFilename);

            var conn = new SQLiteConnection(new SQLitePlatformIOS(), path);
            return conn;
        }
    }
}