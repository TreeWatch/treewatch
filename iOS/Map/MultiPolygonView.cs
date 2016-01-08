// <copyright file="MultiPolygonView.cs" company="TreeWatch">
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
    using System;
    using CoreGraphics;
    using MapKit;

    /// <summary>
    /// View for a multipolygon. 
    /// Handles custom drawing of all polygons inside a multipolygon.
    /// </summary>
    public class MultiPolygonView : MKOverlayRenderer
    {
        /// <summary>
        /// The poylgon overlay.
        /// </summary>
        private IMKOverlay polygonOverlay;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.iOS.MultiPolygonView"/> class.
        /// </summary>
        /// <param name="overlay">Overlay to be displayed.</param>
        public MultiPolygonView(IMKOverlay overlay)
        {
            this.polygonOverlay = overlay;
        }

        /// <summary>
        /// Draws the map rectangle.
        /// </summary>
        /// <param name="mapRect">Map rectangle.</param>
        /// <param name="zoomScale">Zoom scale.</param>
        /// <param name="context"> Graphics context.</param>
        public override void DrawMapRect(MKMapRect mapRect, nfloat zoomScale, CGContext context)
        {
            base.DrawMapRect(mapRect, zoomScale, context);
            var multiPolygons = (MultiPolygon)this.polygonOverlay;
            foreach (var item in multiPolygons.Polygons)
            {
                var path = new CGPath();
                this.InvokeOnMainThread(() =>
                    {
                        path = PolyPath(item.Polygon);
                    });
                if (path != null)
                {
                    context.SetFillColor(item.FillColor);
                    context.BeginPath();
                    context.AddPath(path);
                    context.DrawPath(CGPathDrawingMode.EOFill);
                    if (item.DrawOutlines)
                    {
                        context.BeginPath();
                        context.AddPath(path);
                        context.StrokePath();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the poly path for a polygon.
        /// </summary>
        /// <returns>The path.</returns>
        /// <param name="polygon">Polygon to get the path for.</param>
        public CGPath PolyPath(MKPolygon polygon)
        {
            var path = new CGPath();

            foreach (var item in polygon.InteriorPolygons)
            {
                var interiorPath = this.PolyPath(item);
                path.AddPath(interiorPath);
            }

            var relativePoint = PointForMapPoint(polygon.Points[0]);
            path.MoveToPoint(relativePoint);
            foreach (var point in polygon.Points)
            {
                path.AddLineToPoint(this.PointForMapPoint(point));
            }

            return path;
        }
    }
}
