// <copyright file="CustomNavigationRenderer.cs" company="TreeWatch">
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
using TreeWatch.Droid;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationRenderer))]

namespace TreeWatch.Droid
{
    using Android.App;
    using Android.Graphics.Drawables;
    using Xamarin.Forms.Platform.Android;

    /// <summary>
    /// Custom navigation renderer.
    /// </summary>
    public class CustomNavigationRenderer : NavigationRenderer
    {
        /// <summary>
        /// Raises the element changed event.
        /// </summary>
        /// <param name="e">E event.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {  
            base.OnElementChanged(e);  

            this.RemoveAppIconFromActionBar();  
        }

        /// <summary>
        /// Removes the app icon from action bar.
        /// </summary>
        private void RemoveAppIconFromActionBar()
        {  
            var actionBar = ((Activity)Context).ActionBar;  
            actionBar.SetIcon(new ColorDrawable(Color.Transparent.ToAndroid()));  
        }
    }
}  