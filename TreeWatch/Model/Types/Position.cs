namespace TreeWatch
{
    /// <summary>
    /// Class that has a latitude and longitude, stored as Doubles.
    /// Hides Xamarin.Forms.Maps.Position, since that can't be correctly serialized.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
        public Position()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.Position"/> class.
        /// </summary>
        /// <param name="latitude">Latitude.</param>
        /// <param name="longitude">Longitude.</param>
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

        /// <summary>
        /// Convert Postion to Xamarin.Forms.Maps.Position
        /// </summary>
        /// <param name="pos">Position.</param>
        public static implicit operator Xamarin.Forms.Maps.Position(Position pos)
        {
            return new Xamarin.Forms.Maps.Position(pos.Latitude, pos.Longitude);
        }

        /// <summary>
        /// Convert Xamarin.Forms.Maps.Postion to Position
        /// </summary>
        /// <param name="pos">Position.</param>
        public static implicit operator Position(Xamarin.Forms.Maps.Position pos)
        {
            return new Position(pos.Latitude, pos.Longitude);
        }

    }
}