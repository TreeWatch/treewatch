using System;

using CoreLocation;

using MapKit;

using TreeWatch;
using TreeWatch.iOS;

using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer (typeof(FieldMap), typeof(FieldMapRenderer))]

namespace TreeWatch.iOS
{
	public class FieldMapRenderer : MapRenderer
	{
		MKMapView mapView;
		FieldMap myMap;
		MKPolygon polygon;
		MKPolygonRenderer polygonRenderer;

		protected override void OnElementChanged (Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement == null) {

				mapView = Control as MKMapView;

				myMap = e.NewElement as FieldMap;

				mapView.OverlayRenderer = (m, o) => {
					if (polygonRenderer == null) {
						polygonRenderer = new MKPolygonRenderer (o as MKPolygon);
						polygonRenderer.FillColor = myMap.OverLayColor.ToUIColor ();
					}
					return polygonRenderer;
				};

				foreach (var Field in myMap.Fields) {

					var points = new CLLocationCoordinate2D[Field.BoundingCordinates.Count + 1];
					var i = 0;
					foreach (var pos in Field.BoundingCordinates) {
						points [i] = new CLLocationCoordinate2D (pos.Latitude, pos.Longitude);
						i++;
					}
					points [i] = new CLLocationCoordinate2D (Field.BoundingCordinates [0].Latitude, 
						Field.BoundingCordinates [0].Longitude);

					polygon = MKPolygon.FromCoordinates (points);
					mapView.AddOverlay (polygon);
				}


			}
		}
	}
}
