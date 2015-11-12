using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TreeWatch
{
	public partial class MapContentPage : ContentPage
	{
		public MapContentPage ()
		{
			InitializeComponent ();

			//configurations for navigation bar
			NavigationPage.SetBackButtonTitle (this, Title);

			Content = CreateMapContentView ();
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
