using Xamarin.Forms;

namespace TreeWatch
{
    public partial class BlockInformationContentPage : ContentPage
    {
        public BlockInformationContentPage(InformationViewModel informationViewModel)
        {
            // initialize component
            InitializeComponent();
            // set view model
            BindingContext = informationViewModel;
        }
    }
}