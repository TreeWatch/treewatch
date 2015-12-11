using Xamarin.Forms;

namespace TreeWatch
{
    public class MapMasterDetailPage : MasterDetailPage
    {
        public MapMasterDetailPage()
        {
            // set testing style id
            StyleId = "MapMasterDetailPage";

            // set view model for all pages
            var mapViewModel = new MapViewModel();

            // Create the master page with the ListView.
            var mapMenuContentPage = new MapMenuContentPage(mapViewModel);
            mapMenuContentPage.FieldSelected += (sender, e) =>
            {
                IsPresented = false;
            };
            Master = mapMenuContentPage;

            Detail = TargetPlatform.iOS == Device.OS ? new MapNavigationPage(new MapContentPage(mapViewModel)) as Page : new MapContentPage(mapViewModel);

            // configuration of this page
            MasterBehavior = MasterBehavior.Popover;
            Title = Detail.Title;
            if (Detail.Icon != null)
            {
                Icon = Detail.Icon;
            }

        }
    }
}


