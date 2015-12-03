using System;
using Xamarin.Forms;
using System.Diagnostics;

namespace TreeWatch
{
	public partial class MapMenuContentPage : ContentPage
	{
		public event EventHandler FieldSelected;

		public MapMenuContentPage (MapViewModel mapViewModel)
		{
			InitializeComponent ();

			this.BindingContext = mapViewModel;

            fieldView.ItemTapped += OnFieldSelected;
		}

		protected virtual void OnFieldSelected (object sender, ItemTappedEventArgs e)
		{
			if (FieldSelected != null) {
				FieldSelected (this, EventArgs.Empty);
				FieldHelper.Instance.FieldSelectedEvent ((Field)e.Item);
			}
		}

		public void InfoButtonClicked (object sender, EventArgs e)
		{
			foreach (Field field in fieldView.ItemsSource) {
				if (field.Name.Equals (((Button)sender).CommandParameter)) {
					((MapViewModel)this.BindingContext).NavigateToField (field);
				}
			}
		}
	}
}
