using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TreeWatch
{
	public partial class DetailedInformationContentPage : ContentPage
	{
		public DetailedInformationContentPage (MapViewModel mapViewModel)
		{
			// initialize component
			InitializeComponent ();

			// set view model
			this.BindingContext = mapViewModel;
		}
	}
}

