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

			//filler
			BackgroundColor = Color.Aqua;
			siteLabel.Text = "ToDo";
		}
	}
}

