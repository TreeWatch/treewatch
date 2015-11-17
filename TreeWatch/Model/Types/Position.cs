namespace TreeWatch
{
	public class Position
	{
		public Position ()
		{
		}

		public Position (double latitude, double longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}

		public double Latitude { get; set; }
		public double Longitude { get; set; }

		public static implicit operator Xamarin.Forms.Maps.Position(Position pos)
		{
			return new Xamarin.Forms.Maps.Position(pos.Latitude, pos.Longitude);
		}

		public static implicit operator Position(Xamarin.Forms.Maps.Position pos)
		{
			return new Position (pos.Latitude, pos.Longitude);
		}

	}
}

