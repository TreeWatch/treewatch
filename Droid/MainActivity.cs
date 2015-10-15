using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ExtendedCells.Forms.Plugin.Android;
using ExtendedMap.Forms.Plugin.Droid;
using SVG.Forms.Plugin.Droid;
using Xamarin;

namespace TreeWatch.Droid
{
	[Activity (Label = "TreeWatch.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
			FormsMaps.Init (this, bundle);
			SvgImageRenderer.Init ();
			ExtendedTextCellRenderer.Init ();
			ExtendedMapRenderer.Init ();

			// http://forums.xamarin.com/discussion/21148/calabash-and-xamarin-forms-what-am-i-missing
			Xamarin.Forms.Forms.ViewInitialized += (object sender, Xamarin.Forms.ViewInitializedEventArgs e) => {
				if (!string.IsNullOrWhiteSpace (e.View.StyleId)) {
					e.NativeView.ContentDescription = e.View.StyleId;
				}
			};

			LoadApplication (new App ());
		}
	}
}

