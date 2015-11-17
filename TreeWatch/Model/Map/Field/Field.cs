using System;
using System.Collections.Generic;

using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;
using System.Runtime.Serialization;

namespace TreeWatch
{
	public class Field : PolygonModel
	{
		[OneToMany (CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
		public List<Block> Blocks { get; set; }

		public String Name { get; set; }

		public Field (string name, List<Position> boundingCoordinates, List<Block> blocks)
		{
			Name = name;
			BoundingCoordinates = boundingCoordinates;
			Blocks = blocks;
		}

		public Field ()
		{
		}

		[IgnoreAttribute]
		public Position CalculatePinPosition {
			get {
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

		public struct WidthHeight
		{
			public double Width;
			public double Height;
		
		}

		[IgnoreAttribute]
		public WidthHeight CalculateWidthHeight {
			get {
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
		}
	}
}

