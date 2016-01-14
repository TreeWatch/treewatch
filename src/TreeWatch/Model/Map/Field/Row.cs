using System;
using Xamarin.Forms.Maps;
using System.Collections.Generic;

namespace TreeWatch
{
	public class Row
	{
		List<Position> boundingRectangle;

		public Position StartingPoint {
			get;
			private set;
		}

		public Position EndPoint {
			get;
			private set;
		}

		public TreeType TreeType {
			get;
			private set;
		}

		public List<Position> BoundingRectangle {
			get {
				if (boundingRectangle == null) {
					var list = new List<Position> ();
					list.Add (StartingPoint);
					list.Add (new Position (StartingPoint.Latitude, EndPoint.Longitude));
					list.Add (EndPoint);
					list.Add (new Position (EndPoint.Latitude, StartingPoint.Longitude));
					boundingRectangle = list;
				}
				return boundingRectangle;
			}
		}

		public Row (Position StartingPoint, Position EndPoint, TreeType TreeType)
		{
			this.StartingPoint = StartingPoint;
			this.EndPoint = EndPoint;
			this.TreeType = TreeType;
		}
	}
}

