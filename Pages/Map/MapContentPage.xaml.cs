using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TreeWatch
{
	public partial class MapContentPage : ContentPage
	{
		public MapContentPage ()
		{
			InitializeComponent ();

			//site configurations
			Title = "Map";
			NavigationPage.SetBackButtonTitle (this, "Back");

			//filler
			BackgroundColor = Color.Aqua;
			siteLabel.Text = "Map";
			siteButton.Text = "Show Overlay";
			siteButton.Clicked += async (object sender, EventArgs e) => 
			{
				await this.Navigation.PushAsync(new OverlayContentPage());
			};
		}
	}
}

