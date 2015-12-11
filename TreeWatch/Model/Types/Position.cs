namespace TreeWatch
{
    /// <summary>
    /// Class that has a latitude and longitude, stored as Doubles.
    /// </summary>
    public class Position
    {
        public Position()
        {
        }

        public Position(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude { get; set; }

        public static implicit operator Xamarin.Forms.Maps.Position(Position pos)
        {
            return new Xamarin.Forms.Maps.Position(pos.Latitude, pos.Longitude);
        }

        public static implicit operator Position(Xamarin.Forms.Maps.Position pos)
        {
            return new Position(pos.Latitude, pos.Longitude);
        }

    }
}