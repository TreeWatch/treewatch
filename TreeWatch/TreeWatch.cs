using Xamarin.Forms;

namespace TreeWatch
{
    public class App : Application
    {
        static TreeWatchDatabase database;

        public App()
        {
            // The root page of your application
            MainPage = !App.Database.DBconfig.Init ? new SplashPage() as Page : new NavigationPage(new CustomTabbedPage()); 
        }

        public static TreeWatchDatabase Database
        {
            get
            { 
                if (database == null)
                {
                    database = new TreeWatchDatabase();
                }
                return database; 
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

