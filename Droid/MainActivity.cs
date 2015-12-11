using Android.App;
using Android.Content.PM;
using Android.OS;

using Xamarin;
using Android;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Java.IO;
using Android.Util;

namespace TreeWatch.Droid
{
	[Activity (Label = "TreeWatch", Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
        const int RequestLocationId = 0;
        const string tag = "mainactivity";

        readonly string [] PermissionsLocation = 
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation
        };

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                Log.Error(tag, "Version 23");
                System.Console.Error.WriteLine("Version 23");
                const string permission = Manifest.Permission.AccessFineLocation;
                if (ActivityCompat.CheckSelfPermission(Android.App.Application.Context, permission) == Permission.Denied)
                {
                    if (ActivityCompat.ShouldShowRequestPermissionRationale(this, permission))
                    {
                        Snackbar.Make(this.FindViewById(Android.Resource.Id.Content), "Location access is required to show coffee shops nearby.",
                            Snackbar.LengthIndefinite)
                            .SetAction("OK", v => ActivityCompat.RequestPermissions(this, PermissionsLocation, RequestLocationId))
                            .Show();
                    }
                    else
                    {
                        ActivityCompat.RequestPermissions(this, PermissionsLocation, RequestLocationId);
                    }
                }
            }
            else
            {
                System.Console.Error.WriteLine("Version <23");
                Log.Error(tag, "Version < 23");
            }

            CrossGeofence.Initialize<CrossGeofenceListener>();

			global::Xamarin.Forms.Forms.Init (this, savedInstanceState);
			FormsMaps.Init (this, savedInstanceState);

			// http://forums.xamarin.com/discussion/21148/calabash-and-xamarin-forms-what-am-i-missing
			Xamarin.Forms.Forms.ViewInitialized += (sender, e) => {
				if (!string.IsNullOrWhiteSpace (e.View.StyleId)) {
					e.NativeView.ContentDescription = e.View.StyleId;
				}
			};

			LoadApplication (new App ());
		}
	}
}

