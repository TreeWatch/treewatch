using System;

using Xamarin.Forms;

namespace TreeWatch
{
    public partial class MapMenuContentPage : ContentPage
    {
        public event EventHandler FieldSelected;

        public MapMenuContentPage(MapViewModel mapViewModel)
        {
            InitializeComponent();

            BindingContext = mapViewModel;

            fieldView.ItemTapped += OnFieldSelected;
        }

        protected virtual void OnFieldSelected(object sender, ItemTappedEventArgs e)
        {
            if (FieldSelected != null)
            {
                FieldSelected(this, EventArgs.Empty);
                FieldHelper.Instance.FieldSelectedEvent(e.Item as Field);
            }
        }

        public void InfoButtonClicked(object sender, EventArgs e)
        {
            foreach (Field field in fieldView.ItemsSource)
            {
                if (field.Name.Equals((sender as Button).CommandParameter))
                {
                    MapViewModel.NavigateToField(field);
                }
            }
        }
    }
}