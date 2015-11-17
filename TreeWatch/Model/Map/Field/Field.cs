using System;
using System.Collections.Generic;

using Xamarin.Forms.Maps;
using System.Diagnostics;

using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
	public class Field : PolygonModel
	{
		private List<Position> boundingCoordinates;
		private Position fieldPinPosition;
		private double fieldHeightLat;
		private double fieldWithLon;


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

