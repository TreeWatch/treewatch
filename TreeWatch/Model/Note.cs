using System;
using SQLite;

namespace TreeWatch
{
	public class Note
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; private set; }

		public string title { get; private set; }
		public string desc { get; private set; }
		public string imagePath { get; private set; }
		public DateTime DateTime { get; private set; }
		public PositionModel PositionModel { get; private set; }

		public Note ()
		{
		}
	}
}

