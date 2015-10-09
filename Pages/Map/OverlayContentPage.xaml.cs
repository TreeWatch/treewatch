using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TreeWatch
{
	public partial class OverlayContentPage : ContentPage
	{
		public OverlayContentPage ()
		{
			InitializeComponent ();

			//site configurations
			Title = "Overlay";

			//filler
			BackgroundColor = Color.Aqua;
			siteLabel.Text = "Overlay";
		}
	}
}

