using System;
using System.Collections.Generic;

using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;
using System.Runtime.Serialization;

namespace TreeWatch
{
	public class Field : PolygonModel
	{
		[OneToMany (CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
		public List<Block> Blocks { get; set; }

		public String Name { get; set; }

		public Field (string name, List<Position> boundingCoordinates, List<Block> blocks)
		{
			Name = name;
			BoundingCoordinates = boundingCoordinates;
			Blocks = blocks;
		}		

		public Field ()
		{
		}
	}
}

