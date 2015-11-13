using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

namespace TreeWatch
{
	public class Block
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		[OneToMany (CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
		public List<PositionModel> BoundingCordinates { get; set; }

		[ForeignKey(typeof(Field))]
		public int FieldId { get; set; }

		[ManyToMany(typeof(BlockToDo))]
		public List<ToDo> ToDos { get; set; }

		public TreeType TreeType { get; set; }

		public Block (List<PositionModel> boundingCordinates, TreeType treeType)
		{
			BoundingCordinates = boundingCordinates;
			TreeType = treeType;
		}

		public Block()
		{
		}
	}
}

