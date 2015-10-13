using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using ExtendedMap.Forms.Plugin.Abstractions;


namespace TreeWatch
{
	public partial class MapContentPage : ContentPage
	{
		public MapContentPage ()
		{
			InitializeComponent ();

<<<<<<< Upstream, based on origin/master
			#if __ANDROID__
			this.Content = new ExtendedMap ();
			#endif

			//site configurations
			Title = "Map";
			NavigationPage.SetBackButtonTitle (this, Title);
=======
			NavigationPage.SetBackButtonTitle (this, "Back");
>>>>>>> ed096cc Added ExtendedMap Plugin to replace previous map This closes #5  #324 time:4 comment:Added ExtendedMap Plugin

<<<<<<< Upstream, based on origin/master
			//action after a field is clicked
//			siteButton.Clicked += async (object sender, EventArgs e) => 
//			{
//				await this.Navigation.PushAsync(new OverlayContentPage());
//			};
=======
			BindingContext = new MapViewModel ();

			this.Content = CreateMapContentView ();

		}

		View CreateMapContentView ()
		{
			//Coordinates for the starting point of the map

			MapViewModel model = (MapViewModel)BindingContext;
			Position location = model.getCurrentDevicePosition ();

			var _map = new ExtendedMap.Forms.Plugin.Abstractions.ExtendedMap (MapSpan.FromCenterAndRadius (location, Distance.FromKilometers (1))) { IsShowingUser = true };

			_map.MapType = MapType.Hybrid;

			_map.BindingContext = BindingContext;

			var createMapContentView = new CustomMapContentView (_map);

			return createMapContentView;
>>>>>>> ed096cc Added ExtendedMap Plugin to replace previous map This closes #5  #324 time:4 comment:Added ExtendedMap Plugin
		}
	}
}

