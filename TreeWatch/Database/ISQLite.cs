using System;
using SQLite;
using SQLite.Net;

namespace TreeWatch
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}

