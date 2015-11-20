using Xamarin.Forms;
using System.Diagnostics;

namespace TreeWatch
{
	public partial class FieldInformationContentPage : ContentPage
	{
		public FieldInformationContentPage (InformationViewModel fieldInformationViewModel)
		{
			// initialize component
			InitializeComponent ();

			// set view model
			this.BindingContext = fieldInformationViewModel;
		}
	}
}

