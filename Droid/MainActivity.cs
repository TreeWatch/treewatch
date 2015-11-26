using Android.App;
using Android.Content.PM;
using Android.OS;

using Xamarin;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services;
using Xamarin.Forms;
using XLabs.Platform.Services.Geolocation;

namespace TreeWatch.Droid
{
	[Activity (Label = "TreeWatch.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			global::Xamarin.Forms.Forms.Init (this, savedInstanceState);
			FormsMaps.Init (this, savedInstanceState);

			// http://forums.xamarin.com/discussion/21148/calabash-and-xamarin-forms-what-am-i-missing
			Xamarin.Forms.Forms.ViewInitialized += (sender, e) => {
				if (!string.IsNullOrWhiteSpace (e.View.StyleId)) {
					e.NativeView.ContentDescription = e.View.StyleId;
				}
			};
			var container = new SimpleContainer ();
			container.Register<IDevice>(t => AndroidDevice.CurrentDevice);
			container.Register<IDisplay>(t => t.Resolve<IDevice>().Display);
			container.Register<INetwork>(t => t.Resolve<IDevice>().Network);
			Resolver.SetResolver(container.GetResolver());
			DependencyService.Register<Geolocator> ();

			LoadApplication (new App ());
		}
	}
}

