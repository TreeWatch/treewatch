using Xamarin.Forms;

namespace TreeWatch
{
	public class CustomTabbedPage : TabbedPage
	{
		public CustomTabbedPage ()
		{
			// map tab page
			var mapMasteDetailPage = new MapMasterDetailPage ();
			Children.Add (mapMasteDetailPage);

			// note tab page
			this.Children.Add (new NoteContentPage ());

			// todo tab page
			this.Children.Add (new ToDoContentPage ());

			// history tab page
			this.Children.Add (new HistoryContentPage ());

			// settings tab page
			this.Children.Add (new SettingsContentPage (mapMasteDetailPage.Detail));

			NavigationPage.SetHasNavigationBar (this, false);
		}
	}
}


