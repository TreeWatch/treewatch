using System;
using Xamarin.Forms;

namespace TreeWatch
{
	public partial class MapMenuContentPage : ContentPage
	{
		public event EventHandler FieldSelected;

		public MapMenuContentPage (MapViewModel mapViewModel)
		{
			InitializeComponent ();
			this.BindingContext = mapViewModel;
			ListView listView = this.FindByName<ListView> ("FieldView");
			listView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => OnFieldSelected ();
		}

		protected virtual void OnFieldSelected ()
		{
			if (FieldSelected != null) {
				FieldSelected (this, EventArgs.Empty);
			}
		}
	}
}
