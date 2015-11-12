using SQLite;

namespace TreeWatch
{
	public class ToDo
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public ToDo (string title, string description)
		{
			Description = description;
			Title = title;
		}

		public ToDo (){
		}

	}
}

