using System;

using Xamarin.Forms;
using System.Diagnostics;

namespace TreeWatch
{
	public partial class MapMenuContentPage : ContentPage
	{
		public event EventHandler FieldSelected;

		public MapMenuContentPage ()
		{
			InitializeComponent ();

			ListView listView = this.FindByName<ListView> ("FieldView");
			listView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => OnFieldSelected ();
		}

		protected virtual void OnFieldSelected ()
		{
			if (FieldSelected != null)
			{
				FieldSelected (this, EventArgs.Empty);
			}
		}
	}
}
