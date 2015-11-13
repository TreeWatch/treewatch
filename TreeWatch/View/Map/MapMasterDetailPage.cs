using Xamarin.Forms;
using System;

namespace TreeWatch
{
	public class MapMasterDetailPage : MasterDetailPage
	{
		public MapMasterDetailPage ()
		{
			// set testing style id
			this.StyleId = "MapMasterDetailPage";

			// set view model for all pages
			MapViewModel mapViewModel = new MapViewModel ();

			// Create the master page with the ListView.
			var mapMenuContentPage = new MapMenuContentPage (mapViewModel);
			mapMenuContentPage.FieldSelected += (sender, e) => {
				this.IsPresented = false;
			};
			this.Master = mapMenuContentPage;

			// Create the detail page and wrap it in a navigation page to provide a NavigationBar and Toggle button
			var mapNavigationPage = new MapNavigationPage (new MapContentPage (mapViewModel));
			this.Detail = mapNavigationPage;

			// configuration of this page
			this.MasterBehavior = MasterBehavior.Popover;
			this.Title = Detail.Title;
			if (Detail.Icon != null)
			{
				this.Icon = Detail.Icon;
			}

		}
	}
}


