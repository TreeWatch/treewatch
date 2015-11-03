﻿using System;

using Xamarin.Forms;

namespace TreeWatch
{
	public class MapNavigationPage : NavigationPage
	{
		public MapNavigationPage (Page root) : base (root)
		{
			Title = root.Title;

			if (Device.OS == TargetPlatform.iOS) {
				Icon = root.Icon;
			}
		}
		
	}
}


