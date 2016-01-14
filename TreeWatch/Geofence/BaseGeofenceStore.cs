// <copyright file="BaseGeofenceStore.cs" company="TreeWatch">
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
    using System.Collections.Generic;

    /// <summary>
    /// Base geofence store.
    /// </summary>
    public abstract class BaseGeofenceStore : IGeofenceStore
    {
        /// <summary>
        /// Storage Keys
        /// </summary>
        protected const string GeofenceStoreId = "CrossGeofence.Store";

        /// <summary>
        /// The latitude geofence region key.
        /// </summary>
        protected const string LatitudeGeofenceRegionKey = "Geofence.Region.Latitude";

        /// <summary>
        /// The longitude geofence region key.
        /// </summary>
        protected const string LongitudeGeofenceRegionKey = "Geofence.Region.Longitude";

        /// <summary>
        /// The radius geofence region key.
        /// </summary>
        protected const string RadiusGeofenceRegionKey = "Geofence.Region.Radius";

        /// <summary>
        /// The identifier geofence region key.
        /// </summary>
        protected const string IdGeofenceRegionKey = "Geofence.Region.Id";

        /// <summary>
        /// The transition type geofence region key.
        /// </summary>
        protected const string TransitionTypeGeofenceRegionKey = "Geofence.Region.TransitionType";

        /// <summary>
        /// The expiration duration geofence region key.
        /// </summary>
        protected const string ExpirationDurationGeofenceRegionKey = "Geofence.Region.ExpirationDuration";

        /// <summary>
        /// The notify on entry geofence region key.
        /// </summary>
        protected const string NotifyOnEntryGeofenceRegionKey = "Geofence.Region.NotifyOnEntry";

        /// <summary>
        /// The notify on exit geofence region key.
        /// </summary>
        protected const string NotifyOnExitGeofenceRegionKey = "Geofence.Region.NotifyOnExit";

        /// <summary>
        /// The notify on stay geofence region key.
        /// </summary>
        protected const string NotifyOnStayGeofenceRegionKey = "Geofence.Region.NotifyOnStay";

        /// <summary>
        /// The notification entry message geofence region key.
        /// </summary>
        protected const string NotificationEntryMessageGeofenceRegionKey = "Geofence.Region.NotificationEntryMessage";

        /// <summary>
        /// The notification exit message geofence region key.
        /// </summary>
        protected const string NotificationExitMessageGeofenceRegionKey = "Geofence.Region.NotificationExitMessage";

        /// <summary>
        /// The notification stay message geofence region key.
        /// </summary>
        protected const string NotificationStayMessageGeofenceRegionKey = "Geofence.Region.NotificationStayMessage";

        /// <summary>
        /// The show notification geofence region key.
        /// </summary>
        protected const string ShowNotificationGeofenceRegionKey = "Geofence.Region.ShowNotification";

        /// <summary>
        /// The persistent geofence region key.
        /// </summary>
        protected const string PersistentGeofenceRegionKey = "Geofence.Region.Persistent";

        /// <summary>
        /// The stayed in threshold duration geofence region key.
        /// </summary>
        protected const string StayedInThresholdDurationGeofenceRegionKey = "Geofence.Region.StayedInThresholdDuration";

        /// <summary>
        /// The exit threshold duration geofence region key.
        /// </summary>
        protected const string ExitThresholdDurationGeofenceRegionKey = "Geofence.Region.ExitThresholdDuration";

        /// <summary>
        /// Gets all stored geofence regions
        /// </summary>
        /// <returns>Dictionary of all stored regions.</returns>
        public abstract Dictionary<string, GeofenceCircularRegion> GetAll();

        /// <summary>
        /// Gets specific geofence region from store
        /// </summary>
        /// <param name="id">Region id.</param>
        /// <returns>Region of the given id.</returns>
        public abstract GeofenceCircularRegion Get(string id);

        /// <summary>
        /// Saves geofence region in store
        /// </summary>
        /// <param name="region">Region should be saved.</param>
        public abstract void Save(GeofenceCircularRegion region);

        /// <summary>
        /// Clear geofence regions in store
        /// </summary>
        public abstract void RemoveAll();

        /// <summary>
        /// Remove specific geofence region from store
        /// </summary>
        /// <param name="id">Region id.</param>
        public abstract void Remove(string id);

        /// <summary>
        /// Given a GeofenceCircularRegion object's ID and the name of a field , return the keyname of the object's values in Store
        /// </summary>
        /// <returns>The full key name o a value in Store</returns>
        /// <param name="id">The ID of a GeofenceCircularRegion object</param>
        /// <param name="fieldName">The field represented by the key</param>
        protected string GetFieldKey(string id, string fieldName)
        {
            return GeofenceStoreId + "_" + id + "_" + fieldName;
        }
    }
}