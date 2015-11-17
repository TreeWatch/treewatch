using Xamarin.Forms;

namespace TreeWatch
{
	public class MapNavigationPage : NavigationPage
	{
		public MapNavigationPage (Page root) : base (root)
		{
			Title = root.Title;
			StyleId = "MapNavigationPage";
			if (root.Icon != null) {
				Icon = root.Icon;
			}
		}
		
	}
}


