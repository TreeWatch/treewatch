using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TreeWatch
{
    public partial class MapContentPage : ContentPage
    {
        public MapContentPage(MapViewModel mapViewModel)
        {
            // initialize this page
            InitializeComponent();

            // add view model
            BindingContext = mapViewModel;

            setupMapContentView();

            if (TargetPlatform.Android == Device.OS)
            {
                Title = "Map";
            }

            if (TargetPlatform.iOS == Device.OS)
            {
                var myLocationToolBarItem = new ToolbarItem();
                myLocationToolBarItem.Icon = Device.OS == TargetPlatform.iOS ? "Icons/MyLocationIcon.png" : "MyLocationIcon.png";
                ToolbarItems.Insert(0, myLocationToolBarItem);
            }
        }

        void setupMapContentView()
        {
            var viewModel = BindingContext as MapViewModel;
            var currentLocation = viewModel.getCurrentDevicePosition();

            fieldMap.MoveToRegion(MapSpan.FromCenterAndRadius(currentLocation, Distance.FromKilometers(1)));
            fieldMap.Fields = viewModel.Fields;

            fieldMap.MapType = MapType.Hybrid;

            fieldMap.BindingContext = BindingContext;
        }
    }
}
