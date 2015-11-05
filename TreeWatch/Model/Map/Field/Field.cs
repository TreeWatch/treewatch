using System;
using System.Collections.Generic;

using Xamarin.Forms.Maps;

namespace TreeWatch
{
	public class Field
	{
		private List<Position> pos;

		public String Name { get; private set; }

		public Field (String name)
		{
			this.Name = name;
			pos = new List<Position>();
		}

		public List<Position> BoundingCordinates {
			get{ return pos;}
			set{ this.pos = value; }
		}
	}
}

