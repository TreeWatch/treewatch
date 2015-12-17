// <copyright file="IGeofence.cs" company="TreeWatch">
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
    using System.Collections.Generic;

    /// <summary>
    /// I geofence.
    /// </summary>
    public interface IGeofence
    {
        /// <summary>
        /// Gets a dictionary that contains all regions been monitored.
        /// </summary>
        /// <value>The regions.</value>
        IReadOnlyDictionary<string, GeofenceCircularRegion> Regions { get; }

        /// <summary>
        /// Gets a Dicitonary that contains all geofence results received.
        /// </summary>
        /// <value>The geofence results.</value>
        IReadOnlyDictionary<string, GeofenceResult> GeofenceResults { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is monitoring.
        /// </summary>
        /// <value><c>true</c> if at least one instance is monitoring; otherwise, <c>false</c>.</value>
        bool IsMonitoring { get; }

        /// <summary>
        /// Gets the last known geofence location.
        /// </summary>
        /// <value>The last known location.</value>
        GeofenceLocation LastKnownLocation { get; }

        /// <summary>
        /// Starts the monitoring.
        /// </summary>
        /// <param name="region">Region that should be monitored.</param>
        void StartMonitoring(GeofenceCircularRegion region);

        /// <summary>
        /// Starts monitoring multiple regions.
        /// </summary>
        /// <param name="regions">Regions that should be monitored.</param>
        void StartMonitoring(IList<GeofenceCircularRegion> regions);

        /// <summary>
        /// Stops the monitoring.
        /// </summary>
        /// <param name="identifier">Identifier that should stops.</param>
        void StopMonitoring(string identifier);

        /// <summary>
        /// Stops monitoring multiple regions.
        /// </summary>
        /// <param name="identifier">List of identifiers that should stop.</param>
        void StopMonitoring(IList<string> identifier);

        /// <summary>
        /// Stops monitoring all regions
        /// </summary>
        void StopMonitoringAllRegions();
    }
}