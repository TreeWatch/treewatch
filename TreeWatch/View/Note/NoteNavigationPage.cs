using System;

using Xamarin.Forms;

namespace TreeWatch
{
	public class NoteNavigationPage : NavigationPage
	{
		public NoteNavigationPage (Page root) : base (root)
		{
			Title = root.Title;
			Icon = root.Icon;
		}
		
	}
}


