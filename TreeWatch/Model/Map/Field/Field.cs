using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

namespace TreeWatch
{
	public class Field
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		[OneToMany (CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
		public List<PositionModel> BoundingCordinates { get; set; }

		[OneToMany (CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
		public List<Block> Blocks { get; set; }

		public String Name { get; set; }

		public Field (string name, List<PositionModel> boundingCordinates, List<Block> blocks)
		{
			Name = name;
			BoundingCordinates = boundingCordinates;
			Blocks = blocks;
		}

		public Field()
		{
		}


	}
}

