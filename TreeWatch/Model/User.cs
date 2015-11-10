using System;
using SQLite;

namespace TreeWatch
{
	public class User
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; private set; }

		public string firstName { get; private set; }
		public string lastName { get; private set; }
		public string username { get; private set; }
		public string password { get; private set; }
		public string email { get; private set; }

		public User ()
		{
		}
	}
}

