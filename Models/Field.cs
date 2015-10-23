using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace TreeWatch
{
	public class Field
	{
		private List<Position> pos;
		public Field ()
		{
			pos = new List<Position>();
		}

		public List<Position> BoundingCordinates {
			get{ return pos;}
			set{ this.pos = value; }
		}
	}
}

