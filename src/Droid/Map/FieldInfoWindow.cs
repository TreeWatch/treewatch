// <copyright file="FieldInfoWindow.cs" company="TreeWatch">
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
namespace TreeWatch.Droid
{
    using Android.App;
    using Android.Content;
    using Android.Gms.Maps.Model;
    using Android.Views;
    using Android.Widget;

    /// <summary>
    /// Field info window.
    /// </summary>
    public class FieldInfoWindow : Java.Lang.Object, Android.Gms.Maps.GoogleMap.IInfoWindowAdapter
    {
        /// <summary>
        /// Gets the info contents.
        /// </summary>
        /// <returns>The info contents.</returns>
        /// <param name="marker">Marker object.</param>
        public View GetInfoContents(Marker marker)
        {
            var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;

            View v = inflater.Inflate(Resource.Layout.field_info_window, null);

            var title = v.FindViewById(Resource.Id.textViewName) as TextView;
            title.Text = marker.Title;

            var description = v.FindViewById(Resource.Id.textViewRows) as TextView;
            description.Text = marker.Snippet;

            return v;
        }

        /// <summary>
        /// Gets the info window.
        /// </summary>
        /// <returns>The info window.</returns>
        /// <param name="marker">Marker object.</param>
        public View GetInfoWindow(Marker marker)
        {
            return null;
        }
    }
}