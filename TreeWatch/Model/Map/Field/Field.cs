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
			get;
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

