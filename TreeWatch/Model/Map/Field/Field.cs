﻿using System;
using System.Collections.Generic;

using Xamarin.Forms.Maps;

namespace TreeWatch
{
	public class Field
	{

		public List<Position> BoundingCordinates {
			get;
			set;
		}

		public List<Row> Rows {
			get;
			set;
		}

		public String Name { get; private set; }

		public Field (String name)
		{
			this.Name = name;
			BoundingCordinates = new List<Position> ();
			Rows = new List<Row> ();
		}


	}
}

