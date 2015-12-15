using System;

namespace TreeWatch
{
    public class GeofenceNotInitializedException : Exception
    {
        public GeofenceNotInitializedException()
        {
        }

        public GeofenceNotInitializedException(string message) : base(message)
        {

        }
    }
}

