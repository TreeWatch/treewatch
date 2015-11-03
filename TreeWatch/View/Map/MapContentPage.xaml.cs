using System;
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

			Title = "Map";
			if (Device.OS == TargetPlatform.iOS) {
				//Icon = "MapTabBarIcon.png";
			}

			BindingContext = new MapViewModel ();

			//configurations for navigation bar
			NavigationPage.SetBackButtonTitle (this, Title);

			this.Content = CreateMapContentView ();
		}

		View CreateMapContentView ()
		{
			//Coordinates for the starting point of the map

			MapViewModel model = (MapViewModel)BindingContext;
			Position location = model.getCurrentDevicePosition ();

			var map = new FieldMap (MapSpan.FromCenterAndRadius (location, Distance.FromKilometers (1)));

			map.MapType = MapType.Hybrid;

			var field = new Field("TestField");
			var fieldcords = new List<Position>();

			fieldcords.Add (new Position (51.39202, 6.04745));
			fieldcords.Add (new Position (51.39202, 6.05116));
			fieldcords.Add (new Position (51.38972, 6.05116));
			fieldcords.Add (new Position (51.38972, 6.04745));
			field.BoundingCordinates = fieldcords;
			map.Fields.Add (field);

			map.BindingContext = BindingContext;

			var createMapContentView = map;

			return createMapContentView;
		}
	}
}

