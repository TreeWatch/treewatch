// <copyright file="GeofenceTransitionsIntentService.cs" company="TreeWatch">
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
    using Android.App;
    using Android.Content;
    using Android.Media;
    using Android.OS;
    using Android.Support.V4.App;

    /// <summary>
    /// GeofenceTransitionsIntentService class
    /// Service that handles geofence events
    /// </summary>
    [Service]
    public class GeofenceTransitionsIntentService : IntentService
    {
        /// <summary>
        /// The notification max identifier.
        /// </summary>
        private const int NotificationMaxId = 6;

        /// <summary>
        /// The notification identifier.
        /// </summary>
        private static int notificationId;

        /// <summary>
        /// Creates the notification.
        /// </summary>
        /// <param name="title">Title string.</param>
        /// <param name="message">Message string.</param>
        public static void CreateNotification(string title, string message)
        {
            try
            {
                NotificationCompat.Builder builder;
                Context context = Android.App.Application.Context;

                Intent resultIntent = context.PackageManager.GetLaunchIntentForPackage(context.PackageName);

                // Create a PendingIntent; we're only using one PendingIntent (ID = 0):
                const int PendingIntentId = 0;
                PendingIntent resultPendingIntent = PendingIntent.GetActivity(context, PendingIntentId, resultIntent, PendingIntentFlags.OneShot);

                // Build the notification
                builder = new NotificationCompat.Builder(context)
                    .SetAutoCancel(true) // dismiss the notification from the notification area when the user clicks on it
                    .SetContentIntent(resultPendingIntent) // start up this activity when the user clicks the intent.
                    .SetContentTitle(title) // Set the title
                    .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                    .SetSmallIcon(context.ApplicationInfo.Icon) // This is the icon to display
                    .SetContentText(message); // the message to display.

                var notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;

                if (notificationId >= NotificationMaxId)
                {
                    notificationId = 0;
                }

                notificationManager.Notify(notificationId++, builder.Build());
            }
            catch (Java.Lang.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}", CrossGeofence.Id, ex));
            }
            catch (Exception ex1)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}", CrossGeofence.Id, ex1));
            }
        }

        /// <Docs>To be added.</Docs>
        /// <remarks>To be added.</remarks>
        /// <summary>
        /// Raises the handle intent event.
        /// </summary>
        /// <param name="intent">Intent object.</param>
        protected override void OnHandleIntent(Intent intent)
        {
            Context context = Android.App.Application.Context;
            Bundle extras = intent.Extras;
            Android.Gms.Location.GeofencingEvent geofencingEvent = Android.Gms.Location.GeofencingEvent.FromIntent(intent);

            if (geofencingEvent.HasError)
            {
                string errorMessage = Android.Gms.Location.GeofenceStatusCodes.GetStatusCodeString(geofencingEvent.ErrorCode);
                string message = string.Format("{0} - {1}", CrossGeofence.Id, errorMessage);
                System.Diagnostics.Debug.WriteLine(message);
                CrossGeofence.GeofenceListener.OnError(message);
            }

            // Get the transition type.
            int geofenceTransition = geofencingEvent.GeofenceTransition;

            // Get the geofences that were triggered. A single event can trigger multiple geofences.
            IList<Android.Gms.Location.IGeofence> triggeringGeofences = geofencingEvent.TriggeringGeofences;

            GeofenceTransition geoTransition;

            ((GeofenceImplementation)CrossGeofence.Current).CurrentRequestType = GeofenceImplementation.RequestType.Update;

            foreach (Android.Gms.Location.IGeofence geofence in triggeringGeofences)
            {
                if (!CrossGeofence.Current.GeofenceResults.ContainsKey(geofence.RequestId))
                {
                    ((GeofenceImplementation)CrossGeofence.Current).AddGeofenceResult(geofence.RequestId);
                }

                ////geofencingEvent.TriggeringLocation.Accuracy
                CrossGeofence.Current.GeofenceResults[geofence.RequestId].Latitude = geofencingEvent.TriggeringLocation.Latitude;
                CrossGeofence.Current.GeofenceResults[geofence.RequestId].Longitude = geofencingEvent.TriggeringLocation.Longitude;
                CrossGeofence.Current.GeofenceResults[geofence.RequestId].Accuracy = geofencingEvent.TriggeringLocation.Accuracy;

                double seconds = geofencingEvent.TriggeringLocation.Time / 1000;
                DateTime resultDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(seconds).ToLocalTime();

                ////DateTime resultDate = DateTime.Now;

                switch (geofenceTransition)
                {
                    case Android.Gms.Location.Geofence.GeofenceTransitionEnter:
                        geoTransition = GeofenceTransition.Entered;
                        CrossGeofence.Current.GeofenceResults[geofence.RequestId].LastEnterTime = resultDate;
                        CrossGeofence.Current.GeofenceResults[geofence.RequestId].LastExitTime = null;
                        break;
                    case Android.Gms.Location.Geofence.GeofenceTransitionExit:
                        geoTransition = GeofenceTransition.Exited;
                        CrossGeofence.Current.GeofenceResults[geofence.RequestId].LastExitTime = resultDate;
                        break;
                    case Android.Gms.Location.Geofence.GeofenceTransitionDwell:
                        geoTransition = GeofenceTransition.Stayed;
                        break;
                    default:
                        string message = string.Format("{0} - {1}", CrossGeofence.Id, "Invalid transition type");
                        System.Diagnostics.Debug.WriteLine(message);
                        geoTransition = GeofenceTransition.Unknown;
                        break;
                }

                System.Diagnostics.Debug.WriteLine(string.Format("{0} - Transition: {1}", CrossGeofence.Id, geoTransition));
                if (CrossGeofence.Current.GeofenceResults[geofence.RequestId].Transition != geoTransition)
                {
                    CrossGeofence.Current.GeofenceResults[geofence.RequestId].Transition = geoTransition;

                    CrossGeofence.GeofenceListener.OnRegionStateChanged(CrossGeofence.Current.GeofenceResults[geofence.RequestId]);

                    if (CrossGeofence.Current.Regions.ContainsKey(geofence.RequestId) && CrossGeofence.Current.Regions[geofence.RequestId].ShowNotification)
                    {
                        string message = CrossGeofence.Current.GeofenceResults[geofence.RequestId].ToString();

                        if (CrossGeofence.Current.Regions.ContainsKey(geofence.RequestId))
                        {
                            switch (geoTransition)
                            {
                                case GeofenceTransition.Entered:
                                    message = string.IsNullOrEmpty(CrossGeofence.Current.Regions[geofence.RequestId].NotificationEntryMessage) ? message : CrossGeofence.Current.Regions[geofence.RequestId].NotificationEntryMessage;
                                    break;
                                case GeofenceTransition.Exited:
                                    message = string.IsNullOrEmpty(CrossGeofence.Current.Regions[geofence.RequestId].NotificationExitMessage) ? message : CrossGeofence.Current.Regions[geofence.RequestId].NotificationExitMessage;
                                    break;
                                case GeofenceTransition.Stayed:
                                    message = string.IsNullOrEmpty(CrossGeofence.Current.Regions[geofence.RequestId].NotificationStayMessage) ? message : CrossGeofence.Current.Regions[geofence.RequestId].NotificationStayMessage;
                                    break;
                            }
                        }

                        CreateNotification(context.ApplicationInfo.LoadLabel(context.PackageManager), message);
                    }
                }
            }
        }
    }
}