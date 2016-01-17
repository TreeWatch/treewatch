// <copyright file="MainActivity.cs" company="TreeWatch">
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
    using Android;
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Android.Support.Design.Widget;
    using Android.Support.V4.App;
    using Android.Util;
    using Xamarin;
    using Xamarin.Forms;

    /// <summary>
    /// Main activity.
    /// </summary>
    [Activity(Label = "TreeWatch", Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        /// <summary>
        /// The request location identifier.
        /// </summary>
        private const int RequestLocationId = 0;

        /// <summary>
        /// The tag.
        /// </summary>
        private const string Tag = "mainactivity";

        /// <summary>
        /// The permissions location.
        /// </summary>
        private readonly string[] permissionsLocation =
            {
                Manifest.Permission.AccessCoarseLocation,
                Manifest.Permission.AccessFineLocation
            };

        /// <summary>
        /// Raises the create event.
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                Log.Error(Tag, "Version 23");
                System.Console.Error.WriteLine("Version 23");
                const string StringPermission = Manifest.Permission.AccessFineLocation;
                if (Android.Support.V4.Content.ContextCompat.CheckSelfPermission(Android.App.Application.Context, StringPermission) == Permission.Denied)
                {
                    if (ActivityCompat.ShouldShowRequestPermissionRationale(this, StringPermission))
                    {
                        Snackbar.Make(
                            this.FindViewById(Android.Resource.Id.Content),
                            "Location access is required to show coffee shops nearby.",
                            Snackbar.LengthIndefinite)
                            .SetAction("OK", v => ActivityCompat.RequestPermissions(this, this.permissionsLocation, RequestLocationId))
                            .Show();
                    }
                    else
                    {
                        ActivityCompat.RequestPermissions(this, this.permissionsLocation, RequestLocationId);
                    }
                }
            }
            else
            {
                System.Console.Error.WriteLine("Version <23");
                Log.Error(Tag, "Version < 23");
            }

            CrossGeofence.Initialize<CrossGeofenceListener>();

            Forms.Init(this, savedInstanceState);
            FormsMaps.Init(this, savedInstanceState);

            // http://forums.xamarin.com/discussion/21148/calabash-and-xamarin-forms-what-am-i-missing
            Forms.ViewInitialized += (sender, e) =>
            {
                if (!string.IsNullOrWhiteSpace(e.View.StyleId))
                {
                    e.NativeView.ContentDescription = e.View.StyleId;
                }
            };

            this.LoadApplication(new App());
        }
    }
}