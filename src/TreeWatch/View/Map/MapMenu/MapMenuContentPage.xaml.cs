// <copyright file="MapMenuContentPage.xaml.cs" company="TreeWatch">
// Copyright © 2015 TreeWatch
// </copyright>
#region Copyright
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
#endregion
namespace TreeWatch
{
    using System;
    using Xamarin.Forms;

    /// <summary>
    /// Map menu content page.
    /// </summary>
    public partial class MapMenuContentPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.MapMenuContentPage"/> class.
        /// </summary>
        /// <param name="mapViewModel">Map view model.</param>
        public MapMenuContentPage(MapViewModel mapViewModel)
        {
            this.InitializeComponent();

            this.BindingContext = mapViewModel;

            fieldView.ItemTapped += this.OnFieldSelected;
        }

        /// <summary>
        /// Occurs when field selected.
        /// </summary>
        public event EventHandler FieldSelected;

        /// <summary>
        /// Infos the button clicked.
        /// </summary>
        /// <param name="sender">The sender of clicked event.</param>
        /// <param name="e">Event arguments.</param>
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

        /// <summary>
        /// Raises the field selected event.
        /// </summary>
        /// <param name="sender">The sender of tapped event.</param>
        /// <param name="e">Item tapped event arguments.</param>
        protected virtual void OnFieldSelected(object sender, ItemTappedEventArgs e)
        {
            if (this.FieldSelected != null)
            {
                this.FieldSelected(this, EventArgs.Empty);
                FieldHelper.Instance.FieldSelectedEvent(e.Item as Field);
            }
        }
    }
}