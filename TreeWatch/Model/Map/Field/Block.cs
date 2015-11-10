using System;
using Xamarin.Forms.Maps;
using System.Collections.Generic;

namespace TreeWatch
{
	public class Block
	{
		List<PositionModel> boundingRectangle;

		public PositionModel StartingPoint {
			get;
			private set;
		}

		public PositionModel EndPoint {
			get;
			private set;
		}

		public TreeType TreeType {
			get;
			private set;
		}

		public List<PositionModel> BoundingRectangle {
			get {
				if (boundingRectangle == null) {
					var list = new List<PositionModel> ();
					list.Add (this.StartingPoint);
					list.Add (new PositionModel (StartingPoint.Latitude, EndPoint.Longitude));
					list.Add (this.EndPoint);
					list.Add (new PositionModel (EndPoint.Latitude, StartingPoint.Longitude));
					boundingRectangle = list;
				}
				return boundingRectangle;
			}
		}

		public Block (PositionModel StartingPoint, PositionModel EndPoint, TreeType TreeType)
		{
			this.StartingPoint = StartingPoint;
			this.EndPoint = EndPoint;
			this.TreeType = TreeType;
		}
	}
}

