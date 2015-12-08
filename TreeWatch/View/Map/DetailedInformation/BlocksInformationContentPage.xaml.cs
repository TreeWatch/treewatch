using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TreeWatch
{
    public partial class BlocksInformationContentPage : ContentPage
    {
        public BlocksInformationContentPage(InformationViewModel informationViewModel)
        {
            // initialize component
            InitializeComponent();
            // set view model
            BindingContext = informationViewModel;

            NavigationPage.SetBackButtonTitle(this, "Blocks");
          
            blockView.ItemTapped += (sender, e) => (BindingContext as InformationViewModel).NavigateToBlock();
        }
    }
}

