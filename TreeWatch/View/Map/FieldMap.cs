// <copyright file="FieldMap.cs" company="TreeWatch">
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
    using System.Collections.ObjectModel;

    using Xamarin.Forms;
    using Xamarin.Forms.Maps;

    /// <summary>
    /// Field map.
    /// </summary>
    public class FieldMap : Map
    {
        /// <summary>
        /// The color of the overlay.
        /// </summary>
        private readonly Color overlayColor;

        /// <summary>
        /// The color of the boundary.
        /// </summary>
        private readonly Color boundaryColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.FieldMap"/> class.
        /// </summary>
        /// <param name="region">The actual position inside the map.</param>
        public FieldMap(MapSpan region)
            : base(region)
        {
            this.Fields = new ObservableCollection<Field>();
            this.overlayColor = Color.Transparent;
            this.boundaryColor = Color.FromHex("#ff8400");

            this.StyleId = "FieldMapView";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.FieldMap"/> class.
        /// </summary>
        public FieldMap()
            : this(MapSpan.FromCenterAndRadius(new Position(), Distance.FromKilometers(1)))
        {
        }

        /// <summary>
        /// Gets the color of the over lay.
        /// </summary>
        /// <value>The color of the over lay.</value>
        public Color OverLayColor
        {
            get
            { 
                return this.overlayColor;
            }
        }

        /// <summary>
        /// Gets the color of the boundary.
        /// </summary>
        /// <value>The color of the boundary.</value>
        public Color BoundaryColor
        {
            get
            { 
                return this.boundaryColor;
            }
        }

        /// <summary>
        /// Gets or sets the fields.
        /// </summary>
        /// <value>The fields.</value>
        public ObservableCollection<Field> Fields
        {
            get;
            set;
        }
    }
}