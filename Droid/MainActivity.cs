using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin;
using SVG.Forms.Plugin.Droid;
using ExtendedCells.Forms.Plugin.Android;
using ExtendedMap.Forms.Plugin.Droid;

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
			SvgImageRenderer.Init();
			ExtendedTextCellRenderer.Init ();
			ExtendedMapRenderer.Init ();

			LoadApplication (new App ());
		}
	}
}

