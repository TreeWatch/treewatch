using System;
using System.Diagnostics;

using Xamarin.Forms;

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

			NavigationPage.SetBackButtonTitle (this, informationViewModel.Field.Name);

			blocks.Tapped += (sender, e) => ((InformationViewModel)this.BindingContext).NavigateToBlocks();
		}
	}
}

