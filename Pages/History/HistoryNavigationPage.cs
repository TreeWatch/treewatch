﻿using System;

using Xamarin.Forms;

namespace TreeWatch
{
	public class HistoryNavigationPage : NavigationPage
	{
		public HistoryNavigationPage (Page root) : base (root)
		{
			Title = "History";
			Icon = "Icons/History/HistoryTabBarIcon.png";
		}
		
	}
}


