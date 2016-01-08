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
namespace TreeWatch.Droid
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Android.Content;

    /// <summary>
    /// Geofence store.
    /// </summary>
    public class GeofenceStore : BaseGeofenceStore
    {
        /// <summary>
        /// Invalid values, used to test geofence storage when retrieving geofences.
        /// </summary>
        private const long InvalidLongValue = -999L;

        /// <summary>
        /// The invalid float value.
        /// </summary>
        private const float InvalidFloatValue = -999.0f;

        /// <summary>
        /// The invalid int value.
        /// </summary>
        private const int InvalidIntValue = -999;

        /// <summary>
        /// The shared instance.
        /// </summary>
        private static GeofenceStore sharedInstance = new GeofenceStore();

        /// <summary>
        /// The SharedPreferences object in which geofences are stored
        /// </summary>
        private readonly ISharedPreferences modePrefs;

        /// <summary>
        /// Prevents a default instance of the <see cref="TreeWatch.Droid.GeofenceStore"/> class from being created.
        /// </summary>
        private GeofenceStore()
        {
            this.modePrefs = Android.App.Application.Context.GetSharedPreferences(BaseGeofenceStore.GeofenceStoreId, FileCreationMode.Private);
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
        /// Returns a stored geofence by its ID, or returns null if it's not found
        /// </summary>
        /// <returns>A SimpleGeofence defined by its center and radius, or null if the ID is invalid</returns>
        /// <param name="id">The ID of a stored Geofence</param>
        public override GeofenceCircularRegion Get(string id)
        {
            // Get the latitude for the geofence identified by id, or INVALID_FLOAT_VALUE if it doesn't exist (similarly for the other values that follow)
            double lat = this.modePrefs.GetFloat(GetFieldKey(id, LatitudeGeofenceRegionKey), InvalidFloatValue);
            double lng = this.modePrefs.GetFloat(GetFieldKey(id, LongitudeGeofenceRegionKey), InvalidFloatValue);
            double radius = this.modePrefs.GetFloat(GetFieldKey(id, RadiusGeofenceRegionKey), InvalidFloatValue);
            bool notifyOnEntry = this.modePrefs.GetBoolean(GetFieldKey(id, NotifyOnEntryGeofenceRegionKey), false);
            bool notifyOnExit = this.modePrefs.GetBoolean(GetFieldKey(id, NotifyOnExitGeofenceRegionKey), false);
            bool notifyOnStay = this.modePrefs.GetBoolean(GetFieldKey(id, NotifyOnStayGeofenceRegionKey), false);
            string notificationEntryMessage = this.modePrefs.GetString(GetFieldKey(id, NotificationEntryMessageGeofenceRegionKey), string.Empty);
            string notificationExitMessage = this.modePrefs.GetString(GetFieldKey(id, NotificationExitMessageGeofenceRegionKey), string.Empty);
            string notificationStayMessage = this.modePrefs.GetString(GetFieldKey(id, NotificationStayMessageGeofenceRegionKey), string.Empty);
            bool showNotification = this.modePrefs.GetBoolean(GetFieldKey(id, ShowNotificationGeofenceRegionKey), false);
            bool persistent = this.modePrefs.GetBoolean(GetFieldKey(id, PersistentGeofenceRegionKey), false);
            int stayedInThresholdDuration = this.modePrefs.GetInt(GetFieldKey(id, StayedInThresholdDurationGeofenceRegionKey), InvalidIntValue);
            ////long expirationDuration = mPrefs.GetLong(GetFieldKey(id, ExpirationDurationGeofenceRegionKey), InvalidLongValue);
            ////int transitionType = mPrefs.GetInt(GetFieldKey(id, TransitionTypeGeofenceRegionKey), InvalidIntValue);

            // If none of the values is incorrect, return the object
            ////&& expirationDuration != InvalidLongValue
            ////&& transitionType != InvalidIntValue)
            if (Math.Abs(lat - InvalidFloatValue) > float.Epsilon
                && Math.Abs(lng - InvalidFloatValue) > float.Epsilon
                && Math.Abs(radius - InvalidFloatValue) > float.Epsilon
                && persistent
                && stayedInThresholdDuration != InvalidIntValue)
            {
                return new GeofenceCircularRegion(id, lat, lng, radius, notifyOnEntry, notifyOnExit, notifyOnStay, showNotification, persistent)
                {
                    NotificationEntryMessage = notificationEntryMessage,
                    NotificationStayMessage = notificationStayMessage,
                    NotificationExitMessage = notificationExitMessage,

                    StayedInThresholdDuration = TimeSpan.FromMilliseconds(stayedInThresholdDuration)
                };
            }

            // Otherwise return null
            return null;
        }

        /// <summary>
        /// Save a geofence
        /// </summary>
        /// <param name="region">The GeofenceCircularRegion with the values you want to save in SharedPreferemces</param>
        public override void Save(GeofenceCircularRegion region)
        {
            if (!region.Persistent)
            {
                return;
            }

            string id = region.Id;

            // Get a SharedPreferences editor instance. Among other things, SharedPreferences ensures that updates are atomic and non-concurrent
            ISharedPreferencesEditor prefs = this.modePrefs.Edit();

            // Write the geofence values to SharedPreferences 
            prefs.PutFloat(this.GetFieldKey(id, BaseGeofenceStore.LatitudeGeofenceRegionKey), (float)region.Latitude);
            prefs.PutFloat(this.GetFieldKey(id, BaseGeofenceStore.LongitudeGeofenceRegionKey), (float)region.Longitude);
            prefs.PutFloat(this.GetFieldKey(id, BaseGeofenceStore.RadiusGeofenceRegionKey), (float)region.Radius);
            prefs.PutBoolean(this.GetFieldKey(id, BaseGeofenceStore.NotifyOnEntryGeofenceRegionKey), region.NotifyOnEntry);
            prefs.PutBoolean(this.GetFieldKey(id, BaseGeofenceStore.NotifyOnExitGeofenceRegionKey), region.NotifyOnExit);
            prefs.PutBoolean(this.GetFieldKey(id, BaseGeofenceStore.NotifyOnStayGeofenceRegionKey), region.NotifyOnStay);
            prefs.PutString(this.GetFieldKey(id, BaseGeofenceStore.NotificationEntryMessageGeofenceRegionKey), region.NotificationEntryMessage);
            prefs.PutString(this.GetFieldKey(id, BaseGeofenceStore.NotificationExitMessageGeofenceRegionKey), region.NotificationExitMessage);
            prefs.PutString(this.GetFieldKey(id, BaseGeofenceStore.NotificationStayMessageGeofenceRegionKey), region.NotificationStayMessage);
            prefs.PutBoolean(this.GetFieldKey(id, BaseGeofenceStore.ShowNotificationGeofenceRegionKey), region.ShowNotification);
            prefs.PutBoolean(this.GetFieldKey(id, BaseGeofenceStore.PersistentGeofenceRegionKey), region.Persistent);
            prefs.PutInt(this.GetFieldKey(id, BaseGeofenceStore.StayedInThresholdDurationGeofenceRegionKey), (int)region.StayedInThresholdDuration.TotalMilliseconds);

            // Commit the changes
            prefs.Commit();
        }

        /// <summary>
        /// Remove all geofences regions from store
        /// </summary>
        public override void RemoveAll()
        {
            ISharedPreferencesEditor prefs = this.modePrefs.Edit();
            prefs.Clear();

            // Commit the changes
            prefs.Commit();
        }

        /// <summary>
        /// Remove specific geofence region from store
        /// </summary>
        /// <param name="id">Region id.</param>
        public override void Remove(string id)
        {
            try
            {
                ISharedPreferencesEditor prefs = this.modePrefs.Edit();

                prefs.Remove(this.GetFieldKey(id, BaseGeofenceStore.LatitudeGeofenceRegionKey));
                prefs.Remove(this.GetFieldKey(id, BaseGeofenceStore.LongitudeGeofenceRegionKey));
                prefs.Remove(this.GetFieldKey(id, BaseGeofenceStore.RadiusGeofenceRegionKey));
                prefs.Remove(this.GetFieldKey(id, BaseGeofenceStore.NotifyOnEntryGeofenceRegionKey));
                prefs.Remove(this.GetFieldKey(id, BaseGeofenceStore.NotifyOnExitGeofenceRegionKey));
                prefs.Remove(this.GetFieldKey(id, BaseGeofenceStore.NotifyOnStayGeofenceRegionKey));
                prefs.Remove(this.GetFieldKey(id, BaseGeofenceStore.NotificationEntryMessageGeofenceRegionKey));
                prefs.Remove(this.GetFieldKey(id, BaseGeofenceStore.NotificationExitMessageGeofenceRegionKey));
                prefs.Remove(this.GetFieldKey(id, BaseGeofenceStore.NotificationStayMessageGeofenceRegionKey));
                prefs.Remove(this.GetFieldKey(id, BaseGeofenceStore.ShowNotificationGeofenceRegionKey));
                prefs.Remove(this.GetFieldKey(id, BaseGeofenceStore.PersistentGeofenceRegionKey));
                prefs.Remove(this.GetFieldKey(id, BaseGeofenceStore.StayedInThresholdDurationGeofenceRegionKey));

                // Commit the changes
                prefs.Commit();
            }
            catch (Java.Lang.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} - Error: {1}", CrossGeofence.Id, ex));
            }
        }

        /// <summary>
        /// Gets all geofence regions in store
        /// </summary>
        /// <returns>A dictionary with all regions.</returns>
        public override Dictionary<string, GeofenceCircularRegion> GetAll()
        {
            var keys = this.modePrefs.All.Where(p => p.Key.StartsWith(GeofenceStoreId, StringComparison.Ordinal) && p.Key.Split('_').Length > 1).Select(p => p.Key.Split('_')[1]).Distinct();

            var regions = new Dictionary<string, GeofenceCircularRegion>();

            foreach (string key in keys)
            {
                var region = this.Get(key);
                if (region != null)
                {
                    regions.Add(region.Id, region);
                }
            }

            return regions;
        }
    }
}