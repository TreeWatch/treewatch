// <copyright file="SettingsContentPage.xaml.cs" company="TreeWatch">
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
    using Xamarin.Forms;

    /// <summary>
    /// Settings content page inherits ContentPage
    /// </summary>
    public partial class SettingsContentPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.SettingsContentPage"/> class.
        /// </summary>
        /// <param name="mapPage">Map page.</param>
        public SettingsContentPage(Page mapPage)
        {
            this.InitializeComponent();

            // set view model
            if (Device.OS == TargetPlatform.iOS)
            {
                this.BindingContext = new SettingsViewModel((mapPage as MapNavigationPage).CurrentPage as MapContentPage);
            }
            else if (Device.OS == TargetPlatform.Android)
            {
                this.BindingContext = new SettingsViewModel(mapPage as MapContentPage);
            }

            mapType.Tapped += (sender, e) => (BindingContext as SettingsViewModel).NavigateToMapType();
        }
    }
}