using Xamarin.Forms;

namespace TreeWatch
{
	public class CustomTabbedPage : TabbedPage
	{
		public CustomTabbedPage ()
		{
			// map tab page
			Children.Add (new MapMasterDetailPage ());

			// note tab page
			Children.Add (new NoteNavigationPage (new NoteContentPage ()));

			// todo tab page
			Children.Add (new ToDoNavigationPage (new ToDoContentPage ()));

			// history tab page
			Children.Add (new HistoryNavigationPage (new HistoryContentPage ()));

			// settings tab page
			Children.Add (new SettingsNavigationPage (new SettingsContentPage ()));
		}
	}
}


