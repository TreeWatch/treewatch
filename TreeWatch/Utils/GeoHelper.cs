using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace TreeWatch
{
	public static class GeoHelper
	{

		//coords is the list of positions which are the boundaries of the field
		// position is the position to check if inside the boundaries

		public static Boolean IsInsideCoords(List<Position> coords, Position position){
			int i, j;
			int nvert = coords.Count;

			bool inside = false;

			if (coords.Count < 3)
			{
				return inside;
			}

			for (i = 0, j = nvert - 1; i < nvert; j = i++)
			{
				if (((coords[i].Latitude > position.Latitude) != (coords[j].Latitude > position.Latitude)) &&
					(position.Longitude < (coords[j].Longitude - coords[i].Longitude) * (position.Latitude - coords[i].Latitude) / (coords[j].Latitude - coords[i].Latitude) + coords[i].Longitude))
					inside = !inside; 
			}
			return inside;

		}
	}
}

