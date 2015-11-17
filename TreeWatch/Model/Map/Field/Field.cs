using System;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;
using Xamarin.Forms.Maps;

namespace TreeWatch
{
	public class Field : PolygonModel
	{

		[OneToMany (CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
		public List<Block> Blocks { get; set; }

		public String Name { get; set; }

		public Field (string name, List<Position> boundingCordinates, List<Block> blocks)
		{
			Name = name;
			BoundingCordinates = boundingCordinates;
			Blocks = blocks;
		}

		public Field () 
		{
		}
	}
}

