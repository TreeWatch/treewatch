using System;
using SQLite;
using SQLite.Net.Attributes;

namespace TreeWatch
{
	public class Note
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public string ImagePath { get; set; }

		public DateTime TimeStamp { get; set; }

		public PositionModel Position { get; set; }

		public Note (string title, string description, string imagePath, DateTime timeStamp, PositionModel position)
		{
			Title = title;
			Description = description;
			ImagePath = imagePath;
			TimeStamp = timeStamp;
			Position = position;
		}

		public Note()
		{
		}
	}
}

