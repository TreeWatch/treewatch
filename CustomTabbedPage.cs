using System;

using Xamarin.Forms;

namespace TreeWatch
{
	public class CustomTabbedPage : TabbedPage
	{
		public CustomTabbedPage ()
		{
			this.Children.Add (new MapPage () { Title = "Map"});//, Icon = "Map.png" });
			this.Children.Add (new NotePage () { Title = "Note"});//, Icon = "Note.png" });
			this.Children.Add (new ToDoPage () { Title = "ToDo"});//, Icon = "ToDo.png" });
			this.Children.Add (new HistoryPage () { Title = "History"});//, Icon = "History.png" });
			this.Children.Add (new SettingsPage () { Title = "Settings"});//, Icon = "Settings.png" });
		}
	}
}


