// <copyright file="ColorPolygon.cs" company="TreeWatch">
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
    using CoreGraphics;
    using MapKit;

    /// <summary>
    /// Polygon with Support for Fillcolor 
    /// </summary>
    public class ColorPolygon
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.iOS.ColorPolygon"/> class.
        /// DrawOutlines is set to true;
        /// </summary>
        /// <param name="polygon">Polygon to extend</param>
        public ColorPolygon(MKPolygon polygon)
        {
            this.Polygon = polygon;
            this.DrawOutlines = true;
        }

        /// <summary>
        /// Gets or sets the fill color.
        /// </summary>
        /// <value>The color of the fill.</value>
        public CGColor FillColor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TreeWatch.iOS.ColorPolygon"/> draw outlines.
        /// </summary>
        /// <value><c>true</c> if draw outlines; otherwise, <c>false</c>.</value>
        public bool DrawOutlines { get; set; }

        /// <summary>
        /// Gets or sets the polygon.
        /// </summary>
        /// <value>The polygon.</value>
        public MKPolygon Polygon { get; set; }

        /// <summary>
        /// MKPolygon to ColorPolygon
        /// </summary>
        /// <param name="polygon">Polygon to convert.</param>
        public static explicit operator ColorPolygon(MKPolygon polygon)
        {
            var d = new ColorPolygon(polygon);
            return d;
        }
    }
}
