using System;

using Xamarin.Forms;

namespace TreeWatch
{
	public class CustomTabbedPage : TabbedPage
	{
		public CustomTabbedPage ()
		{
			// map tab page
//			this.Children.Add (new MapPage () { Title = "Map", Icon = "Map.png" });
			this.Children.Add (new MapNavigationPage (new MapContentPage ()));

			// note tab page
//			this.Children.Add (new NotePage () { Title = "Note", Icon = "Note.png" });
			this.Children.Add (new NoteNavigationPage (new NoteContentPage ()));

			// todo tab page
//			this.Children.Add (new ToDoPage () { Title = "ToDo", Icon = "ToDo.png" });
			this.Children.Add (new ToDoNavigationPage (new ToDoContentPage ()));

			// history tab page
//			this.Children.Add (new HistoryPage () { Title = "History", Icon = "History.png" });
			this.Children.Add (new HistoryNavigationPage (new HistoryContentPage ()));

			// settings tab page
//			this.Children.Add (new SettingsPage () { Title = "Settings", Icon = "Settings.png" });
			this.Children.Add (new SettingsNavigationPage (new SettingsContentPage ()));
		}
	}
}


