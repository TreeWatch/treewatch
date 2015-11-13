using SQLite;
using SQLite.Net.Attributes;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
	public class ToDo
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		[ManyToMany (typeof(BlockToDo))]
		public List<Block> Blocks { get; set; }

		public ToDo (string title, string description)
		{
			Description = description;
			Title = title;
		}

		public ToDo (){
		}

	}
}

