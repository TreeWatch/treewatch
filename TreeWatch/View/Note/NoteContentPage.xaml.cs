using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TreeWatch
{
	public partial class NoteContentPage : ContentPage
	{
		public NoteContentPage ()
		{
			InitializeComponent ();

			//site configurations
			Title = "Note";
			if (Device.OS == TargetPlatform.iOS) {
				Icon = "NoteTabBarIcon.png";
			}
		}
	}
}

