using System;
using System.Collections.Generic;

namespace TreeWatch
{
    /// <summary>
    /// Helper class for geo related functions.
    /// </summary>
    public static class GeoHelper
    {
        /// <summary>
        /// Determines if a position is inside the polygon defined by the cordinates.
        /// </summary>
        /// <returns><c>true</c> is inside the polygon defined by the cordinates; otherwise, <c>false</c>.</returns>
        /// <param name="cordinates">cordinates of polygon.</param>
        /// <param name="position">Position.</param>
        public static Boolean IsInsideCoords(List<Position> cordinates, Position position)
        {
            int i, j;
            int nvert = cordinates.Count;

            bool inside = false;

            if (cordinates.Count < 3)
            {
                return inside;
            }

            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((cordinates[i].Latitude > position.Latitude) != (cordinates[j].Latitude > position.Latitude)) &&
                    (position.Longitude < (cordinates[j].Longitude - cordinates[i].Longitude) * (position.Latitude - cordinates[i].Latitude) / (cordinates[j].Latitude - cordinates[i].Latitude) + cordinates[i].Longitude))
                    inside = !inside;
            }
            return inside;

        }

        /// <summary>
        /// HelperStruct to return a box.
        /// </summary>
        public struct BoundingBox
        {
            /// <summary>
            /// The width.
            /// </summary>
            public double Width;

            /// <summary>
            /// The height.
            /// </summary>
            public double Height;

            /// <summary>
            /// The center.
            /// </summary>
            public Position Center;

            /// <summary>
            /// The width in meters.
            /// </summary>
            public double WidthInMeters;

            /// <summary>
            /// The height in meters.
            /// </summary>
            public double HeightInMeters;
        }

        /// <summary>
        /// Calculates the bounding box for a list of coordinates.
        /// </summary>
        /// <returns>The bounding box.</returns>
        /// <param name="boundingCoordinates">List of coordinates.</param>
        public static BoundingBox CalculateBoundingBox(List<Position> boundingCoordinates)
        {
            if (boundingCoordinates.Count < 2)
                return new BoundingBox { Width = 0d, Height = 0d };
            
            double smallestLongitude = boundingCoordinates[0].Longitude;
            double biggestLongitude = smallestLongitude;
            double smallestLatitude = boundingCoordinates[0].Latitude;
            double biggestLatitude = smallestLatitude;

            for (int i = 1; i < boundingCoordinates.Count; i++)
            {
                if (boundingCoordinates[i].Longitude < smallestLongitude)
                {
                    smallestLongitude = boundingCoordinates[i].Longitude;
                }
                if (boundingCoordinates[i].Longitude > biggestLongitude)
                {
                    biggestLongitude = boundingCoordinates[i].Longitude;
                }
                if (boundingCoordinates[i].Latitude < smallestLatitude)
                {
                    smallestLatitude = boundingCoordinates[i].Latitude;
                }
                if (boundingCoordinates[i].Latitude > biggestLatitude)
                {
                    biggestLatitude = boundingCoordinates[i].Latitude;
                }
            }
            double width = biggestLongitude - smallestLongitude;
            double height = biggestLatitude - smallestLatitude;
            var center = new Position(smallestLatitude + height * 0.5, smallestLongitude + width * 0.5);

            return new BoundingBox
            {
                Width = width,
                Height = height,
                Center = center,
                WidthInMeters = DistanceInMeters(smallestLatitude, smallestLongitude, smallestLatitude, biggestLongitude),
                HeightInMeters = DistanceInMeters(smallestLatitude, smallestLongitude, biggestLatitude, smallestLongitude)
            };
        }

        /// <summary>
        /// Calculates the distances between two coordinates the in meters.
        /// </summary>
        /// <returns>The in meters.</returns>
        /// <param name="lat1">Latitude1.</param>
        /// <param name="long1">Longitude1.</param>
        /// <param name="lat2">Latitude2.</param>
        /// <param name="long2">Longitude2.</param>
        public static double DistanceInMeters(double lat1, double long1, double lat2, double long2)
        {
            const double earthRadius = 6371000; //meters
            double dLat = ToRadians(lat2 - lat1);
            double dLng = ToRadians(long2 - long1);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                       Math.Sin(dLng / 2) * Math.Sin(dLng / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            float dist = (float)(earthRadius * c);

            return dist;
        }

        /// <summary>
        /// Converts angle in degrees to radians
        /// </summary>
        /// <returns>The radians.</returns>
        /// <param name="angle">Angle.</param>
        public static double ToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}
