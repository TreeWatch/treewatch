using Xamarin.Forms;

namespace TreeWatch
{
	public class HistoryNavigationPage : NavigationPage
	{
		public HistoryNavigationPage (Page root) : base (root)
		{
			Title = root.Title;
			if (root.Icon != null) {
				Icon = root.Icon;
			}
		}
	}
}
