using Xamarin.Forms;

namespace TreeWatch
{
    public class MapNavigationPage : NavigationPage
    {
        public MapNavigationPage(Page root)
            : base(root)
        {
            // set style id for testing
            this.StyleId = "MapNavigationPage";

            // set default values
            this.Title = "Map";
            if (root.Icon != null)
            {
                Icon = root.Icon;
            }
        }
    }
}