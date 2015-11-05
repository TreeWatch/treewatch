using Xamarin.Forms;

namespace TreeWatch
{
	public class NoteNavigationPage : NavigationPage
	{
		public NoteNavigationPage (Page root) : base (root)
		{
			Title = root.Title;
			if (root.Icon != null)
			{
				Icon = root.Icon;
			}
		}
		
	}
}


