using System;
using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
	public class DBHelper
	{
		public DBHelper ()
		{
		}
	}

	public class FieldPosition
	{
		[ForeignKey(typeof(Field))]
		public int FieldId { get; set; }

		[ForeignKey(typeof(PositionModel))]
		public int PositionId { get; set; }
	}

	public class BlockPosition
	{
		[ForeignKey(typeof(Block))]
		public int BlockId { get; set; }

		[ForeignKey(typeof(PositionModel))]
		public int PositionId { get; set; }
	}

	public class BlockToDo
	{
		[ForeignKey(typeof(Block))]
		public int BlockId { get; set; }

		[ForeignKey(typeof(ToDo))]
		public int ToDoId { get; set; }
	}

	public class UserToDo
	{
		[ForeignKey(typeof(User))]
		public int UserId { get; set; }

		[ForeignKey(typeof(ToDo))]
		public int ToDoId{ get; set; }


	}
}

