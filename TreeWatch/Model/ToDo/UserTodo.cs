using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace TreeWatch
{
	public class UserToDo
	{
		[ForeignKey(typeof(User))]
		public int UserId { get; set; }

		[ForeignKey(typeof(ToDo))]
		public int ToDoId{ get; set; }

		[OneToMany (CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
		public List<Hours> Hours{ get; set; }

	}
}

