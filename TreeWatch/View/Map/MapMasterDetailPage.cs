using System;

using Xamarin.Forms;

namespace TreeWatch
{
	public class MapMasterDetailPage : MasterDetailPage
	{
		public MapMasterDetailPage ()
		{
			// Create the master page with the ListView.
			this.Master = new MapMenuContentPage ();

			// Create the detail page and wrap it in a navigation page to provide a NavigationBar and Toggle button
			this.Detail = new MapNavigationPage (new MapContentPage ());

			Title = Detail.Title;
			//Icon = Detail.Icon;
		}
	}
}


