﻿using System;

using Xamarin.Forms;

namespace TreeWatch
{
	public class SettingsNavigationPage : NavigationPage
	{
		public SettingsNavigationPage (Page root) : base (root)
		{
			Title = "Settings";
			Icon = "Icons/Settings/SettingsTabBarIcon.png";
		}
	}
}

