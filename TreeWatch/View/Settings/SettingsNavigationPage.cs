using Xamarin.Forms;

namespace TreeWatch
{
	public class SettingsNavigationPage : NavigationPage
	{
		public SettingsNavigationPage (Page root) : base (root)
		{
			Title = root.Title;
			if (root.Icon != null)
			{
				Icon = root.Icon;
			}
		}
	}
}


