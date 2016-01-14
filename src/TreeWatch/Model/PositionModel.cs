using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
	public class PositionModel : DBEntity
	{
		public double Latitude { get; set; }

		public double Longitude { get; set; }

		[ForeignKey (typeof(Field))]
		public int FieldId { get; set; }

		[ForeignKey (typeof(Block))]
		public int BlockId { get; set; }


		public PositionModel (double latitude, double longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}

		public PositionModel () 
		{
		}
	}
}

