// <copyright file="SettingsViewModel.cs" company="TreeWatch">
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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Xamarin.Forms;
    using Xamarin.Forms.Maps;

    /// <summary>
    /// Settings view model.
    /// </summary>
    public class SettingsViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.SettingsViewModel"/> class.
        /// </summary>
        /// <param name="mapContentPage">Map content page.</param>
        public SettingsViewModel(MapContentPage mapContentPage)
        {
            FieldMap = mapContentPage.Content as FieldMap;
            this.MapTypes = new List<MType>();

            foreach (var name in Enum.GetValues(typeof(MapType)))
            {
                this.MapTypes.Add(new MType(name.ToString()));
            }
        }

        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the field map.
        /// </summary>
        /// <value>The field map.</value>
        public FieldMap FieldMap { get; }

        /// <summary>
        /// Gets the map types.
        /// </summary>
        /// <value>The map types.</value>
        public List<MType> MapTypes { get; }

        /// <summary>
        /// Navigates to settings.
        /// </summary>
        /// <param name="mapType">M type.</param>
        public void NavigateToSettings(object mapType)
        {
            switch (((MType)mapType).Name)
            {
                case "Satellite":
                    FieldMap.MapType = MapType.Satellite;
                    break;
                case "Street":
                    FieldMap.MapType = MapType.Street;
                    break;
                default:
                    FieldMap.MapType = MapType.Hybrid;
                    break;
            }

            var navigationPage = (NavigationPage)Application.Current.MainPage;

            navigationPage.PopToRootAsync();
        }

        /// <summary>
        /// Navigates the type of the to map.
        /// </summary>
        public void NavigateToMapType()
        {
            var navigationPage = (NavigationPage)Application.Current.MainPage;

            navigationPage.PushAsync(new MapTypeContentPage(this));
        }

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// M type.
        /// </summary>
        public struct MType
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="MType"/> struct.
            /// </summary>
            /// <param name="name">Name of the map type.</param>
            public MType(string name)
            {
                this.Name = name;
            }

            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>The name.</value>
            public string Name { get; set; }
        }
    }
}