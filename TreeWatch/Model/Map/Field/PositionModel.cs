using SQLite;

namespace TreeWatch
{
	[Table("Position")]
	public class PositionModel
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public double Latitude { get; set; }

		public double Longitude { get; set; }

		public PositionModel (double latitude, double longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}

		public PositionModel()
		{
		}


	}
}

