using System;

namespace TreeWatch
{
    public class GeofenceLocation
    {
        public GeofenceLocation()
        {
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Date { get; set; }
        public double Accuracy { get; set; }
    }
}

