using Xamarin.Forms;

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
				IsPresented = false;
			};
			this.Master = mapMenuContentPage;

			if (TargetPlatform.iOS == Device.OS)
			{
				this.Detail = new MapNavigationPage (new MapContentPage (mapViewModel));
			} else
			{
				this.Detail = new MapContentPage (mapViewModel);
			}

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


