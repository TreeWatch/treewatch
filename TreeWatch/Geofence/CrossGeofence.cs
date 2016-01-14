// <copyright file="CrossGeofence.cs" company="TreeWatch">
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
namespace TreeWatch
{
    using System;

    using Xamarin.Forms;

    /// <summary>
    /// Cross geofence.
    /// </summary>
    public static class CrossGeofence
    {
        /// <summary>
        /// Plugin id
        /// </summary>
        public const string Id = "CrossGeofence";

        /// <summary>
        /// The implementation.
        /// </summary>
        private static Lazy<IGeofence> implementation = new Lazy<IGeofence>(CreateGeofence, System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Gets the geofence state events listener.
        /// </summary>
        /// <value>The geofence listener.</value>
        public static IGeofenceListener GeofenceListener { get; private set; }

        /// <summary>
        /// Gets or sets the geofence priority.
        /// </summary>
        /// <value>The geofence priority.</value>
        public static GeofencePriority GeofencePriority { get; set; }

        /// <summary>
        /// Gets or sets the smallest displacement for location updates.
        /// </summary>
        /// <value>The smallest displacement.</value>
        public static float SmallestDisplacement { get; set; }

        /// <summary>
        /// Gets or sets the location updates interval.
        /// </summary>
        /// <value>The location updates interval.</value>
        public static int LocationUpdatesInterval { get; set; }

        /// <summary>
        /// Gets or sets the fastest location updates interval.
        /// </summary>
        /// <value>The fastest location updates interval.</value>
        public static int FastestLocationUpdatesInterval { get; set; }

        /// <summary>
        /// Gets a value indicating whether is initialized.
        /// </summary>
        /// <value><c>true</c> if is initialized; otherwise, <c>false</c>.</value>
        public static bool IsInitialized
        {
            get
            {
                return GeofenceListener != null;
            }
        }

        /// <summary>
        /// Gets the current settings to use.
        /// </summary>
        /// <value>The current.</value>
        public static IGeofence Current
        {
            get
            {
                // Should always initialize plugin before use
                if (!CrossGeofence.IsInitialized)
                {
                    throw GeofenceNotInitializedException();
                }

                var ret = implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }

                return ret;
            }
        }

        /// <summary>
        /// Initialize the specified priority and smallestDisplacement for the geofence plugin.
        /// </summary>
        /// <param name="priority">Geofence priority.</param>
        /// <param name="smallestDisplacement">Smallest displacement.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void Initialize<T>(GeofencePriority priority = GeofencePriority.BalancedPower, float smallestDisplacement = 0)
            where T : IGeofenceListener, new()
        {
            if (GeofenceListener == null)
            {
                GeofenceListener = (IGeofenceListener)Activator.CreateInstance(typeof(T));
            }

            GeofencePriority = priority;
            SmallestDisplacement = smallestDisplacement;
        }

        /// <summary>
        /// Nots the implemented in reference assembly.
        /// </summary>
        /// <returns>The implemented in reference assembly.</returns>
        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }

        /// <summary>
        /// Geofences the not initialized exception.
        /// </summary>
        /// <returns>The not initialized exception.</returns>
        internal static GeofenceNotInitializedException GeofenceNotInitializedException()
        {
            string description = string.Format("{0} - {1}", CrossGeofence.Id, "Plugin is not initialized. Should initialize before use with CrossGeofence Initialize method. Example:  CrossGeofence.Initialize<CrossGeofenceListener>()");

            return new GeofenceNotInitializedException(description);
        }

        /// <summary>
        /// Creates the geofence.
        /// </summary>
        /// <returns>The geofence.</returns>
        private static IGeofence CreateGeofence()
        {
            IGeofence geo = DependencyService.Get<IGeofence>();

            return geo;
        }
    }
}