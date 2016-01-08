// <copyright file="GeofenceStore.cs" company="TreeWatch">
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
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", MessageId = "Ctl", Scope = "namespace", Target = "Assembly name", Justification = "Auto generated name")]

// Analysis disable once InconsistentNaming
namespace TreeWatch.iOS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Foundation;

    /// <summary>
    /// Geofence store.
    /// </summary>
    public class GeofenceStore : BaseGeofenceStore
    {
        /// <summary>
        /// The geofence identifiers key.
        /// </summary>
        private const string GeofenceIdsKey = "Geofence.KeyIds";

        /// <summary>
        /// The identifier separator.
        /// </summary>
        private const string IdSeparator = "|";

        /// <summary>
        /// The shared instance.
        /// </summary>
        private static GeofenceStore sharedInstance = new GeofenceStore();

        /// <summary>
        /// The geofence identifiers.
        /// </summary>
        private ISet<string> geofenceIds;

        /// <summary>
        /// Prevents a default instance of the <see cref="TreeWatch.iOS.GeofenceStore"/> class from being created.
        /// </summary>
        private GeofenceStore()
        {
            this.geofenceIds = new HashSet<string>();

            if (!string.IsNullOrEmpty(NSUserDefaults.StandardUserDefaults.StringForKey(GeofenceIdsKey)))
            {
                ////GeofenceIdsKey
                string[] keys = NSUserDefaults.StandardUserDefaults.StringForKey(GeofenceIdsKey).Split(IdSeparator[0]);

                foreach (string k in keys)
                {
                    this.geofenceIds.Add(k);
                }
            }
        }

        /// <summary>
        /// Gets the shared instance.
        /// </summary>
        /// <value>The shared instance.</value>
        public static GeofenceStore SharedInstance
        {
            get { return sharedInstance; }
        }

        /// <summary>
        /// Gets all geofence regions in store
        /// </summary>
        /// <returns>A dictionary with all regions.</returns>
        public override Dictionary<string, GeofenceCircularRegion> GetAll()
        {
            var regions = new Dictionary<string, GeofenceCircularRegion>();

            foreach (NSString key in this.geofenceIds)
            {
                var region = this.Get(key.ToString());
                if (region != null)
                {
                    regions.Add(region.Id, region);
                }
            }

            return regions;
        }

        /// <summary>
        /// Gets an specific geofence from store
        /// </summary>
        /// <param name="id">Region id.</param>
        /// <returns>Region of id.</returns>
        public override GeofenceCircularRegion Get(string id)
        {
            GeofenceCircularRegion region = null;

            if (!string.IsNullOrEmpty(NSUserDefaults.StandardUserDefaults.StringForKey(this.GetFieldKey(id, BaseGeofenceStore.IdGeofenceRegionKey))))
            {
                double lat = NSUserDefaults.StandardUserDefaults.DoubleForKey(GetFieldKey(id, LatitudeGeofenceRegionKey));
                double lon = NSUserDefaults.StandardUserDefaults.DoubleForKey(GetFieldKey(id, LongitudeGeofenceRegionKey));
                bool notifyOnEntry = NSUserDefaults.StandardUserDefaults.BoolForKey(GetFieldKey(id, NotifyOnEntryGeofenceRegionKey));
                bool notifyOnExit = NSUserDefaults.StandardUserDefaults.BoolForKey(GetFieldKey(id, NotifyOnExitGeofenceRegionKey));
                bool notifyOnStay = NSUserDefaults.StandardUserDefaults.BoolForKey(GetFieldKey(id, NotifyOnStayGeofenceRegionKey));
                double radius = NSUserDefaults.StandardUserDefaults.DoubleForKey(GetFieldKey(id, RadiusGeofenceRegionKey));
                string notificationEntryMessage = NSUserDefaults.StandardUserDefaults.StringForKey(GetFieldKey(id, NotificationEntryMessageGeofenceRegionKey));
                string notificationExitMessage = NSUserDefaults.StandardUserDefaults.StringForKey(GetFieldKey(id, NotificationExitMessageGeofenceRegionKey));
                string notificationStayMessage = NSUserDefaults.StandardUserDefaults.StringForKey(GetFieldKey(id, NotificationStayMessageGeofenceRegionKey));
                bool showNotification = NSUserDefaults.StandardUserDefaults.BoolForKey(GetFieldKey(id, ShowNotificationGeofenceRegionKey));
                bool persistent = NSUserDefaults.StandardUserDefaults.BoolForKey(GetFieldKey(id, PersistentGeofenceRegionKey));
                int stayedInThresholdDuration = (int)NSUserDefaults.StandardUserDefaults.IntForKey(GetFieldKey(id, StayedInThresholdDurationGeofenceRegionKey));

                region = new GeofenceCircularRegion(id, lat, lon, radius, notifyOnEntry, notifyOnExit, notifyOnStay, showNotification, persistent)
                {
                    NotificationEntryMessage = notificationEntryMessage,
                    NotificationStayMessage = notificationStayMessage,
                    NotificationExitMessage = notificationExitMessage,
                    StayedInThresholdDuration = TimeSpan.FromMilliseconds(stayedInThresholdDuration)
                };
            }

            return region;
        }

        /// <summary>
        /// Save geofence region in store
        /// </summary>
        /// <param name="region">New region.</param>
        public override void Save(GeofenceCircularRegion region)
        {
            string id = region.Id;

            if (string.IsNullOrEmpty(id) || !region.Persistent)
            {
                return;
            }

            NSUserDefaults.StandardUserDefaults.SetString(region.Id, this.GetFieldKey(id, BaseGeofenceStore.IdGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.SetDouble(region.Latitude, this.GetFieldKey(id, BaseGeofenceStore.LatitudeGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.SetDouble(region.Longitude, this.GetFieldKey(id, BaseGeofenceStore.LongitudeGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.SetBool(region.NotifyOnEntry, this.GetFieldKey(id, BaseGeofenceStore.NotifyOnEntryGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.SetBool(region.NotifyOnExit, this.GetFieldKey(id, BaseGeofenceStore.NotifyOnExitGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.SetBool(region.NotifyOnStay, this.GetFieldKey(id, BaseGeofenceStore.NotifyOnStayGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.SetDouble(region.Radius, this.GetFieldKey(id, BaseGeofenceStore.RadiusGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.SetBool(region.ShowNotification, this.GetFieldKey(id, BaseGeofenceStore.ShowNotificationGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.SetBool(region.Persistent, this.GetFieldKey(id, BaseGeofenceStore.PersistentGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.SetInt((int)region.StayedInThresholdDuration.TotalMilliseconds, this.GetFieldKey(id, BaseGeofenceStore.StayedInThresholdDurationGeofenceRegionKey));

            if (!string.IsNullOrEmpty(region.NotificationEntryMessage))
            {
                NSUserDefaults.StandardUserDefaults.SetString(region.NotificationEntryMessage, this.GetFieldKey(id, BaseGeofenceStore.NotificationEntryMessageGeofenceRegionKey));
            }

            if (!string.IsNullOrEmpty(region.NotificationExitMessage))
            {
                NSUserDefaults.StandardUserDefaults.SetString(region.NotificationExitMessage, this.GetFieldKey(id, BaseGeofenceStore.NotificationExitMessageGeofenceRegionKey));
            }

            if (!string.IsNullOrEmpty(region.NotificationStayMessage))
            {
                NSUserDefaults.StandardUserDefaults.SetString(region.NotificationStayMessage, this.GetFieldKey(id, BaseGeofenceStore.NotificationStayMessageGeofenceRegionKey));
            }

            this.geofenceIds.Add(id);

            NSUserDefaults.StandardUserDefaults.SetString(string.Join(IdSeparator, this.geofenceIds.ToArray<string>()), GeofenceIdsKey);

            NSUserDefaults.StandardUserDefaults.Synchronize();
        }

        /// <summary>
        /// Remove all geofences regions from store
        /// </summary>
        public override void RemoveAll()
        {
            foreach (string key in this.geofenceIds)
            {
                this.ClearKeysForId(key);
            }

            NSUserDefaults.StandardUserDefaults.RemoveObject(GeofenceIdsKey);
        }

        /// <summary>
        /// Clears the keys for identifier.
        /// </summary>
        /// <param name="id">Identifier for keys.</param>
        public void ClearKeysForId(string id)
        {
            NSUserDefaults.StandardUserDefaults.RemoveObject(this.GetFieldKey(id, BaseGeofenceStore.IdGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.RemoveObject(this.GetFieldKey(id, BaseGeofenceStore.LatitudeGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.RemoveObject(this.GetFieldKey(id, BaseGeofenceStore.LongitudeGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.RemoveObject(this.GetFieldKey(id, BaseGeofenceStore.NotifyOnEntryGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.RemoveObject(this.GetFieldKey(id, BaseGeofenceStore.NotifyOnExitGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.RemoveObject(this.GetFieldKey(id, BaseGeofenceStore.NotifyOnStayGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.RemoveObject(this.GetFieldKey(id, BaseGeofenceStore.RadiusGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.RemoveObject(this.GetFieldKey(id, BaseGeofenceStore.NotificationEntryMessageGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.RemoveObject(this.GetFieldKey(id, BaseGeofenceStore.NotificationExitMessageGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.RemoveObject(this.GetFieldKey(id, BaseGeofenceStore.NotificationStayMessageGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.RemoveObject(this.GetFieldKey(id, BaseGeofenceStore.ShowNotificationGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.RemoveObject(this.GetFieldKey(id, BaseGeofenceStore.PersistentGeofenceRegionKey));
            NSUserDefaults.StandardUserDefaults.RemoveObject(this.GetFieldKey(id, BaseGeofenceStore.StayedInThresholdDurationGeofenceRegionKey));
        }

        /// <summary>
        /// Remove specific geofence region from store
        /// </summary>
        /// <param name="id">Region id.</param>
        public override void Remove(string id)
        {
            if (!string.IsNullOrEmpty(NSUserDefaults.StandardUserDefaults.StringForKey(this.GetFieldKey(id, BaseGeofenceStore.IdGeofenceRegionKey))))
            {
                this.ClearKeysForId(id);

                if (this.geofenceIds.Contains(id))
                {
                    this.geofenceIds.Remove(id);

                    NSUserDefaults.StandardUserDefaults.SetString(string.Join(IdSeparator, this.geofenceIds.ToArray<string>()), GeofenceIdsKey);
                }

                NSUserDefaults.StandardUserDefaults.Synchronize();
            }
        }
    }
}