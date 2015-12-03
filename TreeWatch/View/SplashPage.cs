using System;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace TreeWatch
{
	public class SplashPage : ContentPage
	{
		public SplashPage ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Initializing" },
					new ActivityIndicator { IsRunning = true}
				},
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand

			};

		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			init ();
		}
		public void init(){
			if (!App.Database.DBconfig.init) {
				MockDataProvider.SetUpMockData();
				App.Database.DBconfig = new DatabaseConfig (true);
			}

			Application.Current.MainPage = new NavigationPage(new CustomTabbedPage());
		}
	}
}


