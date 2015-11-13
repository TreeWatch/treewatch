using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Diagnostics;

namespace TreeWatch
{
	public partial class MapContentPage : ContentPage
	{
		public MapContentPage (MapViewModel mapViewModel)
		{
			// initialize this page
			InitializeComponent ();

			// add view model
			this.BindingContext = mapViewModel;

			// set content of page
			Content = CreateMapContentView ();

//			this.Navigation.PushAsync (new DetailedInformationContentPage (mapViewModel));
		}

		View CreateMapContentView ()
		{
			//Coordinates for the starting point of the map

			var model = (MapViewModel)BindingContext;
			var location = model.getCurrentDevicePosition ();

			var map = new FieldMap (MapSpan.FromCenterAndRadius (location, Distance.FromKilometers (1)));
			map.Fields = model.Fields;

			map.MapType = MapType.Hybrid;

			map.BindingContext = BindingContext;

			var createMapContentView = map;

			return createMapContentView;
		}
	}
}
