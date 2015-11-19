using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TreeWatch
{
	public partial class BlockInformationContentPage : ContentPage
	{
		public BlockInformationContentPage (FieldInformationViewModel fieldInformationViewModel)
		{
			// initialize component
			InitializeComponent ();

			// set view model
			this.BindingContext = fieldInformationViewModel;
		}
	}
}

