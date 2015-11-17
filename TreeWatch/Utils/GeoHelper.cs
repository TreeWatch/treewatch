using System;
using System.Collections.Generic;

namespace TreeWatch
{
	public static class GeoHelper
	{
		/// <summary>
		/// Determines if a position is inside the polygon defined by the cordinates.
		/// </summary>
		/// <returns><c>true</c> is inside the polygon defined by the cordinates; otherwise, <c>false</c>.</returns>
		/// <param name="cordinates">cordinates of polygon.</param>
		/// <param name="position">Position.</param>
		public static Boolean IsInsideCoords (List<Position> cordinates, Position position)
		{
			int i, j;
			int nvert = cordinates.Count;

			bool inside = false;

			if (cordinates.Count < 3) {
				return inside;
			}

			for (i = 0, j = nvert - 1; i < nvert; j = i++) {
				if (((cordinates [i].Latitude > position.Latitude) != (cordinates [j].Latitude > position.Latitude)) &&
				    (position.Longitude < (cordinates [j].Longitude - cordinates [i].Longitude) * (position.Latitude - cordinates [i].Latitude) / (cordinates [j].Latitude - cordinates [i].Latitude) + cordinates [i].Longitude))
					inside = !inside; 
			}
			return inside;

		}
	}
}

