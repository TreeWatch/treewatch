using System;
using System.Linq;

using Xamarin.Forms;

namespace TreeWatch
{
	public partial class MapMenuContentPage : ContentPage
	{
		public event EventHandler FieldSelected;

		public MapMenuContentPage ()
		{
			InitializeComponent ();
			ListView lv = this.FindByName<ListView> ("FieldView");
			lv.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => 
			{
				OnFieldSelected();
			};
		}

		protected virtual void OnFieldSelected(){
			if (FieldSelected != null) {
				FieldSelected (this, EventArgs.Empty);
			}
		}
	}
}
