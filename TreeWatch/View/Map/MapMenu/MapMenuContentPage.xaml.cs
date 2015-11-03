using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TreeWatch
{
	public partial class MapMenuContentPage : ContentPage
	{
		private Fields fields = new Fields ();

		public MapMenuContentPage ()
		{
			InitializeComponent ();

			Title = "Menu";
			Icon = "HamburgerMenuIcon.png";

			FieldView.ItemsSource = fields;
		}

		private void SearchBarTextChanged (object sender, EventArgs args)
		{
			FilterFields (searchBar.Text);
		}

		private void SearchBarSearchButtonPressed (object sender, EventArgs args)
		{
			FilterFields (searchBar.Text);
		}

		private void FilterFields (String searchBarText)
		{
			FieldView.BeginRefresh ();

			if (string.IsNullOrWhiteSpace (searchBarText)) {
				FieldView.ItemsSource = fields;
			} else {
				FieldView.ItemsSource = fields.Where (x => x.Name.ToLower ().Contains (searchBarText.ToLower ()));
			}

			FieldView.EndRefresh ();
		}

		private void FieldSelected (object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
				return;
			var selected = (Field)e.SelectedItem;
		}
	}
}

