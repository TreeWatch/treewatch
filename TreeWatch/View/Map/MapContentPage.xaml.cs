using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Diagnostics;

namespace TreeWatch
{
    public partial class MapContentPage : ContentPage
    {
        public MapContentPage(MapViewModel mapViewModel)
        {
            // initialize this page
            InitializeComponent();

            // add view model
            this.BindingContext = mapViewModel;

            // set content of page
            Content = CreateMapContentView();

            if (TargetPlatform.Android == Device.OS)
            {
                this.Title = "Map";
            }

            var clearLayersToolBarItem = new ToolbarItem();
            clearLayersToolBarItem.Icon = Device.OS == TargetPlatform.iOS ? "Icons/ClearLayersIcon" : "ClearLayersIcon";
            ToolbarItems.Add(clearLayersToolBarItem);
            var layersToolBarItem = new ToolbarItem();
            layersToolBarItem.Icon = Device.OS == TargetPlatform.iOS ? "Icons/LayersIcon" : "LayersIcon";
            ToolbarItems.Add(layersToolBarItem);
        }

        View CreateMapContentView()
        {
            var viewModel = (MapViewModel)BindingContext;
            var currentLocation = viewModel.getCurrentDevicePosition();

            var fieldMap = new FieldMap(MapSpan.FromCenterAndRadius(currentLocation, Distance.FromKilometers(1)));
            fieldMap.Fields = viewModel.Fields;

            fieldMap.MapType = MapType.Hybrid;

            fieldMap.BindingContext = BindingContext;

            return fieldMap;
        }
    }
}
