// <copyright file="GeoHelper.cs" company="TreeWatch">
// Copyright © 2015 TreeWatch
// </copyright>
#region Copyright
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
#endregion
namespace TreeWatch
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Helper class for geo related functions.
    /// </summary>
    public static class GeoHelper
    {
        /// <summary>
        /// Determines if a position is inside the polygon defined by the coordinates.
        /// </summary>
        /// <returns><c>true</c> is inside the polygon defined by the coordinates; otherwise, <c>false</c>.</returns>
        /// <param name="coordinates">coordinates of polygon.</param>
        /// <param name="position">Position which should be tested if in.</param>
        public static bool IsInsideCoords(List<Position> coordinates, Position position)
        {
            int i, j;
            int nvert = coordinates.Count;

            bool inside = false;

            if (coordinates.Count < 3)
            {
                return inside;
            }

            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((coordinates[i].Latitude > position.Latitude) != (coordinates[j].Latitude > position.Latitude))
                    && (position.Longitude < ((coordinates[j].Longitude - coordinates[i].Longitude) * (position.Latitude - coordinates[i].Latitude) / ((coordinates[j].Latitude - coordinates[i].Latitude) + coordinates[i].Longitude))))
                {
                    inside = !inside;
                }
            }

            return inside;
        }

        /// <summary>
        /// Calculates the bounding box for a list of coordinates.
        /// </summary>
        /// <returns>The bounding box.</returns>
        /// <param name="boundingCoordinates">List of coordinates.</param>
        public static BoundingBox CalculateBoundingBox(List<Position> boundingCoordinates)
        {
            if (boundingCoordinates.Count < 2)
            {
                return new BoundingBox { Width = 0d, Height = 0d };
            }
            
            double smallestLongitude = boundingCoordinates[0].Longitude;
            double biggestLongitude = smallestLongitude;
            double smallestLatitude = boundingCoordinates[0].Latitude;
            double biggestLatitude = smallestLatitude;

            for (int i = 1; i < boundingCoordinates.Count; i++)
            {
                smallestLongitude = Math.Min(boundingCoordinates[i].Longitude, smallestLongitude);
                biggestLongitude = Math.Max(boundingCoordinates[i].Longitude, biggestLongitude);

                smallestLatitude = Math.Min(boundingCoordinates[i].Latitude, smallestLatitude);
                biggestLatitude = Math.Max(boundingCoordinates[i].Latitude, biggestLatitude);
            }

            double width = biggestLongitude - smallestLongitude;
            double height = biggestLatitude - smallestLatitude;
            var center = new Position(smallestLatitude + (height * 0.5), smallestLongitude + (width * 0.5));

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
        /// <param name="latitude1">Latitude of coordinate 1.</param>
        /// <param name="longitude1">Longitude of coordinate 1.</param>
        /// <param name="latitude2">Latitude of coordinate 2.</param>
        /// <param name="longitude2">Longitude of coordinate 2.</param>
        public static double DistanceInMeters(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            const double EarthRadius = 6371000; // meters
            double doubleLatitude = ToRadians(latitude2 - latitude1);
            double doubleLongitude = ToRadians(longitude2 - longitude1);
            double a = (Math.Sin(doubleLatitude / 2) * Math.Sin(doubleLatitude / 2)) +
                       (Math.Cos(ToRadians(latitude1)) * Math.Cos(ToRadians(latitude2)) * Math.Sin(doubleLongitude / 2) * Math.Sin(doubleLongitude / 2));
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            float dist = (float)(EarthRadius * c);

            return dist;
        }

        /// <summary>
        /// Converts angle in degrees to radians
        /// </summary>
        /// <returns>The radians.</returns>
        /// <param name="angle">Angle as double.</param>
        public static double ToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
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
    }
}