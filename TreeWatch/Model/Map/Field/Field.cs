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
			double smallestLon = boundingCoordinates[0].Longitude;
			double biggestLon = boundingCoordinates[0].Longitude;
			double smallestLat = boundingCoordinates [0].Latitude;
			double biggestLat = boundingCoordinates [0].Latitude;

			foreach(Position fieldPoint in boundingCoordinates)
			{
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

