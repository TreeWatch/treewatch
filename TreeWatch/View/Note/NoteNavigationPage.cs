using Xamarin.Forms;

namespace TreeWatch
{
	public class NoteNavigationPage : NavigationPage
	{
		public NoteNavigationPage (Page root) : base (root)
		{
			// configure navigation page
			Title = root.Title;
			if (root.Icon != null)
			{
				Icon = root.Icon;
			}
		}
		
	}
}


