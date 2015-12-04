using MapKit;
using System.Collections.Generic;

namespace TreeWatch.iOS
{
	public class MultiPolygon : MKOverlay
	{

		MKMapRect boundingRect;
		public List<ColorPolygon> Polygons {
			get;
			set;
		}

		public override CoreLocation.CLLocationCoordinate2D Coordinate {
			get {
				return new CoreLocation.CLLocationCoordinate2D (BoundingMapRect.MidX, BoundingMapRect.MidY);
			}
		}

		public override MKMapRect BoundingMapRect {
			get {return boundingRect;}
		}

		public MultiPolygon (List<ColorPolygon> polygons)
		{
			
			Polygons = polygons;

			boundingRect.Origin = polygons [0].Polygon.BoundingMapRect.Origin;
			foreach (var item in polygons) {
				boundingRect = MKMapRect.Union(boundingRect, item.Polygon.BoundingMapRect);

			}
		}
	}
}

