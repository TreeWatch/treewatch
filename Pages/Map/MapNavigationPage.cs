using System;
using Mono;
using Xamarin.Forms;

namespace TreeWatch
{
	public class MapNavigationPage : NavigationPage
	{
		public MapNavigationPage (Page root) : base (root)
		{
			Title = "Map";
			Icon = "Icons/Map/MapTabBarIcon.png";
		}
		
	}
}


