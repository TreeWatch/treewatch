using Xamarin.Forms;

namespace TreeWatch
{
	public partial class FieldInformationContentPage : ContentPage
	{
		public FieldInformationContentPage (FieldInformationViewModel fieldInformationViewModel)
		{
			// initialize component
			InitializeComponent ();

			// set view model
			this.BindingContext = fieldInformationViewModel;
		}
	}
}

