using Xamarin.Forms;

namespace TreeWatch
{
	public class MapMasterDetailPage : MasterDetailPage
	{
		public MapMasterDetailPage ()
		{
			// Create the master page with the ListView.
			Master = new MapMenuContentPage ();

			// Create the detail page and wrap it in a navigation page to provide a NavigationBar and Toggle button
			Detail = new MapNavigationPage (new MapContentPage ());

			Title = Detail.Title;
			if (Detail.Icon != null)
			{
				Icon = Detail.Icon;
			}

		}
	}
}


