using Xamarin.Forms;

namespace TreeWatch
{
    public partial class MapTypeContentPage : ContentPage
    {
        public MapTypeContentPage(SettingsViewModel settingsViewModel)
        {
            // initialize component
            InitializeComponent();

            // set view model
            BindingContext = settingsViewModel;

            mapTypes.ItemTapped += (sender, e) => (BindingContext as SettingsViewModel).NavigateToSettings(e.Item);
        }
    }
}