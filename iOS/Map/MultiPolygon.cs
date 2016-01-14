// <copyright file="MultiPolygon.cs" company="TreeWatch">
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
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", MessageId = "Ctl", Scope = "namespace", Target = "Assembly name", Justification = "Auto generated name")]

// Analysis disable once InconsistentNaming
namespace TreeWatch.iOS
{
    using System.Collections.Generic;
    using MapKit;

    /// <summary>
    /// Multipolygon for faster rendering on the map contains multiple polygons.
    /// </summary>
    public class MultiPolygon : MKOverlay
    {
        /// <summary>
        /// The bounding rectangle.
        /// </summary>
        private MKMapRect boundingRect;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.iOS.MultiPolygon"/> class.
        /// </summary>
        /// <param name="polygons">List of Polygons to display.</param>
        public MultiPolygon(List<ColorPolygon> polygons)
        {
            this.Polygons = polygons;

            this.boundingRect.Origin = polygons[0].Polygon.BoundingMapRect.Origin;
            foreach (var item in polygons)
            {
                this.boundingRect = MKMapRect.Union(this.boundingRect, item.Polygon.BoundingMapRect);
            }
        }

        /// <summary>
        /// Gets or sets the polygons.
        /// </summary>
        /// <value>The polygons.</value>
        public List<ColorPolygon> Polygons
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the coordinate.
        /// </summary>
        /// <value>The coordinate.</value>
        public override CoreLocation.CLLocationCoordinate2D Coordinate
        {
            get
            {
                return new CoreLocation.CLLocationCoordinate2D(this.BoundingMapRect.MidX, this.BoundingMapRect.MidY);
            }
        }

        /// <summary>
        /// Gets the bounding map rect.
        /// </summary>
        /// <value>The bounding map rect.</value>
        public override MKMapRect BoundingMapRect
        {
            get { return this.boundingRect; }
        }
    }
}