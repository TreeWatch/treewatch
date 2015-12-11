using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms.Maps;

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
            public Position Center;
            public double WidthMeters;
            public double HeightMeters;
		}

		public static WidthHeight CalculateWidthHeight(List<Position> boundingCoordinates)
		{
            if (boundingCoordinates.Count < 2)
                return new WidthHeight { Width = 0d, Height = 0d };
            double smallestLon = boundingCoordinates[0].Longitude;
            double biggestLon = smallestLon;
            double smallestLat = boundingCoordinates[0].Latitude;
            double biggestLat = smallestLat;

            for(int i = 1; i < boundingCoordinates.Count; i++)
			{
                if (boundingCoordinates[i].Longitude < smallestLon)
				{
                    smallestLon = boundingCoordinates[i].Longitude;
				}
                if (boundingCoordinates[i].Longitude > biggestLon)
				{
                    biggestLon = boundingCoordinates[i].Longitude;
				}
                if (boundingCoordinates[i].Latitude < smallestLat)
				{
                    smallestLat = boundingCoordinates[i].Latitude;
				}
                if (boundingCoordinates[i].Latitude > biggestLat)
				{
                    biggestLat = boundingCoordinates[i].Latitude;
				}
			}
            double width = biggestLon - smallestLon;
            double height = biggestLat - smallestLat;
            Position center = new Position(smallestLat + height * 0.5, smallestLon + width * 0.5);
            return new WidthHeight { 
                Width = width,
                Height = height,
                Center = center,
                WidthMeters = DistanceInMeters(smallestLat, smallestLon, smallestLat, biggestLon),
                HeightMeters = DistanceInMeters(smallestLat, smallestLon, biggestLat, smallestLon)
            };
		}

        public static double DistanceInMeters(double lat1, double lng1, double lat2, double lng2)
        {
            const double earthRadius = 6371000; //meters
            double dLat =  ToRadians(lat2-lat1);
            double dLng = ToRadians(lng2-lng1);
            double a = Math.Sin(dLat/2) * Math.Sin(dLat/2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLng/2) * Math.Sin(dLng/2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));
            float dist = (float) (earthRadius * c);

            return dist;
        }

        public static double ToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

	}
}

