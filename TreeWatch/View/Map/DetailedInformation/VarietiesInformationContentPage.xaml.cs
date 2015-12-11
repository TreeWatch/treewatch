using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TreeWatch
{
    public partial class VarietiesInformationContentPage : ContentPage
    {
        public VarietiesInformationContentPage(InformationViewModel informationViewModel)
        {
            // initialize component
            InitializeComponent();
            // set view model
            BindingContext = informationViewModel;

            NavigationPage.SetBackButtonTitle(this, "Varieties");
          
            varietiesView.ItemTapped += (sender, e) => (BindingContext as InformationViewModel).NavigateToBlocks();
        }
    }
}

