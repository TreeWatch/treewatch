using System;
using System.Collections.Generic;

using Xamarin.Forms.Maps;
using SQLite;

namespace TreeWatch
{
	public class Field
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public List<PositionModel> BoundingCordinates {
			get;
			set;
		}

		public List<Block> Rows {
			get;
			set;
		}

		public String Name { get; private set; }

		public Field (String name)
		{
			this.Name = name;
			BoundingCordinates = new List<PositionModel> ();
			Rows = new List<Block> ();
		}


	}
}

