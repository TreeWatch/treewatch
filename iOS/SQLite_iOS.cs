using TreeWatch;
using System;
using System.IO;
using Xamarin.Forms;
using SQLite.Net.Interop;

[assembly: Dependency (typeof (SQLite_iOS))]

public class SQLite_iOS : ISQLite {
	public SQLite_iOS () {}
	public SQLite.Net.SQLiteConnection GetConnection ()
	{
		var sqliteFilename = "TreeWatchDB.db3";
		string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
		string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
		var path = Path.Combine(libraryPath, sqliteFilename);
		// Create the connection

		var conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS(), path);
		// Return the database connection
		return conn;
	}}

