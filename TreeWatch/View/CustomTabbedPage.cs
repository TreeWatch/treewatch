using System;

using Xamarin.Forms;

namespace TreeWatch
{
	public class CustomTabbedPage : TabbedPage
	{
		public CustomTabbedPage ()
		{
			// map tab page
			this.Children.Add (new MapMasterDetailPage ());

			// note tab page
			this.Children.Add (new NoteNavigationPage (new NoteContentPage ()));

			// todo tab page
			this.Children.Add (new ToDoNavigationPage (new ToDoContentPage ()));

			// history tab page
			this.Children.Add (new HistoryNavigationPage (new HistoryContentPage ()));

			// settings tab page
			this.Children.Add (new SettingsNavigationPage (new SettingsContentPage ()));
		}
	}
}


