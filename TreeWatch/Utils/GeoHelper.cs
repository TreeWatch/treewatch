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

