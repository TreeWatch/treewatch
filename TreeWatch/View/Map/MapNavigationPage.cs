using Xamarin.Forms;

namespace TreeWatch
{
	public class MapNavigationPage : NavigationPage
	{
		public MapNavigationPage (Page root) : base (root)
		{
			// set style id for testing
			this.StyleId = "MapNavigationPage";

			// set default values
			Title = root.Title;
			if (root.Icon != null)
			{
				Icon = root.Icon;
			}
		}
		
	}
}


