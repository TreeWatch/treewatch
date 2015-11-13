using Xamarin.Forms;
using System;

namespace TreeWatch
{
	public class MapMasterDetailPage : MasterDetailPage
	{
		public MapMasterDetailPage ()
		{
			this.StyleId = "MapMasterDetailPage";
			MapViewModel mapViewModel = new MapViewModel ();

			// Create the master page with the ListView.
			var mapMenuContentPage = new MapMenuContentPage (mapViewModel);
			mapMenuContentPage.FieldSelected += (sender, e) => {
				this.IsPresented = false;
			};
			this.Master = mapMenuContentPage;
			// Create the detail page and wrap it in a navigation page to provide a NavigationBar and Toggle button
			Detail = new MapNavigationPage (new MapContentPage (mapViewModel));

			this.MasterBehavior = MasterBehavior.Popover;
			Title = Detail.Title;

			if (Detail.Icon != null)
			{
				Icon = Detail.Icon;
			}

		}
	}
}


