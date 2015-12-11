using System;
using System.Collections.Generic;
using CoreGraphics;
using CoreLocation;
using MapKit;
using TreeWatch;
using TreeWatch.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using ObjCRuntime;

[assembly: ExportRenderer (typeof(FieldMap), typeof(FieldMapRenderer))]

namespace TreeWatch.iOS
{
	public class FieldMapRenderer : MapRenderer
	{

		public static double MERCATOR_RADIUS = 85445659.44705395;
		public static int MAX_GOOGLE_LEVELS = 20;

		MKMapView mapView;

		FieldMap myMap;

		UITapGestureRecognizer tapGesture;

		FieldHelper fieldHelper;

		public FieldMapRenderer ()
		{
			tapGesture = new UITapGestureRecognizer (MapTapped);
			tapGesture.NumberOfTapsRequired = 1;
			fieldHelper = FieldHelper.Instance;
			fieldHelper.FieldSelected += FieldSelected;
		}

		protected void FieldSelected (object sender, FieldSelectedEventArgs e)
		{
			if (mapView != null) {
				var widthHeight = GeoHelper.CalculateWidthHeight (e.Field.BoundingCoordinates);
                var center = widthHeight.Center;
				var coords = new CLLocationCoordinate2D (center.Latitude, center.Longitude);
				var span = new MKCoordinateSpan (widthHeight.Width * 1.1, widthHeight.Height * 1.1);
				mapView.Region = new MKCoordinateRegion (coords, span);
			}
		}

		protected override void OnElementChanged (ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement == null) {
				mapView = Control as MKMapView;
                mapView.ShowsUserLocation = true;
				mapView.AddGestureRecognizer (tapGesture);
				mapView.GetViewForAnnotation = GetViewForAnnotation;

				myMap = e.NewElement as FieldMap;
				mapView.OverlayRenderer = GetOverlayRender;

				AddFields ();
			}
		}



		MKOverlayRenderer GetOverlayRender (MKMapView m, IMKOverlay o)
		{
			var overlay = Runtime.GetNSObject(o.Handle) as MKPolygon;
			if (overlay != null) {
				var polygon = overlay;
				var polygonRenderer = new MKPolygonRenderer (polygon);

				if (polygon.Title == "Field") {
					polygonRenderer.FillColor = myMap.OverLayColor.ToUIColor ();
					polygonRenderer.StrokeColor = myMap.BoundaryColor.ToUIColor ();
					polygonRenderer.LineWidth = 1;
				}
				return polygonRenderer;
			} else if (o is MultiPolygon) 
				return new MultiPolygonView (o);
			 else
				return null;
		}

		void AddFields ()
		{
			foreach (var field in myMap.Fields) {
				var connection = new TreeWatchDatabase ();
				var query = new DBQuery<Field> (connection);
				var blockPolygons = new List<ColorPolygon> ();
				query.GetChildren (field);
				if (field.Blocks.Count != 0) {
					foreach (var block in field.Blocks) {
						if (block.BoundingCoordinates.Count != 0 && block.BoundingCoordinates.Count >= 3) {
							var blockPoints = convertCoordinates (block.BoundingCoordinates);
							var blockPolygon = (ColorPolygon) MKPolygon.FromCoordinates (blockPoints);
							blockPolygon.FillColor = block.TreeType.ColorProp.ToCGColor ();
							blockPolygons.Add (blockPolygon);
						}
					}
					var blockMultiPolygon = new MultiPolygon (blockPolygons);

					mapView.AddOverlay (blockMultiPolygon);
				}


				if (field.BoundingCoordinates.Count != 0 && field.BoundingCoordinates.Count >= 3) {
					var points = convertCoordinates (field.BoundingCoordinates);
					var polygon = MKPolygon.FromCoordinates (points);
					polygon.Title = "Field";
					mapView.AddOverlay(polygon);
				}
			}
		}

		void AddFieldMapAnnotation (Field field)
		{
			var annotation = new FieldMapAnnotation (field);
			mapView.AddAnnotation (annotation);
		}

		static MKAnnotationView GetViewForAnnotation (MKMapView mapView, IMKAnnotation annotation)
		{
			try {
				const string annotationId = "pin";
				var fieldMapAnnotation = (FieldMapAnnotation)annotation;
				var aview = (MKPinAnnotationView)mapView.DequeueReusableAnnotation (annotationId);

				if (aview == null) {
					aview = new MKPinAnnotationView (fieldMapAnnotation, annotationId);
				} else {
					aview.Annotation = fieldMapAnnotation;
				}

				aview.AnimatesDrop = true;
				aview.Selected = true;
				aview.PinColor = MKPinAnnotationColor.Red;
				aview.CanShowCallout = true;

				UIButton detailButton = UIButton.FromType (UIButtonType.DetailDisclosure);

				detailButton.TouchUpInside += (s, ev) => {
					var navigationPage = (NavigationPage)Xamarin.Forms.Application.Current.MainPage;

					navigationPage.PushAsync (new FieldInformationContentPage (new InformationViewModel (fieldMapAnnotation.Field)));
				};

				aview.RightCalloutAccessoryView = detailButton;

				return aview;
			} catch (Exception) {
				return null;
			}
		}

		static CLLocationCoordinate2D[] convertCoordinates (List<Position> Coordinates)
		{
			var points = new CLLocationCoordinate2D[Coordinates.Count];
			var i = 0;
			foreach (var pos in Coordinates) {
				points [i] = new CLLocationCoordinate2D (pos.Latitude, pos.Longitude);
				i++;
			}

			return points;
		}

		void MapTapped (UIGestureRecognizer sender)
		{
			CGPoint pointInView = sender.LocationInView (mapView);
			CLLocationCoordinate2D touchCoordinates = mapView.ConvertPoint (pointInView, this.mapView);

			FieldHelper.Instance.MapTappedEvent (new Position (touchCoordinates.Latitude, touchCoordinates.Longitude), ZoomLevel (mapView));
		}

		static double ZoomLevel (MKMapView mapView)
		{
			var longitudeDelta = mapView.Region.Span.LongitudeDelta;
			var mapWidthInPixels = mapView.Bounds.Size.Width;

			double zoomScale = longitudeDelta * MERCATOR_RADIUS * Math.PI / (180.0 * mapWidthInPixels);
			double zoomLevel = MAX_GOOGLE_LEVELS - Math.Log (zoomScale, 2.0);
			if (zoomLevel < 0)
				zoomLevel = 0;

			return zoomLevel;
		}
	}
}
