using Xamarin.Forms;

namespace TreeWatch
{
    public partial class FieldInformationContentPage : ContentPage
    {
        public FieldInformationContentPage(InformationViewModel informationViewModel)
        {
            // initialize component
            InitializeComponent();

            // set view model
            BindingContext = informationViewModel;

            NavigationPage.SetBackButtonTitle(this, informationViewModel.Field.Name);

            blocks.Tapped += (sender, e) => (BindingContext as InformationViewModel).NavigateToBlocks();
        }
    }
}

