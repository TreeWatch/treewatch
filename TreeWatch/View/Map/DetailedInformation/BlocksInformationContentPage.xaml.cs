using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TreeWatch
{
    public partial class BlocksInformationContentPage : ContentPage
    {
        public BlocksInformationContentPage(InformationViewModel fieldInformationViewModel)
        {
            // initialize component
            InitializeComponent();
            // set view model
            this.BindingContext = fieldInformationViewModel;

            blockView.ItemTapped += (sender, e) => ((InformationViewModel)this.BindingContext).NavigateToBlock();
        }
    }
}

