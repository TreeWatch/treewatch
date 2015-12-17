﻿using System;
using Android.Content;
using Android.OS;
using Android.App;
using Android.Gms.Location;
using System.Collections.Generic;
using Android.Support.V4.App;
using Android.Media;

namespace TreeWatch.Droid
{
    /// <summary>
    /// GeofenceTransitionsIntentService class
    /// Service that handles geofence events
    /// </summary>
    [Service]
    public class GeofenceTransitionsIntentService : IntentService
    {
        static int NotificationId = 0;
        const int NotificationMaxId = 6;

        /// <summary>
        /// Handles geofence intent arrival
        /// </summary>
        /// <param name="intent"></param>
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

            GeofenceTransition gTransition = GeofenceTransition.Unknown;

            ((GeofenceImplementation)CrossGeofence.Current).CurrentRequestType = GeofenceImplementation.RequestType.Update;

            foreach (Android.Gms.Location.IGeofence geofence in triggeringGeofences)
            {

                if (!CrossGeofence.Current.GeofenceResults.ContainsKey(geofence.RequestId))
                {
                    ((GeofenceImplementation)CrossGeofence.Current).AddGeofenceResult(geofence.RequestId);

                }
                //geofencingEvent.TriggeringLocation.Accuracy
                CrossGeofence.Current.GeofenceResults[geofence.RequestId].Latitude = geofencingEvent.TriggeringLocation.Latitude;
                CrossGeofence.Current.GeofenceResults[geofence.RequestId].Longitude = geofencingEvent.TriggeringLocation.Longitude;
                CrossGeofence.Current.GeofenceResults[geofence.RequestId].Accuracy = geofencingEvent.TriggeringLocation.Accuracy;

                double seconds = geofencingEvent.TriggeringLocation.Time / 1000;
                DateTime resultDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(seconds).ToLocalTime();

                //DateTime resultDate = DateTime.Now;

                switch (geofenceTransition)
                {
                    case Android.Gms.Location.Geofence.GeofenceTransitionEnter:
                        gTransition = GeofenceTransition.Entered;
                        CrossGeofence.Current.GeofenceResults[geofence.RequestId].LastEnterTime = resultDate;
                        CrossGeofence.Current.GeofenceResults[geofence.RequestId].LastExitTime = null;
                        break;
                    case Android.Gms.Location.Geofence.GeofenceTransitionExit:
                        gTransition = GeofenceTransition.Exited;
                        CrossGeofence.Current.GeofenceResults[geofence.RequestId].LastExitTime = resultDate;
                        break;
                    case Android.Gms.Location.Geofence.GeofenceTransitionDwell:
                        gTransition = GeofenceTransition.Stayed;
                        break;
                    default:
                        string message = string.Format("{0} - {1}", CrossGeofence.Id, "Invalid transition type");
                        System.Diagnostics.Debug.WriteLine(message);
                        gTransition = GeofenceTransition.Unknown;
                        break;
                }
                System.Diagnostics.Debug.WriteLine(string.Format("{0} - Transition: {1}", CrossGeofence.Id, gTransition));
                if (CrossGeofence.Current.GeofenceResults[geofence.RequestId].Transition != gTransition)
                {

                    CrossGeofence.Current.GeofenceResults[geofence.RequestId].Transition = gTransition;

                    CrossGeofence.GeofenceListener.OnRegionStateChanged(CrossGeofence.Current.GeofenceResults[geofence.RequestId]);

                    if (CrossGeofence.Current.Regions.ContainsKey(geofence.RequestId) && CrossGeofence.Current.Regions[geofence.RequestId].ShowNotification)
                    {

                        string message = CrossGeofence.Current.GeofenceResults[geofence.RequestId].ToString();

                        if (CrossGeofence.Current.Regions.ContainsKey(geofence.RequestId))
                        {
                            switch (gTransition)
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

        /// <summary>
        /// Create local notification
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public void CreateNotification(string title, string message)
        {
            try
            {

                NotificationCompat.Builder builder = null;
                Context context = Android.App.Application.Context;

                Intent resultIntent = context.PackageManager.GetLaunchIntentForPackage(context.PackageName);

                // Create a PendingIntent; we're only using one PendingIntent (ID = 0):
                const int pendingIntentId = 0;
                PendingIntent resultPendingIntent = PendingIntent.GetActivity(context, pendingIntentId, resultIntent, PendingIntentFlags.OneShot);

                // Build the notification
                builder = new NotificationCompat.Builder(context)
                    .SetAutoCancel(true) // dismiss the notification from the notification area when the user clicks on it
                    .SetContentIntent(resultPendingIntent) // start up this activity when the user clicks the intent.
                    .SetContentTitle(title) // Set the title
                    .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                    .SetSmallIcon(context.ApplicationInfo.Icon) // This is the icon to display
                    .SetContentText(message); // the message to display.

                NotificationManager notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);

                if (NotificationId >= NotificationMaxId)
                {
                    NotificationId = 0;
                }

                notificationManager.Notify(NotificationId++, builder.Build());
            }
            catch (Java.Lang.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}", CrossGeofence.Id, ex.ToString()));
            }
            catch (System.Exception ex1)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}", CrossGeofence.Id, ex1.ToString()));
            }

        }
    }
}

