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

            var toolBarItem = new ToolbarItem();
            toolBarItem.Text = "Filter";
            toolBarItem.Icon = Device.OS == TargetPlatform.iOS ? "Icons/FilterListIcon" : "FilterListIcon";
            ToolbarItems.Add(toolBarItem);

            blockView.ItemTapped += (sender, e) => ((InformationViewModel)this.BindingContext).NavigateToBlock();
        }
    }
}

