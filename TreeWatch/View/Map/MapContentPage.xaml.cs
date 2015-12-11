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

            if (TargetPlatform.Android == Device.OS)
            {
                Title = "Map";
            }

            // add view model
            BindingContext = mapViewModel;

            SetupMapContentView();

            SetupToolbarItems();
        }

        /**
         * This method configurate the field map view inside the xaml
         */
        internal void SetupMapContentView()
        {
            // Get binding context of this view
            var viewModel = BindingContext as MapViewModel;
            // Get current position
            var currentLocation = viewModel.GetCurrentDevicePosition();

            // Jump to the current location inside the map
            fieldMap.MoveToRegion(MapSpan.FromCenterAndRadius(currentLocation, Distance.FromKilometers(1)));

            // Add all fields into the map
            fieldMap.Fields = viewModel.Fields;

            // Change Map type to hybrid
            fieldMap.MapType = MapType.Hybrid;

            // set binding context of field map view to the same of this view
            fieldMap.BindingContext = BindingContext;
        }

        /**
         * This method create and configurate all toolbar items
         */
        internal void SetupToolbarItems()
        {
            if (TargetPlatform.iOS == Device.OS)
            {
                ToolbarItems.Insert(0, GetMyLocationToolbarItem());
            }
        }

        /**
         * This method create the toolbar item that should jump the map view to current location
         */
        internal static ToolbarItem GetMyLocationToolbarItem()
        {
            var myLocationToolBarItem = new ToolbarItem();

            // Set clicked event
            myLocationToolBarItem.Clicked += (sender, e) => FieldHelper.Instance.CenterUserPostionEvent();

            // Design Toolbar Item
            myLocationToolBarItem.Icon = Device.OS == TargetPlatform.iOS ? "Icons/MyLocationIcon.png" : "MyLocationIcon.png";
            // Set style id for ui testing
            myLocationToolBarItem.StyleId = "MMyLocationButton";

            return myLocationToolBarItem;
        }
    }
}
