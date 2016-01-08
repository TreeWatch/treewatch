// <copyright file="GeofenceLocationListener.cs" company="TreeWatch">
// Copyright © 2015 TreeWatch
// </copyright>
#region Copyright
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
#endregion
namespace TreeWatch.Droid
{
    using Android.Gms.Location;

    /// <summary>
    /// Geofence location listener.
    /// </summary>
    public class GeofenceLocationListener : Java.Lang.Object, ILocationListener
    {
        /// <summary>
        /// The shared instance.
        /// </summary>
        private static GeofenceLocationListener sharedInstance = new GeofenceLocationListener();

        /// <summary>
        /// Prevents a default instance of the <see cref="TreeWatch.Droid.GeofenceLocationListener"/> class from being created.
        /// </summary>
        private GeofenceLocationListener()
        {
        }

        /// <summary>
        /// Gets the shared instance.
        /// </summary>
        /// <value>The shared instance.</value>
        public static GeofenceLocationListener SharedInstance
        { 
            get { return sharedInstance; }
        }

        /// <summary>
        /// Raises the location changed event.
        /// </summary>
        /// <param name="location">Location object.</param>
        void ILocationListener.OnLocationChanged(Android.Locations.Location location)
        {
            // Location Updated
            System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}: {2},{3}", CrossGeofence.Id, "Location Update", location.Latitude, location.Longitude));
            ((GeofenceImplementation)CrossGeofence.Current).SetLastKnownLocation(location);
            /*
            GeofenceCircularRegion region = null;
            CrossGeofence.Current.Regions.TryGetValue("Fontys", out region);
            System.Diagnostics.Debug.WriteLine(region.Radius);
            */
        }
    }
}