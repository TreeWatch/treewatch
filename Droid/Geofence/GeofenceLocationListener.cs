using System;
using Android.Gms.Location;
using Android.OS;

namespace TreeWatch.Droid
{
    public class GeofenceLocationListener : Java.Lang.Object, ILocationListener
    {
        private static GeofenceLocationListener sharedInstance = new GeofenceLocationListener();

        /// <summary>
        /// Location listener instance
        /// </summary>
        public static GeofenceLocationListener SharedInstance { get { return sharedInstance; } }

        private GeofenceLocationListener()
        {

        }
        void Android.Gms.Location.ILocationListener.OnLocationChanged(Android.Locations.Location location)
        {
            //Location Updated
            System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}: {2},{3}",CrossGeofence.Id,"Location Update",location.Latitude,location.Longitude));
            ((GeofenceImplementation)CrossGeofence.Current).SetLastKnownLocation(location);
            /*
            GeofenceCircularRegion region = null;
            CrossGeofence.Current.Regions.TryGetValue("Fontys", out region);
            System.Diagnostics.Debug.WriteLine(region.Radius);
            */
        }
    }
}

