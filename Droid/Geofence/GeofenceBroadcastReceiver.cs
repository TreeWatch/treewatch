// <copyright file="GeofenceBroadcastReceiver.cs" company="TreeWatch">
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
    using Android.App;
    using Android.Content;
    using Android.Support.V4.Content;

    /// <summary>
    /// GeofenceBootReceiver class
    /// Receive geofence updates
    /// </summary>
    [BroadcastReceiver]
    [IntentFilter(new[] { "nl.gtl.treewatch.ACTION_RECEIVE_GEOFENCE" })]
    public class GeofenceBroadcastReceiver : WakefulBroadcastReceiver
    {
        /// <Docs>The Context in which the receiver is running.</Docs>
        /// <summary>
        /// This method is called when the BroadcastReceiver is receiving an Intent
        ///  broadcast.
        /// </summary>
        /// <param name="context">Context object.</param>
        /// <param name="intent">Intent object.</param>
        public override void OnReceive(Context context, Intent intent)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} - {1}", CrossGeofence.Id, "Region State Change Received"));
            var serviceIntent = new Intent(context, typeof(GeofenceTransitionsIntentService));
            serviceIntent.AddFlags(ActivityFlags.IncludeStoppedPackages);
            serviceIntent.ReplaceExtras(intent.Extras);
            serviceIntent.SetAction(intent.Action);
            WakefulBroadcastReceiver.StartWakefulService(context, serviceIntent);

            this.ResultCode = Result.Ok;
        }
    }
}