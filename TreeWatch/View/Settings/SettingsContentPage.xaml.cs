using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TreeWatch
{
	public partial class SettingsContentPage : ContentPage
	{
		public SettingsContentPage ()
		{
			InitializeComponent ();

			//site configurations
			Title = "Settings";
			if (Device.OS == TargetPlatform.iOS) {
				Icon = "SettingsTabBarIcon.png";
			}
		}
	}
}

