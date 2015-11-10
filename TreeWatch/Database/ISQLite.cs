using System;
using SQLite;

namespace TreeWatch
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}

