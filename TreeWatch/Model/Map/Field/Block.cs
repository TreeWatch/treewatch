using System.Collections.Generic;
using SQLite;

namespace TreeWatch
{
	public class Block
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public TreeType TreeType { get; set; }

		public List<PositionModel> BoundingCordinates { get; set; }

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

