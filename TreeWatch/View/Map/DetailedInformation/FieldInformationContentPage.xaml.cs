using Xamarin.Forms;
using System;
using System.Diagnostics;

namespace TreeWatch
{
	public partial class FieldInformationContentPage : ContentPage
	{
		public FieldInformationContentPage (InformationViewModel informationViewModel)
		{
			// initialize component
			InitializeComponent ();

			// set view model
			this.BindingContext = informationViewModel;

			NavigationPage.SetBackButtonTitle (this, informationViewModel.FieldName);

			blocks.Tapped += (sender, e) => ((InformationViewModel)this.BindingContext).NavigateToBlocks();
		}
	}
}

