using System;
using System.Collections.Generic;

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

			//filler
			BackgroundColor = Color.Aqua;
			siteLabel.Text = "History";
		}
	}
}

