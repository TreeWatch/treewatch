using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TreeWatch
{
	public partial class MapTypeContentPage : ContentPage
	{
		public MapTypeContentPage (SettingsViewModel settingsViewModel)
		{
			// initialize component
			InitializeComponent ();

			// set view model
			this.BindingContext = settingsViewModel;

			mapTypes.ItemTapped += (sender, e) => (this.BindingContext as SettingsViewModel).NavigateToSettings (e.Item);
		}

		//		SetMapType;
		//	}
		//
		//	protected virtual void SetMapType (object sender, ItemTappedEventArgs e)
		//	{
		//		(this.BindingContext as SettingsViewModel).NavigateToSettings (e.Item);
	}
}

