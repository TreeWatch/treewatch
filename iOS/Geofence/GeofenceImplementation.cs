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
[assembly: Xamarin.Forms.Dependency(typeof(TreeWatch.iOS.GeofenceImplementation))]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", MessageId = "Ctl", Scope = "namespace", Target = "Assembly name", Justification = "Auto generated name")]

// Analysis disable once InconsistentNaming
namespace TreeWatch.iOS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CoreLocation;

    using Foundation;

    using TreeWatch.iOS;

    using UIKit;

    /// <summary>
    /// Geofence implementation.
    /// </summary>
    public class GeofenceImplementation : IGeofence
    {
        /// <summary>
        /// The view action.
        /// </summary>
        private const string ViewAction = "View";

        /// <summary>
        /// The location manager.
        /// </summary>
        private readonly CLLocationManager locationManager;

        /// <summary>
        /// The geofence circular regions.
        /// </summary>
        private Dictionary<string, GeofenceCircularRegion> regions = GeofenceStore.SharedInstance.GetAll();

        /// <summary>
        /// The geofence results.
        /// </summary>
        private Dictionary<string, GeofenceResult> geofenceResults;

        /// <summary>
        /// The last known geofence location.
        /// </summary>
        private GeofenceLocation lastKnownGeofenceLocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.iOS.GeofenceImplementation"/> class.
        /// </summary>
        public GeofenceImplementation()
        {
            this.geofenceResults = new Dictionary<string, GeofenceResult>();

            this.locationManager = new CLLocationManager();
            this.locationManager.DidStartMonitoringForRegion += GeofenceImplementation.DidStartMonitoringForRegion;
            this.locationManager.RegionEntered += this.RegionEntered;
            this.locationManager.RegionLeft += this.RegionLeft;
            this.locationManager.Failed += this.OnFailure;
            this.locationManager.DidDetermineState += this.DidDetermineState;
            this.locationManager.LocationsUpdated += this.LocationsUpdated;

            this.locationManager.RequestAlwaysAuthorization();
            string priorityType = "Balanced Power";
            switch (CrossGeofence.GeofencePriority)
            {
                case GeofencePriority.HighAccuracy:
                    priorityType = "High Accuracy";
                    this.locationManager.DesiredAccuracy = CLLocation.AccuracyBest;
                    break;
                case GeofencePriority.AcceptableAccuracy:
                    priorityType = "Acceptable Accuracy";
                    this.locationManager.DesiredAccuracy = CLLocation.AccuracyNearestTenMeters;
                    break;
                case GeofencePriority.MediumAccuracy:
                    priorityType = "Medium Accuracy";
                    this.locationManager.DesiredAccuracy = CLLocation.AccuracyHundredMeters;
                    break;
                case GeofencePriority.LowAccuracy:
                    priorityType = "Low Accuracy";
                    this.locationManager.DesiredAccuracy = CLLocation.AccuracyKilometer;
                    break;
                case GeofencePriority.LowestAccuracy:
                    priorityType = "Lowest Accuracy";
                    this.locationManager.DesiredAccuracy = CLLocation.AccuracyThreeKilometers;
                    break;
                default:
                    this.locationManager.DesiredAccuracy = CLLocation.AccurracyBestForNavigation;
                    break;
            }

            System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}: {2}", CrossGeofence.Id, "Location priority set to", priorityType));

            if (CrossGeofence.SmallestDisplacement > 0)
            {
                this.locationManager.DistanceFilter = CrossGeofence.SmallestDisplacement;
                System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}: {2} meters", CrossGeofence.Id, "Location smallest displacement set to", CrossGeofence.SmallestDisplacement));
            }

            if (this.locationManager.MonitoredRegions.Count > 0 && this.IsMonitoring)
            {
                NSSet monitoredRegions = this.locationManager.MonitoredRegions;

                foreach (CLCircularRegion region in monitoredRegions)
                {
                    // If not on regions remove on startup since that region was set not persistent
                    if (!this.Regions.ContainsKey(region.Identifier))
                    {
                        this.locationManager.StopMonitoring(region);
                    }
                    else
                    {
                        this.locationManager.RequestState(region);
                    }
                }

                this.locationManager.StartMonitoringSignificantLocationChanges();

                string message = string.Format("{0} - {1} {2} region(s)", CrossGeofence.Id, "Actually monitoring", this.locationManager.MonitoredRegions.Count);
                System.Diagnostics.Debug.WriteLine(message);
            }

            this.SetLastKnownLocation(this.locationManager.Location);
        }

        /// <summary>
        /// Gets the Monitored regions
        /// </summary>
        public IReadOnlyDictionary<string, GeofenceCircularRegion> Regions
        { 
            get { return this.regions; } 
        }

        /// <summary>
        /// Gets a Dicitonary that contains all geofence results received
        /// </summary>
        /// <value>The geofence results.</value>
        public IReadOnlyDictionary<string, GeofenceResult> GeofenceResults
        { 
            get { return this.geofenceResults; } 
        }

        /// <summary>
        /// Gets a value indicating whether at least one region is been monitored
        /// </summary>
        /// <value><c>true</c> if this instance is monitoring; otherwise, <c>false</c>.</value>
        public bool IsMonitoring
        { 
            get { return this.regions.Count > 0; } 
        }

        /// <summary>
        /// Gets the last known geofence location
        /// </summary>
        /// <value>The last known location.</value>
        public GeofenceLocation LastKnownLocation
        { 
            get { return this.lastKnownGeofenceLocation; } 
        }

        /// <summary>
        /// Checks if has passed to stayed state
        /// </summary>
        /// <returns>The if stayed.</returns>
        /// <param name="regionId">Region identifier.</param>
        public static async Task CheckIfStayed(string regionId)
        {
            if (CrossGeofence.Current.GeofenceResults.ContainsKey(regionId) && CrossGeofence.Current.Regions.ContainsKey(regionId) && CrossGeofence.Current.Regions[regionId].NotifyOnStay && CrossGeofence.Current.GeofenceResults[regionId].Transition == GeofenceTransition.Entered && Math.Abs(CrossGeofence.Current.Regions[regionId].StayedInThresholdDuration.TotalMilliseconds) > float.Epsilon)
            {
                await Task.Delay((int)CrossGeofence.Current.Regions[regionId].StayedInThresholdDuration.TotalMilliseconds);

                if (CrossGeofence.Current.GeofenceResults[regionId].LastExitTime == null && CrossGeofence.Current.GeofenceResults[regionId].Transition != GeofenceTransition.Stayed)
                {
                    CrossGeofence.Current.GeofenceResults[regionId].Transition = GeofenceTransition.Stayed;

                    CrossGeofence.GeofenceListener.OnRegionStateChanged(CrossGeofence.Current.GeofenceResults[regionId]);

                    if (CrossGeofence.Current.Regions[regionId].ShowNotification)
                    {
                        GeofenceImplementation.CreateNotification(ViewAction, string.IsNullOrEmpty(CrossGeofence.Current.Regions[regionId].NotificationStayMessage) ? CrossGeofence.Current.GeofenceResults[regionId].ToString() : CrossGeofence.Current.Regions[regionId].NotificationStayMessage);
                    }
                }
            }
        }

        /// <summary>
        /// Availables for monitoring.
        /// </summary>
        /// <returns><c>true</c>, if for monitoring was availabled, <c>false</c> otherwise.</returns>
        public bool AvailableForMonitoring()
        {
            bool retVal = false;
            this.RequestAlwaysAuthorization();

            if (!CLLocationManager.LocationServicesEnabled)
            {
                string message = string.Format("{0} - {1}", CrossGeofence.Id, "You need to enable Location Services");
                System.Diagnostics.Debug.WriteLine(message);
                CrossGeofence.GeofenceListener.OnError(message);
            }
            else if (CLLocationManager.Status == CLAuthorizationStatus.Denied || CLLocationManager.Status == CLAuthorizationStatus.Restricted)
            {
                string message = string.Format("{0} - {1}", CrossGeofence.Id, "You need to authorize Location Services");
                System.Diagnostics.Debug.WriteLine(message);
                CrossGeofence.GeofenceListener.OnError(message);
            }
            else if (CLLocationManager.IsMonitoringAvailable(typeof(CLRegion)))
            {
                var settings = UIUserNotificationSettings.GetSettingsForTypes(
                                   UIUserNotificationType.Alert
                                   | UIUserNotificationType.Badge
                                   | UIUserNotificationType.Sound,
                                   new NSSet());
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);

                retVal = true;
            }
            else
            {
                string message = string.Format("{0} - {1}", CrossGeofence.Id, "Not available for monitoring");
                System.Diagnostics.Debug.WriteLine(message);
                CrossGeofence.GeofenceListener.OnError(message);
            }

            return retVal;
        }

        /// <summary>
        /// Starts monitoring region
        /// </summary>
        /// <param name="region">Region to monitor</param>
        public void StartMonitoring(GeofenceCircularRegion region)
        {
            if (this.AvailableForMonitoring())
            {
                if (!this.regions.ContainsKey(region.Id))
                {
                    this.regions.Add(region.Id, region);
                }
                else
                {
                    this.regions[region.Id] = region;
                }

                GeofenceStore.SharedInstance.Save(region);

                if (this.Regions.Count > 20 && this.locationManager.MonitoredRegions.Count == 20)
                {
                    this.RecalculateRegions();
                }
                else
                {
                    this.AddRegion(region);
                }

                this.locationManager.StartMonitoringSignificantLocationChanges();
            }
        }

        /// <summary>
        /// Start monitoring regions
        /// </summary>
        /// <param name="regions"> Regions to monitor</param>
        public void StartMonitoring(IList<GeofenceCircularRegion> regions)
        {
            if (this.AvailableForMonitoring())
            {
                foreach (var region in regions)
                {
                    if (!this.regions.ContainsKey(region.Id))
                    {
                        this.regions.Add(region.Id, region);
                    }
                    else
                    {
                        this.regions[region.Id] = region;
                    }

                    GeofenceStore.SharedInstance.Save(region);
                }

                if (this.Regions.Count > 20 && this.locationManager.MonitoredRegions.Count == 20)
                {
                    this.RecalculateRegions();
                }
                else
                {
                    foreach (var region in regions)
                    {
                        this.AddRegion(region);
                    }
                }

                this.locationManager.StartMonitoringSignificantLocationChanges();
            }
        }

        /// <summary>
        /// Stops monitoring all regions
        /// </summary>
        public void StopMonitoringAllRegions()
        {
            if (this.AvailableForMonitoring())
            {
                GeofenceStore.SharedInstance.RemoveAll();

                foreach (CLCircularRegion region in this.locationManager.MonitoredRegions)
                {
                    this.locationManager.StopMonitoring(region);
                }

                this.locationManager.StopMonitoringSignificantLocationChanges();
                this.regions.Clear();
                this.geofenceResults.Clear();
                CrossGeofence.GeofenceListener.OnMonitoringStopped();
            }
        }

        /// <summary>
        /// Stops monitoring region
        /// </summary>
        /// <param name="regionIdentifier">Region to stop monitoring</param>
        public void StopMonitoring(string regionIdentifier)
        {
            if (CLLocationManager.IsMonitoringAvailable(typeof(CLRegion)))
            {
                this.RemoveRegionMonitoring(regionIdentifier);

                CrossGeofence.GeofenceListener.OnMonitoringStopped(regionIdentifier);

                if (this.regions.Count == 0)
                {
                    CrossGeofence.GeofenceListener.OnMonitoringStopped();
                }
            }
        }

        /// <summary>
        /// Stop monitoring regions
        /// </summary>
        /// <param name="regionIdentifiers">Region to stop monitoring</param>
        public void StopMonitoring(IList<string> regionIdentifiers)
        {
            if (this.AvailableForMonitoring())
            {
                foreach (string regionIdentifier in regionIdentifiers)
                {
                    this.StopMonitoring(regionIdentifier);
                }
            }
        }

        /// <summary>
        /// Get current 20 monitored regions.
        /// </summary>
        /// <param name="regions">List of all regions</param>
        /// <returns>current 20 monitored regions</returns>
        public IList<GeofenceCircularRegion> GetCurrentRegions(IList<GeofenceCircularRegion> regions)
        {
            IList<GeofenceCircularRegion> nearestRegions;

            if (regions.Count > 20)
            {
                if (this.locationManager.Location != null)
                {
                    IEnumerable<GeofenceCircularRegion> newRegions = regions.OrderBy(r => GeofenceImplementation.CalculateDistance(this.locationManager.Location.Coordinate.Latitude, this.locationManager.Location.Coordinate.Longitude, r.Latitude, r.Longitude)).Take(20);

                    nearestRegions = newRegions.ToList();
                }
                else
                {
                    nearestRegions = regions.Take(20).ToList();
                }
            }
            else
            {
                nearestRegions = regions;
            }

            return nearestRegions;
        }

        /// <summary>
        /// Calculates distance between two locations
        /// </summary>
        /// <param name="lat1">Latitude first point</param>
        /// <param name="lon1">Longitude first point</param>
        /// <param name="lat2">Latitude second point</param>
        /// <param name="lon2">Longitude second point</param>
        /// <returns>The Distance</returns>
        private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6372.8; // In kilometers
            double lat = Math.PI * (lat2 - lat1) / 180.0; 
            double lon = Math.PI * (lon2 - lon1) / 180.0; 
            lat1 = Math.PI * lat1 / 180.0; 
            lat2 = Math.PI * lat2 / 180.0; 

            double a = (Math.Sin(lat / 2) * Math.Sin(lat / 2)) + (Math.Sin(lon / 2) * Math.Sin(lon / 2) * Math.Cos(lat1) * Math.Cos(lat2));

            return (R * 2 * Math.Asin(Math.Sqrt(a))) * 1000; // meters
        }

        /// <summary>
        /// Dids the start monitoring for region.
        /// </summary>
        /// <param name="sender">Sender wo fired the event.</param>
        /// <param name="e">Event args.</param>
        private static void DidStartMonitoringForRegion(object sender, CLRegionEventArgs e)
        {
            CrossGeofence.GeofenceListener.OnMonitoringStarted(e.Region.Identifier); 
        }

        /// <summary>
        /// Creates the notification.
        /// </summary>
        /// <param name="title">Title of the notification.</param>
        /// <param name="message">Message of the notification.</param>
        private static void CreateNotification(string title, string message)
        {
            var notification = new UILocalNotification();

            notification.AlertAction = title;
            notification.AlertBody = message;
            notification.HasAction = true;

            notification.SoundName = UILocalNotification.DefaultSoundName;
            #if __UNIFIED__
            UIApplication.SharedApplication.PresentLocalNotificationNow(notification);
            #else
            UIApplication.SharedApplication.PresentLocationNotificationNow(notification);
            #endif
        }

        /// <summary>
        /// Sets the last known location.
        /// </summary>
        /// <param name="location">Location to set.</param>
        private void SetLastKnownLocation(CLLocation location)
        {
            if (location != null)
            {
                if (this.lastKnownGeofenceLocation == null)
                {
                    this.lastKnownGeofenceLocation = new GeofenceLocation();
                }

                this.lastKnownGeofenceLocation.Latitude = location.Coordinate.Latitude;
                this.lastKnownGeofenceLocation.Longitude = location.Coordinate.Longitude;
                this.lastKnownGeofenceLocation.Accuracy = location.HorizontalAccuracy;
                DateTime referenceDate = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2001, 1, 1, 0, 0, 0));
                referenceDate.AddSeconds(location.Timestamp.SecondsSinceReferenceDate);

                this.lastKnownGeofenceLocation.Date = referenceDate;
            }
        }

        /// <summary>
        /// Locationses the updated.
        /// </summary>
        /// <param name="sender">Sender who fired the event.</param>
        /// <param name="e">Event arguemnts</param>
        private void LocationsUpdated(object sender, CLLocationsUpdatedEventArgs e)
        {
            CLLocation lastLocation = e.Locations[e.Locations.Length - 1];

            this.SetLastKnownLocation(lastLocation);

            if (this.Regions.Count > 20 && this.locationManager.MonitoredRegions.Count == 20)
            {
                this.RecalculateRegions();
            }
            else
            {
                // Check any current monitored regions not in loaded persistent regions and stop monitoring them
                foreach (CLCircularRegion region in this.locationManager.MonitoredRegions)
                {
                    if (!this.Regions.ContainsKey(region.Identifier))
                    {
                        this.locationManager.StopMonitoring(region);
                        string message = string.Format("{0} - Stopped monitoring region {1} wasn't in persistent loaded regions", CrossGeofence.Id, region.Identifier);
                        System.Diagnostics.Debug.WriteLine(message);
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}: {2},{3}", CrossGeofence.Id, "Location update", lastLocation.Coordinate.Latitude, lastLocation.Coordinate.Longitude));
        }

        /// <summary>
        /// Gets called after State is determined.
        /// </summary>
        /// <param name="sender">Sender who fired the event.</param>
        /// <param name="e">Event arguemnts</param>>
        private void DidDetermineState(object sender, CLRegionStateDeterminedEventArgs e)
        {
            switch (e.State)
            {
                case CLRegionState.Inside:
                    System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}", CrossGeofence.Id, "StartedRegion: " + e.Region));
                    this.OnRegionEntered(e.Region);
                    break;
                case CLRegionState.Outside:
                    break;
                default:
                    string message = string.Format("{0} - {1}", CrossGeofence.Id, "Unknown region state");
                    System.Diagnostics.Debug.WriteLine(message);
                    break;
            }
        }

        /// <summary>
        /// Get Called on Failure.
        /// </summary>
        /// <param name="sender">Sender who fired the event.</param>
        /// <param name="e">Event arguemnts</param>
        private void OnFailure(object sender, NSErrorEventArgs e)
        {
            if (this.IsMonitoring)
            {
                this.StopMonitoringAllRegions();
            }

            CrossGeofence.GeofenceListener.OnError(e.Error.LocalizedDescription);
        }

        /// <summary>
        /// Get Called on RegionEntered.
        /// </summary>
        /// <param name="sender">Sender who fired the event.</param>
        /// <param name="e">Event arguemnts</param>
        private void RegionEntered(object sender, CLRegionEventArgs e)
        {
            this.OnRegionEntered(e.Region);
        }

        /// <summary>
        /// Get Called on RegionEntered.
        /// </summary>
        /// <param name="region">Region entered.</param>
        private async void OnRegionEntered(CLRegion region)
        {
            if (this.GeofenceResults.ContainsKey(region.Identifier) && this.GeofenceResults[region.Identifier].Transition == GeofenceTransition.Entered)
            {
                return;
            }

            if (!this.geofenceResults.ContainsKey(region.Identifier))
            {
                this.geofenceResults.Add(
                    region.Identifier,
                    new GeofenceResult
                    {
                        RegionId = region.Identifier
                    });
            }

            if (this.LastKnownLocation != null)
            {
                this.geofenceResults[region.Identifier].Latitude = this.LastKnownLocation.Latitude;
                this.geofenceResults[region.Identifier].Longitude = this.LastKnownLocation.Longitude;
                this.geofenceResults[region.Identifier].Accuracy = this.LastKnownLocation.Accuracy;
            }
            else
            {
                this.geofenceResults[region.Identifier].Latitude = region.Center.Latitude;
                this.geofenceResults[region.Identifier].Longitude = region.Center.Longitude;
                this.geofenceResults[region.Identifier].Accuracy = region.Radius;
            }

            this.geofenceResults[region.Identifier].LastEnterTime = DateTime.Now;
            this.geofenceResults[region.Identifier].LastExitTime = null;
            this.geofenceResults[region.Identifier].Transition = GeofenceTransition.Entered;

            if (region.NotifyOnEntry)
            {
                CrossGeofence.GeofenceListener.OnRegionStateChanged(this.geofenceResults[region.Identifier]);

                if (this.Regions.ContainsKey(region.Identifier) && this.Regions[region.Identifier].ShowNotification)
                {
                    GeofenceImplementation.CreateNotification(ViewAction, string.IsNullOrEmpty(this.Regions[region.Identifier].NotificationEntryMessage) ? this.GeofenceResults[region.Identifier].ToString() : this.Regions[region.Identifier].NotificationEntryMessage);
                }
            }

            // Checks if device has stayed asynchronously
            await GeofenceImplementation.CheckIfStayed(region.Identifier);
        }

        /// <summary>
        /// Get called when Region is left
        /// </summary>
        /// <param name="sender">Sender who fired the event.</param>
        /// <param name="e">Event arguemnts</param>
        private void RegionLeft(object sender, CLRegionEventArgs e)
        {
            if (!this.GeofenceResults.ContainsKey(e.Region.Identifier) || this.GeofenceResults[e.Region.Identifier].Transition != GeofenceTransition.Exited)
            {
                this.OnRegionLeft(e.Region);
            }
        }

        /// <summary>
        /// Get called when Region is left
        /// </summary>
        /// <param name="region">Region thats left.</param>
        private void OnRegionLeft(CLRegion region)
        {
            if (!this.geofenceResults.ContainsKey(region.Identifier))
            {
                this.geofenceResults.Add(
                    region.Identifier, 
                    new GeofenceResult
                    {
                        RegionId = region.Identifier
                    });
            }

            if (this.LastKnownLocation != null)
            {
                this.geofenceResults[region.Identifier].Latitude = this.LastKnownLocation.Latitude;
                this.geofenceResults[region.Identifier].Longitude = this.LastKnownLocation.Longitude;
                this.geofenceResults[region.Identifier].Accuracy = this.LastKnownLocation.Accuracy;
            }
            else
            {
                this.geofenceResults[region.Identifier].Latitude = region.Center.Latitude;
                this.geofenceResults[region.Identifier].Longitude = region.Center.Longitude;
                this.geofenceResults[region.Identifier].Accuracy = region.Radius;
            }

            this.geofenceResults[region.Identifier].LastExitTime = DateTime.Now;
            this.geofenceResults[region.Identifier].Transition = GeofenceTransition.Exited;

            CrossGeofence.GeofenceListener.OnRegionStateChanged(this.geofenceResults[region.Identifier]);

            if (this.Regions[region.Identifier].ShowNotification)
            {
                GeofenceImplementation.CreateNotification(ViewAction, string.IsNullOrEmpty(this.Regions[region.Identifier].NotificationExitMessage) ? this.GeofenceResults[region.Identifier].ToString() : this.Regions[region.Identifier].NotificationExitMessage);
            }
        }

        /// <summary>
        /// Recalculates the regions.
        /// </summary>
        private void RecalculateRegions()
        {
            IList<GeofenceCircularRegion> tmpRegions = this.Regions.Values.ToList();

            // Stop all monitored regions
            foreach (CLCircularRegion region in this.locationManager.MonitoredRegions)
            {
                this.locationManager.StopMonitoring(region);
            }

            IList<GeofenceCircularRegion> nearestRegions = this.GetCurrentRegions(tmpRegions);

            foreach (GeofenceCircularRegion region in nearestRegions)
            {
                this.AddRegion(region);
            }

            string message = string.Format("{0} - {1}", CrossGeofence.Id, "Restarted monitoring to nearest 20 regions");
            System.Diagnostics.Debug.WriteLine(message);
        }

        /// <summary>
        /// Adds the region.
        /// </summary>
        /// <param name="region">Region to add.</param>
        private void AddRegion(GeofenceCircularRegion region)
        {
            CLRegion addingRegion;

            addingRegion = UIDevice.CurrentDevice.CheckSystemVersion(7, 0)
                ? new CLCircularRegion(new CLLocationCoordinate2D(region.Latitude, region.Longitude), (region.Radius > this.locationManager.MaximumRegionMonitoringDistance) ? this.locationManager.MaximumRegionMonitoringDistance : region.Radius, region.Id) 
                : new CLRegion(new CLLocationCoordinate2D(region.Latitude, region.Longitude), (region.Radius > this.locationManager.MaximumRegionMonitoringDistance) ? this.locationManager.MaximumRegionMonitoringDistance : region.Radius, region.Id);
            
            addingRegion.NotifyOnEntry = region.NotifyOnEntry || region.NotifyOnStay;
            addingRegion.NotifyOnExit = region.NotifyOnExit;
            this.locationManager.StartMonitoring(addingRegion);
            this.locationManager.RequestState(addingRegion);
        }

        /// <summary>
        /// Removes the region from monitoring.
        /// </summary>
        /// <param name="regionIdentifier">Region identifier.</param>
        private void RemoveRegionMonitoring(string regionIdentifier)
        {
            if (this.regions.ContainsKey(regionIdentifier))
            {
                this.regions.Remove(regionIdentifier);
            }

            if (this.geofenceResults.ContainsKey(regionIdentifier))
            {
                this.geofenceResults.Remove(regionIdentifier);
            }

            GeofenceStore.SharedInstance.Remove(regionIdentifier);

            var region = this.GetRegion(regionIdentifier);
            if (region != null)
            {
                this.locationManager.StopMonitoring(region);
            }
        }

        /// <summary>
        /// Gets the region.
        /// </summary>
        /// <returns>The region.</returns>
        /// <param name="identifier">Identifier of the region to get.</param>
        private CLRegion GetRegion(string identifier)
        {
            CLRegion region = null;
            foreach (CLRegion r in this.locationManager.MonitoredRegions)
            {
                if (r.Identifier.Equals(identifier, StringComparison.Ordinal))
                {
                    region = r;
                    break;
                }
            }

            return region;
        }

        /// <summary>
        /// Requests the always authorization.
        /// </summary>
        private void RequestAlwaysAuthorization()
        {
            CLAuthorizationStatus status = CLLocationManager.Status;
            if (status == CLAuthorizationStatus.AuthorizedWhenInUse || status == CLAuthorizationStatus.Denied)
            {
                string title = (status == CLAuthorizationStatus.Denied) ? "Location services are off" : "Background location is not enabled";
                const string Message = "To use background location you must turn on 'Always' in the Location Services Settings";

                var alertView = new UIAlertView(title, Message, null, "Cancel", "Settings");

                alertView.Clicked += (sender, buttonArgs) =>
                {
                    if (buttonArgs.ButtonIndex == 1)
                    {
                        // Send the user to the Settings for this app
                        var settingsUrl = new NSUrl(UIApplication.OpenSettingsUrlString);
                        UIApplication.SharedApplication.OpenUrl(settingsUrl);
                    }
                };

                alertView.Show();
            }
            else if (status == CLAuthorizationStatus.NotDetermined)
            {
                this.locationManager.RequestAlwaysAuthorization();
            }
        }
    }
}