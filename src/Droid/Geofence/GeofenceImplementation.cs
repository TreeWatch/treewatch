// <copyright file="GeofenceImplementation.cs" company="TreeWatch">
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
using TreeWatch.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(GeofenceImplementation))]

namespace TreeWatch.Droid
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Android.App;
    using Android.Content;
    using Android.Gms.Common;
    using Android.Gms.Common.Apis;
    using Android.OS;
    using Java.Interop;

    /// <summary>
    /// Geofence implementation.
    /// </summary>
    public class GeofenceImplementation : Java.Lang.Object, IGeofence, GoogleApiClient.IConnectionCallbacks,
    GoogleApiClient.IOnConnectionFailedListener, IResultCallback
    {
        /// <summary>
        /// The geo receiver action.
        /// </summary>
        internal const string GeoReceiverAction = "ACTION_RECEIVE_GEOFENCE";

        /// <summary>
        /// The m regions.
        /// </summary>
        private Dictionary<string, GeofenceCircularRegion> modeRegions = GeofenceStore.SharedInstance.GetAll();

        /// <summary>
        /// The m geofence results.
        /// </summary>
        private Dictionary<string, GeofenceResult> modeGeofenceResults = new Dictionary<string, GeofenceResult>();

        /// <summary>
        /// The last known geofence location.
        /// </summary>
        private GeofenceLocation lastKnownGeofenceLocation;

        /// <summary>
        /// The m geofence pending intent.
        /// </summary>
        private PendingIntent modeGeofencePendingIntent;

        /// <summary>
        /// The m google API client.
        /// </summary>
        private GoogleApiClient modeGoogleApiClient;

        /// <summary>
        /// The m requested region identifiers.
        /// </summary>
        private IList<string> modeRequestedRegionIdentifiers;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.Droid.GeofenceImplementation"/> class.
        /// </summary>
        public GeofenceImplementation()
        {
            // Check if location services are enabled
            if (!((Android.Locations.LocationManager)Application.Context.GetSystemService(Context.LocationService)).IsProviderEnabled(Android.Locations.LocationManager.GpsProvider))
            {
                string message = string.Format("{0} - {1}", CrossGeofence.Id, "You need to enable Location Services");
                System.Diagnostics.Debug.WriteLine(message);
                CrossGeofence.GeofenceListener.OnError(message);
                return;
            }

            this.CurrentRequestType = RequestType.Default;

            this.InitializeGoogleAPI();

            if (this.IsMonitoring)
            { 
                this.StartMonitoring(this.Regions.Values.ToList());
                System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}", CrossGeofence.Id, "Monitoring was restored"));
            }
        }

        /// <summary>
        /// Defines the allowable request types
        /// </summary>
        internal enum RequestType
        {
            /// <summary>
            /// The add.
            /// </summary>
            Add,

            /// <summary>
            /// The update.
            /// </summary>
            Update,

            /// <summary>
            /// The delete.
            /// </summary>
            Delete,

            /// <summary>
            /// The clear.
            /// </summary>
            Clear,

            /// <summary>
            /// The default.
            /// </summary>
            Default
        }

        /// <summary>
        /// Gets a dictionary that contains all regions been monitored.
        /// </summary>
        /// <value>The regions.</value>
        public IReadOnlyDictionary<string, GeofenceCircularRegion> Regions
        { 
            get { return this.modeRegions; }
        }

        /// <summary>
        /// Gets a Dicitonary that contains all geofence results received.
        /// </summary>
        /// <value>The geofence results.</value>
        public IReadOnlyDictionary<string, GeofenceResult> GeofenceResults
        { 
            get { return this.modeGeofenceResults; }
        }

        /// <summary>
        /// Gets the last known geofence location.
        /// </summary>
        /// <value>The last known location.</value>
        public GeofenceLocation LastKnownLocation
        { 
            get { return this.lastKnownGeofenceLocation; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is monitoring.
        /// </summary>
        /// <value>true or false</value>
        /// <c>false</c>
        public bool IsMonitoring
        {
            get { return this.modeRegions.Count > 0; }
        }

        ////IsMonitoring?RequestType.Add:

        /// <summary>
        /// Gets or sets the type of the current request.
        /// </summary>
        /// <value>The type of the current request.</value>
        internal RequestType CurrentRequestType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the geofence transition pending intent.
        /// </summary>
        /// <value>The geofence transition pending intent.</value>
        private PendingIntent GeofenceTransitionPendingIntent
        {
            get
            {
                // If the PendingIntent already exists
                if (this.modeGeofencePendingIntent == null)
                {
                    ////var intent = new Intent(Application.Context, typeof(GeofenceBroadcastReceiver));
                    ////intent.SetAction(string.Format("{0}.{1}", Application.Context.PackageName, GeoReceiverAction));
                    var intent = new Intent(string.Format("{0}.{1}", Application.Context.PackageName, GeoReceiverAction));
                    Console.Error.WriteLine("{0}.{1}", Application.Context.PackageName, GeoReceiverAction);
                    ////var intent = new Intent(Application.Context, typeof(GeofenceTransitionsIntentService));
                    this.modeGeofencePendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, intent, PendingIntentFlags.UpdateCurrent);
                }

                return this.modeGeofencePendingIntent;
            }
        }

        /// <summary>
        /// Starts the monitoring.
        /// </summary>
        /// <param name="region">Region that should be monitored.</param>
        public void StartMonitoring(GeofenceCircularRegion region)
        {
            /*if (IsMonitoring && mGoogleApiClient.IsConnected)
          {
              Android.Gms.Location.LocationServices.GeofencingApi.RemoveGeofences(mGoogleApiClient, GeofenceTransitionPendingIntent).SetResultCallback(this);
          }*/

            if (!this.modeRegions.ContainsKey(region.Id))
            {
                this.modeRegions.Add(region.Id, region);
            }

            this.RequestMonitoringStart();
        }

        /// <summary>
        /// Starts the monitoring.
        /// </summary>
        /// <param name="regions">Regions list.</param>
        public void StartMonitoring(IList<GeofenceCircularRegion> regions)
        {
            /* if (IsMonitoring && mGoogleApiClient.IsConnected)
          {
              Android.Gms.Location.LocationServices.GeofencingApi.RemoveGeofences(mGoogleApiClient, GeofenceTransitionPendingIntent).SetResultCallback(this);
          }*/

            foreach (var region in regions)
            {
                if (!this.modeRegions.ContainsKey(region.Id))
                {
                    this.modeRegions.Add(region.Id, region);
                }
            }

            this.RequestMonitoringStart();
        }

        /// <summary>
        /// Stops the monitoring.
        /// </summary>
        /// <param name="regionIdentifier">Region identifier.</param>
        public void StopMonitoring(string regionIdentifier)
        {
            this.StopMonitoring(new List<string> { regionIdentifier });
        }

        /// <summary>
        /// Stops the monitoring.
        /// </summary>
        /// <param name="regionIdentifiers">Region identifiers.</param>
        public void StopMonitoring(IList<string> regionIdentifiers)
        {
            this.modeRequestedRegionIdentifiers = regionIdentifiers;

            if (this.IsMonitoring && this.modeGoogleApiClient.IsConnected)
            {
                this.RemoveGeofences(regionIdentifiers);
            }
            else
            {
                // If not connection then connect
                if (!this.modeGoogleApiClient.IsConnecting)
                {
                    this.modeGoogleApiClient.Connect();
                }

                // Request to add geofence regions once connected
                this.CurrentRequestType = RequestType.Delete;
            }
        }

        /// <summary>
        /// Stops monitoring all geofence regions
        /// </summary>
        public void StopMonitoringAllRegions()
        {
            if (this.IsMonitoring && this.modeGoogleApiClient.IsConnected)
            {
                this.RemoveGeofences();
            }
            else
            {
                // If not connection then connect
                if (!this.modeGoogleApiClient.IsConnecting)
                {
                    this.modeGoogleApiClient.Connect();
                }

                // Request to add geofence regions once connected
                this.CurrentRequestType = RequestType.Clear;
            }
        }

        /// <summary>
        /// Raises the connection failed event.
        /// </summary>
        /// <param name="result">Connection result.</param>
        public void OnConnectionFailed(ConnectionResult result)
        {
            int errorCode = result.ErrorCode;
            string message = string.Format("{0} - {1} {2}", CrossGeofence.Id, "Connection to Google Play services failed with error code ", errorCode);
            System.Diagnostics.Debug.WriteLine(message);
            CrossGeofence.GeofenceListener.OnError(message);
        }

        /// <summary>
        /// Raises the connected event.
        /// </summary>
        /// <param name="connectionHint">Connection hint.</param>
        public void OnConnected(Bundle connectionHint)
        {
            Android.Locations.Location location = Android.Gms.Location.LocationServices.FusedLocationApi.GetLastLocation(this.modeGoogleApiClient);
            this.SetLastKnownLocation(location);

            switch (this.CurrentRequestType)
            {
                case RequestType.Add:
                    this.AddGeofences();
                    this.StartLocationUpdates();
                    break;
                case RequestType.Clear:
                    this.RemoveGeofences();
                    break;
                case RequestType.Delete:
                    if (this.modeRequestedRegionIdentifiers != null)
                    {
                        this.RemoveGeofences(this.modeRequestedRegionIdentifiers);
                    }

                    break;
            }

            this.CurrentRequestType = RequestType.Default;
        }

        /// <summary>
        /// Raises the result event.
        /// </summary>
        /// <param name="result">Result object.</param>
        public void OnResult(Java.Lang.Object result)
        {
            var res = result.JavaCast<IResult>();

            int statusCode = res.Status.StatusCode;
            ////int statusCode = 0;
            string message = string.Empty;

            switch (statusCode)
            {
                case CommonStatusCodes.SuccessCache:
                case CommonStatusCodes.Success:
                    if (this.CurrentRequestType == RequestType.Add)
                    {
                        message = string.Format("{0} - {1}", CrossGeofence.Id, "Successfully added Geofence.");

                        foreach (GeofenceCircularRegion region in this.Regions.Values)
                        {
                            CrossGeofence.GeofenceListener.OnMonitoringStarted(region.Id);
                        }
                    }
                    else
                    {
                        message = string.Format("{0} - {1} - {2}", CrossGeofence.Id, "Geofence Update Received", CurrentRequestType);
                    }

                    break;
                case CommonStatusCodes.Error:
                    message = string.Format("{0} - {1}", CrossGeofence.Id, "Error adding Geofence.");
                    break;
                case Android.Gms.Location.GeofenceStatusCodes.GeofenceTooManyGeofences:
                    message = string.Format("{0} - {1}", CrossGeofence.Id, "Too many geofences.");
                    break;
                case Android.Gms.Location.GeofenceStatusCodes.GeofenceTooManyPendingIntents:
                    message = string.Format("{0} - {1}", CrossGeofence.Id, "Too many pending intents.");
                    break;
                case Android.Gms.Location.GeofenceStatusCodes.GeofenceNotAvailable:
                    message = string.Format("{0} - {1}", CrossGeofence.Id, "Geofence not available.");
                    break;
            }

            System.Diagnostics.Debug.WriteLine(message);

            if (statusCode != CommonStatusCodes.Success && statusCode != CommonStatusCodes.SuccessCache && this.IsMonitoring)
            {
                this.StopMonitoringAllRegions();

                if (!string.IsNullOrEmpty(message))
                {
                    CrossGeofence.GeofenceListener.OnError(message);
                }
            }
        }

        /// <summary>
        /// Raises the connection suspended event.
        /// </summary>
        /// <param name="cause">Cause int.</param>
        public void OnConnectionSuspended(int cause)
        {
            string message = string.Format("{0} - {1} {2}", CrossGeofence.Id, "Connection to Google Play services suspended with error code ", cause);
            System.Diagnostics.Debug.WriteLine(message);
            CrossGeofence.GeofenceListener.OnError(message);
        }

        /// <summary>
        /// Raises the monitoring removal event.
        /// </summary>
        internal void OnMonitoringRemoval()
        {
            if (this.modeRegions.Count == 0)
            {
                CrossGeofence.GeofenceListener.OnMonitoringStopped();

                this.StopLocationUpdates();

                this.modeGoogleApiClient.Disconnect();
            }
        }

        /// <summary>
        /// Starts the location updates.
        /// </summary>
        internal void StartLocationUpdates()
        {
            var modeLocationRequest = new Android.Gms.Location.LocationRequest();
            modeLocationRequest.SetInterval(CrossGeofence.LocationUpdatesInterval == 0 ? 30000 : CrossGeofence.LocationUpdatesInterval);
            modeLocationRequest.SetFastestInterval(CrossGeofence.FastestLocationUpdatesInterval == 0 ? 5000 : CrossGeofence.FastestLocationUpdatesInterval);
            string priorityType = "Balanced Power";
            switch (CrossGeofence.GeofencePriority)
            {
                case GeofencePriority.HighAccuracy:
                    priorityType = "High Accuracy";
                    modeLocationRequest.SetPriority(Android.Gms.Location.LocationRequest.PriorityHighAccuracy);
                    break;
                case GeofencePriority.LowAccuracy:
                    priorityType = "Low Accuracy";
                    modeLocationRequest.SetPriority(Android.Gms.Location.LocationRequest.PriorityLowPower);
                    break;
                case GeofencePriority.LowestAccuracy:
                    priorityType = "Lowest Accuracy";
                    modeLocationRequest.SetPriority(Android.Gms.Location.LocationRequest.PriorityNoPower);
                    break;
                default:
                    modeLocationRequest.SetPriority(Android.Gms.Location.LocationRequest.PriorityBalancedPowerAccuracy);
                    break;
            }

            System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}: {2}", CrossGeofence.Id, "Priority set to", priorityType));
            ////(Regions.Count == 0) ? (CrossGeofence.SmallestDisplacement==0?50 :CrossGeofence.SmallestDisplacement): Regions.Min(s => (float)s.Value.Radius)
            if (CrossGeofence.SmallestDisplacement > 0)
            {
                modeLocationRequest.SetSmallestDisplacement(CrossGeofence.SmallestDisplacement);
                System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}: {2} meters", CrossGeofence.Id, "Location smallest displacement set to", CrossGeofence.SmallestDisplacement));
            }

            Android.Gms.Location.LocationServices.FusedLocationApi.RequestLocationUpdates(this.modeGoogleApiClient, modeLocationRequest, GeofenceLocationListener.SharedInstance);
        }

        /// <summary>
        /// Sets the last known location.
        /// </summary>
        /// <param name="location">Location object.</param>
        internal void SetLastKnownLocation(Android.Locations.Location location)
        {
            if (location != null)
            {
                if (this.lastKnownGeofenceLocation == null)
                {
                    this.lastKnownGeofenceLocation = new GeofenceLocation();
                }

                this.lastKnownGeofenceLocation.Latitude = location.Latitude;
                this.lastKnownGeofenceLocation.Longitude = location.Longitude;
                double seconds = location.Time / 1000;
                this.lastKnownGeofenceLocation.Date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local).AddSeconds(seconds);
            }
        }

        /// <summary>
        /// Stops the location updates.
        /// </summary>
        internal void StopLocationUpdates()
        {
            Android.Gms.Location.LocationServices.FusedLocationApi.RemoveLocationUpdates(this.modeGoogleApiClient, GeofenceLocationListener.SharedInstance);
        }

        /// <summary>
        /// Adds the geofence result.
        /// </summary>
        /// <param name="identifier">Identifier string.</param>
        internal void AddGeofenceResult(string identifier)
        {
            this.modeGeofenceResults.Add(
                identifier,
                new GeofenceResult
                {
                    RegionId = identifier,
                    Transition = GeofenceTransition.Unknown
                });
        }

        /// <summary>
        /// Requests the monitoring start.
        /// </summary>
        private void RequestMonitoringStart()
        {
            // If connected to google play services then add regions
            if (this.modeGoogleApiClient.IsConnected)
            {
                this.AddGeofences();
            }
            else
            {
                // If not connection then connect
                if (!this.modeGoogleApiClient.IsConnecting)
                {
                    this.modeGoogleApiClient.Connect();
                }

                // Request to add geofence regions once connected
                this.CurrentRequestType = RequestType.Add;
            }
        }

        /// <summary>
        /// Adds the geofences.
        /// </summary>
        private void AddGeofences()
        {
            try
            {
                var geofenceList = new List<Android.Gms.Location.IGeofence>();
                var regions = this.Regions.Values;
                foreach (GeofenceCircularRegion region in regions)
                {
                    int transitionTypes = 0;

                    if (region.NotifyOnStay)
                    {
                        transitionTypes |= Android.Gms.Location.Geofence.GeofenceTransitionDwell;
                    }

                    if (region.NotifyOnEntry)
                    {
                        transitionTypes |= Android.Gms.Location.Geofence.GeofenceTransitionEnter;
                    }

                    if (region.NotifyOnExit)
                    {
                        transitionTypes |= Android.Gms.Location.Geofence.GeofenceTransitionExit;
                    }

                    if (transitionTypes != 0)
                    {
                        geofenceList.Add(new Android.Gms.Location.GeofenceBuilder()
                            .SetRequestId(region.Id)
                            .SetCircularRegion(region.Latitude, region.Longitude, (float)region.Radius)
                            .SetLoiteringDelay((int)region.StayedInThresholdDuration.TotalMilliseconds)
                            ////.SetNotificationResponsiveness(mNotificationResponsivness)
                            .SetExpirationDuration(Android.Gms.Location.Geofence.NeverExpire)
                            .SetTransitionTypes(transitionTypes)
                            .Build());

                        if (GeofenceStore.SharedInstance.Get(region.Id) == null)
                        {
                            GeofenceStore.SharedInstance.Save(region);
                        }

                        CrossGeofence.GeofenceListener.OnMonitoringStarted(region.Id);
                    }
                }

                if (geofenceList.Count > 0)
                {
                    Android.Gms.Location.GeofencingRequest request = new Android.Gms.Location.GeofencingRequest.Builder().SetInitialTrigger(Android.Gms.Location.GeofencingRequest.InitialTriggerEnter).AddGeofences(geofenceList).Build();

                    Android.Gms.Location.LocationServices.GeofencingApi.AddGeofences(this.modeGoogleApiClient, request, this.GeofenceTransitionPendingIntent).SetResultCallback(this);

                    this.CurrentRequestType = RequestType.Default;
                }
            }
            catch (Java.Lang.Exception ex1)
            {
                string message = string.Format("{0} - Error: {1}", CrossGeofence.Id, ex1);
                System.Diagnostics.Debug.WriteLine(message);
                CrossGeofence.GeofenceListener.OnError(message);
            }
            catch (Exception ex2)
            {
                string message = string.Format("{0} - Error: {1}", CrossGeofence.Id, ex2);
                System.Diagnostics.Debug.WriteLine(message);
                CrossGeofence.GeofenceListener.OnError(message);
            }
        }

        /// <summary>
        /// Removes the geofences.
        /// </summary>
        private void RemoveGeofences()
        {
            GeofenceStore.SharedInstance.RemoveAll();
            this.modeRegions.Clear();
            this.modeGeofenceResults.Clear();
            Android.Gms.Location.LocationServices.GeofencingApi.RemoveGeofences(this.modeGoogleApiClient, this.GeofenceTransitionPendingIntent).SetResultCallback(this);
            this.StopLocationUpdates();
            this.modeGoogleApiClient.Disconnect();
            CrossGeofence.GeofenceListener.OnMonitoringStopped();
        }

        /// <summary>
        /// Initializes the google AP.
        /// </summary>
        private void InitializeGoogleAPI()
        {
            int queryResult = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(Application.Context);

            if (queryResult == ConnectionResult.Success)
            {
                if (this.modeGoogleApiClient == null)
                {
                    this.modeGoogleApiClient = new GoogleApiClient.Builder(Application.Context).AddApi(Android.Gms.Location.LocationServices.API).AddConnectionCallbacks(this).AddOnConnectionFailedListener(this).Build();
                    string message = string.Format("{0} - {1}", CrossGeofence.Id, "Google Play services is available.");
                    System.Diagnostics.Debug.WriteLine(message);
                }

                if (!this.modeGoogleApiClient.IsConnected)
                {
                    this.modeGoogleApiClient.Connect();
                }
            }
            else
            {
                string message = string.Format("{0} - {1}", CrossGeofence.Id, "Google Play services is unavailable.");
                System.Diagnostics.Debug.WriteLine(message);
                CrossGeofence.GeofenceListener.OnError(message);
            }
        }

        /// <summary>
        /// Removes the geofences.
        /// </summary>
        /// <param name="regionIdentifiers">Region identifiers.</param>
        private void RemoveGeofences(IList<string> regionIdentifiers)
        {
            foreach (string identifier in regionIdentifiers)
            {
                // Remove this region from regions dictionary and results
                this.RemoveRegion(identifier);

                // Remove from persistent store
                GeofenceStore.SharedInstance.Remove(identifier);
               
                // Notify monitoring was stopped
                CrossGeofence.GeofenceListener.OnMonitoringStopped(identifier);
            }

            // Stop Monitoring
            Android.Gms.Location.LocationServices.GeofencingApi.RemoveGeofences(this.modeGoogleApiClient, regionIdentifiers).SetResultCallback(this);

            // Check if there are still regions
            this.OnMonitoringRemoval();
        }

        /// <summary>
        /// Removes the region.
        /// </summary>
        /// <param name="regionIdentifier">Region identifier.</param>
        private void RemoveRegion(string regionIdentifier)
        {
            if (this.modeRegions.ContainsKey(regionIdentifier))
            {
                this.modeRegions.Remove(regionIdentifier);
            }

            if (this.modeGeofenceResults.ContainsKey(regionIdentifier))
            {
                this.modeGeofenceResults.Remove(regionIdentifier);
            }
        }
    }
}