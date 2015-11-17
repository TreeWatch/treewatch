using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TreeWatch
{
	public partial class DetailedInformationContentPage : ContentPage
	{
		public DetailedInformationContentPage (DetailInformationViewModel detailInformationViewModel)
		{
			// initialize component
			InitializeComponent ();

			// set view model
			this.BindingContext = detailInformationViewModel;
		}
	}
}

