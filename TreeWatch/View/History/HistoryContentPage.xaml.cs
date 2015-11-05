using Xamarin.Forms;

namespace TreeWatch
{
	public partial class HistoryContentPage : ContentPage
	{
		public HistoryContentPage ()
		{
			InitializeComponent ();

			//site configurations
			Title = "History";
			if (Device.OS == TargetPlatform.iOS)
			{
				Icon = "HistoryTabBarIcon.png";
			}
		}
	}
}

