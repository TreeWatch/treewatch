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

		public struct WidthHeight
		{
			public double Width;
			public double Height;

		}

		public static WidthHeight CalculateWidthHeight(List<Position> BoundingCoordinates)
		{
			double smallestLon = 0.0;
			double biggestLon = 0.0;
			double smallestLat = 0.0;
			double biggestLat = 0.0;

			foreach (Position fieldPoint in BoundingCoordinates)
			{
				if (smallestLon == 0.0 && biggestLon == 0.0 && smallestLat == 0.0 && biggestLat == 0.0)
				{
					smallestLon = fieldPoint.Longitude;
					biggestLon = fieldPoint.Longitude;
					smallestLat = fieldPoint.Latitude;
					biggestLat = fieldPoint.Latitude;
				}
				if (fieldPoint.Longitude < smallestLon)
				{
					smallestLon = fieldPoint.Longitude;
				}
				if (fieldPoint.Longitude > biggestLon)
				{
					biggestLon = fieldPoint.Longitude;
				}
				if (fieldPoint.Latitude < smallestLat)
				{
					smallestLat = fieldPoint.Latitude;
				}
				if (fieldPoint.Latitude > biggestLat)
				{
					biggestLat = fieldPoint.Latitude;
				}
			}

			return new WidthHeight { Width = biggestLon - smallestLon, Height = biggestLat - smallestLat };
		}
			
		public static Position CalculateCenter (List<Position> BoundingCoordinates) 
		{
			double coordinateCounter = 0.0;
			double latSum = 0.0;
			double lonSum = 0.0;

			foreach (Position fieldPoint in BoundingCoordinates)
			{
				coordinateCounter++;
				latSum += fieldPoint.Latitude;
				lonSum += fieldPoint.Longitude;
			}

			return new Position (latSum / coordinateCounter, lonSum / coordinateCounter);
		}

	}
}

