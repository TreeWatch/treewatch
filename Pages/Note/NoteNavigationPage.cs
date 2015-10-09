using System;

using Xamarin.Forms;

namespace TreeWatch
{
	public class NoteNavigationPage : NavigationPage
	{
		public NoteNavigationPage (Page root) : base (root)
		{
			Title = "Note";
			Icon = "Icons/Note/NoteTabBarIcon.png";
		}
		
	}
}


