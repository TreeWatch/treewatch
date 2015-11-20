using Xamarin.Forms;

namespace TreeWatch
{
	public class MapMasterDetailPage : MasterDetailPage
	{
		public MapMasterDetailPage ()
		{
			StyleId = "MapMasterDetailPage";
			var mapViewModel = new MapViewModel ();

			// Create the master page with the ListView.
			var mapMenuContentPage = new MapMenuContentPage (mapViewModel);
			mapMenuContentPage.FieldSelected += (sender, e) => {
				IsPresented = false;
			};
			Master = mapMenuContentPage;

			// Create the detail page and wrap it in a navigation page to provide a NavigationBar and Toggle button
			Detail = new MapNavigationPage (new MapContentPage (mapViewModel));

			MasterBehavior = MasterBehavior.Popover;
			Title = Detail.Title;

			if (Detail.Icon != null) {
				Icon = Detail.Icon;
			}
		}
	}
}


