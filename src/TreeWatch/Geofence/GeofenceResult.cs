// <copyright file="GeofenceResult.cs" company="TreeWatch">
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

    /// <summary>
    /// Geofence result.
    /// </summary>
    public class GeofenceResult
    {
        /// <summary>
        /// Gets or sets the last enter time.
        /// </summary>
        /// <value>The last enter time.</value>
        public DateTime? LastEnterTime { get; set; }

        /// <summary>
        /// Gets or sets the last exit time.
        /// </summary>
        /// <value>The last exit time.</value>
        public DateTime? LastExitTime { get; set; }

        /// <summary>
        /// Gets or sets the transition.
        /// </summary>
        /// <value>The transition.</value>
        public GeofenceTransition Transition { get; set; }

        /// <summary>
        /// Gets or sets the region identifier.
        /// </summary>
        /// <value>The region identifier.</value>
        public string RegionId { get; set; }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <value>The duration.</value>
        public TimeSpan? Duration
        { 
            get
            {
                return this.LastExitTime - this.LastEnterTime;
            }
        }

        /// <summary>
        /// Gets the since last entry.
        /// </summary>
        /// <value>The since last entry.</value>
        public TimeSpan? SinceLastEntry
        {
            get
            {
                return DateTime.Now - this.LastEnterTime;
            }
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
        /// Gets or sets the accuracy.
        /// </summary>
        /// <value>The accuracy.</value>
        public double Accuracy { get; set; }

        /// <summary>
        /// Gets the name of the transition.
        /// </summary>
        /// <value>The name of the transition.</value>
        public string TransitionName
        {
            get
            {
                switch (this.Transition)
                {
                    case GeofenceTransition.Entered:
                        return "Entered";

                    case GeofenceTransition.Exited:
                        return "Exited";

                    case GeofenceTransition.Stayed:
                        return "Stayed in";

                    default:
                        return "Unknown";
                }
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="TreeWatch.GeofenceResult"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="TreeWatch.GeofenceResult"/>.</returns>
        public override string ToString()
        {
            return string.Format("{0} geofence region: {1}", this.TransitionName, this.RegionId);
        }
    }
}