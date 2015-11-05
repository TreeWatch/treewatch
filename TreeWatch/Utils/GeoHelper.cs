using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace TreeWatch
{
	public static class GeoHelper
	{
		public static Boolean isInsideCoords(List<Position> coords, Position position){
			Position p1, p2;

			bool inside = false;

			if (coords.Count < 3)
			{
				return inside;
			}

			var oldPos = new Position(
				coords[coords.Count -1].Latitude, coords[coords.Count - 1].Longitude);

			foreach(var pos in coords){

				if (pos.Latitude > oldPos.Latitude) {
					p1 = oldPos;
					p2 = pos;

				} else {
					p1 = pos;
					p2 = oldPos;
				}

				if ((pos.Latitude < position.Latitude) == (position.Latitude <= oldPos.Latitude)
					&& (pos.Longitude - (long) p1.Longitude)*(p2.Latitude - p1.Latitude)
					< (p2.Longitude - (long) p1.Longitude)*(pos.Latitude - p1.Latitude))
				{
					inside = !inside;
				}

				oldPos = pos;
			}

			return inside;
		}
	}
}

