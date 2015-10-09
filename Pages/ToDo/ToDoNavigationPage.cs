using System;

using Xamarin.Forms;

namespace TreeWatch
{
	public class ToDoNavigationPage : NavigationPage
	{
		public ToDoNavigationPage (Page root) : base (root)
		{
			Title = "ToDo";
			Icon = "Icons/ToDo/ToDoTabBarIcon.png";
		}
	}
}


