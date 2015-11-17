using System;
using System.Collections.Generic;

using Xamarin.Forms.Maps;
using System.Diagnostics;

namespace TreeWatch
{
	public class Field
	{
		private List<Position> boundingCoordinates;
		private Position fieldPinPosition;
		private double fieldHeightLat;
		private double fieldWithLon;

		public List<Position> BoundingCordinates {
			get
			{
				return boundingCoordinates;
			}
			set
			{
				boundingCoordinates = value;
				CalculatePinPosition ();
				CalculateWidthHeight ();
			}
		}

		public Position FieldPinPosition {
			get{ return fieldPinPosition; }
		}

		public double FieldWidthLon {
			get{ return fieldWithLon; }
		}

		public double FieldHeightLat {
			get{ return fieldHeightLat; }
		}

		public List<Row> Rows {
			get;
			set;
		}

		public String Name { get; private set; }

		public Field (String name)
		{
			this.Name = name;
			BoundingCordinates = new List<Position> ();
			Rows = new List<Row> ();
			fieldPinPosition = new Position ();
		}

		private void CalculateWidthHeight()
		{
			double smallestLon = 0.0;
			double biggestLon = 0.0;
			double smallestLat = 0.0;
			double biggestLat = 0.0;

			foreach(Position fieldPoint in boundingCoordinates)
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

			fieldWithLon = biggestLon - smallestLon;
			fieldHeightLat = biggestLat - smallestLat;
			Debug.WriteLine ("width: {0}, height: {1}", fieldWithLon, fieldHeightLat);
		}

		private void CalculatePinPosition()
		{
			double coordinateCounter = 0.0;
			double latSum = 0.0;
			double lonSum = 0.0;

			foreach(Position fieldPoint in boundingCoordinates)
			{
				coordinateCounter++;
				latSum += fieldPoint.Latitude;
				lonSum += fieldPoint.Longitude;
			}
			fieldPinPosition = new Position (latSum/coordinateCounter, lonSum/coordinateCounter);

		}
	}
}

