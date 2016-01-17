// <copyright file="GeofenceCircularRegion.cs" company="TreeWatch">
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
    /// Geofence circular region.
    /// </summary>
    public class GeofenceCircularRegion
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.GeofenceCircularRegion"/> class.
        /// </summary>
        /// <param name="id">Identifier string.</param>
        /// <param name="centerLatitude">Center latitude.</param>
        /// <param name="centerLongitude">Center longitude.</param>
        /// <param name="radius">Radius as double.</param>
        /// <param name="notifyOnEntry">If set to <c>true</c> notify on entry.</param>
        /// <param name="notifyOnExit">If set to <c>true</c> notify on exit.</param>
        /// <param name="notifyOnStay">If set to <c>true</c> notify on stay.</param>
        /// <param name="showNotification">If set to <c>true</c> show notification.</param>
        /// <param name="persistent">If set to <c>true</c> persistent.</param>
        public GeofenceCircularRegion(string id, double centerLatitude, double centerLongitude, double radius, bool notifyOnEntry = true, bool notifyOnExit = true, bool notifyOnStay = false, bool showNotification = true, bool persistent = true)
        {
            this.Id = id;
            this.Latitude = centerLatitude;
            this.Longitude = centerLongitude;
            this.Radius = radius;
            this.NotifyOnEntry = notifyOnEntry;
            this.NotifyOnExit = notifyOnExit;
            this.NotifyOnStay = notifyOnStay;
            this.ShowNotification = showNotification;
            this.Persistent = persistent;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }

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
        /// Gets or sets the radius.
        /// </summary>
        /// <value>The radius.</value>
        public double Radius { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TreeWatch.GeofenceCircularRegion"/> notify on entry.
        /// </summary>
        /// <value><c>true</c> if notify on entry; otherwise, <c>false</c>.</value>
        public bool NotifyOnEntry { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TreeWatch.GeofenceCircularRegion"/> notify on stay.
        /// </summary>
        /// <value><c>true</c> if notify on stay; otherwise, <c>false</c>.</value>
        public bool NotifyOnStay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TreeWatch.GeofenceCircularRegion"/> notify on exit.
        /// </summary>
        /// <value><c>true</c> if notify on exit; otherwise, <c>false</c>.</value>
        public bool NotifyOnExit { get; set; }

        /// <summary>
        /// Gets or sets the notification entry message.
        /// </summary>
        /// <value>The notification entry message.</value>
        public string NotificationEntryMessage { get; set; }

        /// <summary>
        /// Gets or sets the notification exit message.
        /// </summary>
        /// <value>The notification exit message.</value>
        public string NotificationExitMessage { get; set; }

        /// <summary>
        /// Gets or sets the notification stay message.
        /// </summary>
        /// <value>The notification stay message.</value>
        public string NotificationStayMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TreeWatch.GeofenceCircularRegion"/> is persistent.
        /// </summary>
        /// <value><c>true</c> if persistent; otherwise, <c>false</c>.</value>
        public bool Persistent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TreeWatch.GeofenceCircularRegion"/> show notification.
        /// Messages could be configured using properties: NotificationEntryMessage, NotificationExitMessage, NotificationStayMessage
        /// </summary>
        /// <value><c>true</c> if show notification; otherwise, <c>false</c>.</value>
        public bool ShowNotification { get; set; }

        /// <summary>
        /// Gets or sets the duration of the stayed in threshold.
        /// </summary>
        /// <value>The duration of the stayed in threshold.</value>
        public TimeSpan StayedInThresholdDuration { get; set; }

        /// <summary>
        /// Gets or sets the duration of the exit threshold.
        /// </summary>
        /// <value>The duration of the exit threshold.</value>
        public TimeSpan ExitThresholdDuration { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="TreeWatch.GeofenceCircularRegion"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="TreeWatch.GeofenceCircularRegion"/>.</returns>
        public override string ToString()
        {
            return this.Id;
        }
    }
}