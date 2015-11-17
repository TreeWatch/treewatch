using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
	public class Block : PolygonModel
	{
		[ForeignKey (typeof(Field))]
		public int FieldId { get; set; }

		[ManyToMany (typeof(BlockToDo))]
		public List<ToDo> ToDos { get; set; }

		public TreeType TreeType { get; set; }

		public Block (List<Position> boundingCoordinates, TreeType treeType)
		{
			BoundingCoordinates = boundingCoordinates;
			TreeType = treeType;
		}

		public Block ()
		{
		}
	}
}

