﻿using Xamarin.Forms;

namespace TreeWatch
{
	public class CustomTabbedPage : TabbedPage
	{
		public CustomTabbedPage ()
		{
			// map tab page
			Children.Add (new MapMasterDetailPage ());

			// note tab page
			this.Children.Add (new NoteContentPage ());

			// todo tab page
			this.Children.Add (new ToDoContentPage ());

			// history tab page
			this.Children.Add (new HistoryContentPage ());

			// settings tab page
			this.Children.Add (new SettingsContentPage ());

			NavigationPage.SetBackButtonTitle (this, "Home");
			NavigationPage.SetHasNavigationBar (this, false);
		}
	}
}


