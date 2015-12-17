using System;
using CoreGraphics;
using MapKit;

// Analysis disable once InconsistentNaming
namespace TreeWatch.iOS
{
    public class MultiPolygonView : MKOverlayRenderer
    {
        public MultiPolygonView(IMKOverlay overlay)
        {
            PoylgonOverlay = overlay;
        }

        IMKOverlay PoylgonOverlay;

        public override void DrawMapRect(MKMapRect mapRect, nfloat zoomScale, CGContext context)
        {
            base.DrawMapRect(mapRect, zoomScale, context);
            var multiPolygons = (MultiPolygon)PoylgonOverlay;
            foreach (var item in multiPolygons.Polygons)
            {
                var path = new CGPath();
                InvokeOnMainThread(() =>
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

        public CGPath PolyPath(MKPolygon polygon)
        {
            var path = new CGPath();

            foreach (var item in polygon.InteriorPolygons)
            {
                var interiorPath = PolyPath(item);
                path.AddPath(interiorPath);
            }

            var relativePoint = PointForMapPoint(polygon.Points[0]);
            path.MoveToPoint(relativePoint);
            foreach (var point in polygon.Points)
            {
                path.AddLineToPoint(PointForMapPoint(point));
            }

            return path;
        }

    }
}
