using Xamarin.Forms;

namespace TreeWatch
{
	public partial class SettingsContentPage : ContentPage
	{
		public SettingsContentPage (Page mapPage)
		{
			// initialize component
			InitializeComponent ();

			// set view model
			if (Device.OS == TargetPlatform.iOS) {
				this.BindingContext = new SettingsViewModel ((mapPage as MapNavigationPage).CurrentPage as MapContentPage);
			} else if (Device.OS == TargetPlatform.Android) {
				this.BindingContext = new SettingsViewModel (mapPage as MapContentPage);
			}

			mapType.Tapped += (sender, e) => ((SettingsViewModel)this.BindingContext).NavigateToMapType ();
		}
	}
}

