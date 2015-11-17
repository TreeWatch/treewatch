using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
	public class ToDo : BaseModel
	{
		public string Title { get; set; }

		public string Description { get; set; }

		[ManyToMany (typeof(BlockToDo))]
		public List<Block> Blocks { get; set; }

		[OneToMany (CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
		public List<UserToDo> ToDos {
			get;
			set;
		}

		public ToDo (string title, string description)
		{
			Description = description;
			Title = title;
		}

		public ToDo () 
		{
		}
	}
}

