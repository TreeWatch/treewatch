using Xamarin.Forms;

namespace TreeWatch
{
    public class CustomTabbedPage : TabbedPage
    {
        public CustomTabbedPage()
        {
            StyleId = "TabbedPageView";

            // map tab page
            var mapMasteDetailPage = new MapMasterDetailPage();
            Children.Add(mapMasteDetailPage);

            // note tab page
            Children.Add(new NoteContentPage());

            // todo tab page
            Children.Add(new ToDoContentPage());

            // history tab page
            Children.Add(new HistoryContentPage());

            // settings tab page
            Children.Add(new SettingsContentPage(mapMasteDetailPage.Detail));

            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}