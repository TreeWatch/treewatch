using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TreeWatch
{
	public partial class ToDoContentPage : ContentPage
	{
		public ToDoContentPage ()
		{
			InitializeComponent ();

			//site configurations
			Title = "ToDo";
			if (Device.OS == TargetPlatform.iOS) {
				Icon = "ToDoTabBarIcon.png";
			}
		}
	}
}

