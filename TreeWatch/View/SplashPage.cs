using Xamarin.Forms;

namespace TreeWatch
{
    public class SplashPage : ContentPage
    {
        public SplashPage()
        {
            Content = new StackLayout
            { 
                Children =
                {
                    new Label { Text = "Initializing" },
                    new ActivityIndicator { IsRunning = true }
                },
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand

            };

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Init();
        }

        public static void Init()
        {
            if (!App.Database.DBconfig.Init)
            {
                MockDataProvider.SetUpMockData();
                App.Database.DBconfig = new DatabaseConfig(true);
            }

            Application.Current.MainPage = new NavigationPage(new CustomTabbedPage());
        }
    }
}


