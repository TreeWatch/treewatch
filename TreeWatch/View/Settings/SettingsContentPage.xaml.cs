using Xamarin.Forms;

namespace TreeWatch
{
    public partial class SettingsContentPage : ContentPage
    {
        public SettingsContentPage(Page mapPage)
        {
            // initialize component
            InitializeComponent();

            // set view model
            if (Device.OS == TargetPlatform.iOS)
            {
                BindingContext = new SettingsViewModel((mapPage as MapNavigationPage).CurrentPage as MapContentPage);
            }
            else if (Device.OS == TargetPlatform.Android)
            {
                BindingContext = new SettingsViewModel(mapPage as MapContentPage);
            }

            mapType.Tapped += (sender, e) => (BindingContext as SettingsViewModel).NavigateToMapType();
        }
    }
}