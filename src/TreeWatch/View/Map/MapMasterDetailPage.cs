// <copyright file="MapMasterDetailPage.cs" company="TreeWatch">
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
    /// Map master detail page.
    /// </summary>
    public class MapMasterDetailPage : MasterDetailPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.MapMasterDetailPage"/> class.
        /// </summary>
        public MapMasterDetailPage()
        {
            // set testing style id
            this.StyleId = "MapMasterDetailPage";

            // set view model for all pages
            var mapViewModel = new MapViewModel();

            // Create the master page with the ListView.
            var mapMenuContentPage = new MapMenuContentPage(mapViewModel);
            mapMenuContentPage.FieldSelected += (sender, e) =>
            {
                IsPresented = false;
            };
            this.Master = mapMenuContentPage;

            this.Detail = TargetPlatform.iOS == Device.OS ? new MapNavigationPage(new MapContentPage(mapViewModel)) as Page : new MapContentPage(mapViewModel);

            // configuration of this page
            this.MasterBehavior = MasterBehavior.Popover;
            this.Title = this.Detail.Title;
            if (Detail.Icon != null)
            {
                this.Icon = this.Detail.Icon;
            }
        }
    }
}