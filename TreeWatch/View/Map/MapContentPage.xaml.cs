// <copyright file="MapContentPage.xaml.cs" company="TreeWatch">
// Copyright © 2015 TreeWatch
// </copyright>
#region Copyright
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
#endregion
namespace TreeWatch
{
    using Xamarin.Forms;
    using Xamarin.Forms.Maps;

    /// <summary>
    /// Map content page.
    /// </summary>
    public partial class MapContentPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.MapContentPage"/> class.
        /// </summary>
        /// <param name="mapViewModel">Map view model.</param>
        public MapContentPage(MapViewModel mapViewModel)
        {
            this.InitializeComponent();

            if (TargetPlatform.Android == Device.OS)
            {
                this.Title = "Map";
            }

            // add view model
            this.BindingContext = mapViewModel;

            this.SetupMapContentView();

            this.SetupToolbarItems();
        }

        /// <summary>
        /// Gets my location toolbar item.
        /// </summary>
        /// <returns>The my location toolbar item.</returns>
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

        /// <summary>
        /// Setups the map content view.
        /// </summary>
        internal void SetupMapContentView()
        {
            // Get current position
            var currentLocation = MapViewModel.GetCurrentDevicePosition();

            // Jump to the current location inside the map
            fieldMap.MoveToRegion(MapSpan.FromCenterAndRadius(currentLocation, Distance.FromKilometers(1)));

            // Get binding context of this view
            var viewModel = BindingContext as MapViewModel;

            // Add all fields into the map
            fieldMap.Fields = viewModel.Fields;

            // Change Map type to hybrid
            fieldMap.MapType = MapType.Hybrid;

            // set binding context of field map view to the same of this view
            fieldMap.BindingContext = this.BindingContext;
        }

        /// <summary>
        /// Setups the toolbar items.
        /// </summary>
        internal void SetupToolbarItems()
        {
            if (TargetPlatform.iOS == Device.OS)
            {
                ToolbarItems.Insert(0, GetMyLocationToolbarItem());
            }
        }
    }
}