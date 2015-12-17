using System;
using CoreGraphics;
using MapKit;

// Analysis disable once InconsistentNaming
namespace TreeWatch.iOS
{
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
