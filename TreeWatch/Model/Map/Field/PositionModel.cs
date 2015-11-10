using System;
using Xamarin.Forms.Maps;
using SQLite;

namespace TreeWatch
{
	[Table("Position")]
	public class PositionModel
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public double Latitude { get; private set; }
		public double Longitude { get; private set; }

		public PositionModel ()
		{
		}

		public PositionModel (double longi, double lat)
		{
			this.Latitude = lat;
			this.Longitude = longi;
		}
	}
}

