using System;
using SQLite;

namespace TreeWatch
{
	public class ToDo
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; private set; }
		public string Title { get; private set; }
		public string Desc { get; private set; }

		public ToDo ()
		{
		}

		public ToDo (string title, string desc){
			this.Desc = desc;
			this.Title = title;
		}

	}
}

