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

            // set content of page
            Content = CreateMapContentView();

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

        View CreateMapContentView()
        {
            var viewModel = BindingContext as MapViewModel;
            var currentLocation = viewModel.getCurrentDevicePosition();

            var fieldMap = new FieldMap(MapSpan.FromCenterAndRadius(currentLocation, Distance.FromKilometers(1)));
            fieldMap.Fields = viewModel.Fields;

            fieldMap.MapType = MapType.Hybrid;

            fieldMap.BindingContext = BindingContext;

            return fieldMap;
        }
    }
}
