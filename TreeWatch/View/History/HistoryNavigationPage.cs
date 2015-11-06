using Xamarin.Forms;

namespace TreeWatch
{
	public class HistoryNavigationPage : NavigationPage
	{
		public HistoryNavigationPage (Page root) : base (root)
		{
			Title = root.Title;
<<<<<<< HEAD
			if (root.Icon != null)
			{
				Icon = root.Icon;
			}
=======
			Icon = root.Icon;
>>>>>>> 5ac15a78c018a0791722caa5014642cd1adabed3
		}

	}
}
