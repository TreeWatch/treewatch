using Xamarin.Forms;

namespace TreeWatch
{
    public class MapNavigationPage : NavigationPage
    {
        public MapNavigationPage(Page root)
            : base(root)
        {
            // set style id for testing
            StyleId = "MapNavigationPage";

            // set default values
            Title = "Map";
            if (root.Icon != null)
            {
                Icon = root.Icon;
            }
        }
    }
}