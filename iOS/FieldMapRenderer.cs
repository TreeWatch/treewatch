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

[assembly: ExportRenderer (typeof(FieldMap), typeof(FieldMapRenderer))]

namespace TreeWatch.iOS
{
	public class FieldMapRenderer : MapRenderer
	{

		public static double MERCATOR_RADIUS = 85445659.44705395;
		public static int MAX_GOOGLE_LEVELS = 20;

		MKMapView mapView;

		FieldMap myMap;

		List<MKPolygon> polygons;

		List<MKPolygonRenderer> renderers;

		MKPolygonRenderer polygonRenderer;

		UITapGestureRecognizer tapGesture;
		CLLocationManager locationManager;
		FieldHelper fieldHelper;

		public FieldMapRenderer ()
		{
			polygons = new List<MKPolygon> ();
			renderers = new List<MKPolygonRenderer> ();
			tapGesture = new UITapGestureRecognizer (MapTapped);
			tapGesture.NumberOfTapsRequired = 1;
			locationManager = new CLLocationManager ();
			locationManager.RequestWhenInUseAuthorization ();
			fieldHelper = FieldHelper.Instance;
			fieldHelper.FieldSelected += FieldSelected;
		}

		protected void FieldSelected (object sender, FieldSelectedEventArgs e)
		{
			if (mapView != null) {
				var center = GeoHelper.CalculateCenter (e.Field.BoundingCoordinates);
				var widthHeight = GeoHelper.CalculateWidthHeight (e.Field.BoundingCoordinates);
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
				mapView.AddGestureRecognizer (tapGesture);
				myMap = e.NewElement as FieldMap;
				mapView.ShowsUserLocation = true;
				//update user position on map
				mapView.DidUpdateUserLocation += UpdateUserLocation;
				//MKUserTrackingBarButtonItem buttonItem = new MKUserTrackingBarButtonItem(mapView);

				mapView.GetViewForAnnotation = GetViewForAnnotation;

				myMap = e.NewElement as FieldMap;
				mapView.OverlayRenderer = GetOverlayRender;

				AddFields ();
			}
		}

		private void UpdateUserLocation(object sender, MKUserLocationEventArgs e)
		{
			mapView.SetCenterCoordinate(e.UserLocation.Coordinate, true);
			//remove listener so we dont keep setting map to user position
			mapView.DidUpdateUserLocation -= UpdateUserLocation;
		}

		MKOverlayRenderer GetOverlayRender (MKMapView m, IMKOverlay o)
		{
			var polygon = o as MKPolygon;
			polygonRenderer = new MKPolygonRenderer (polygon);
			renderers.Add (polygonRenderer);

			if (polygon.Title == "Field") {
				polygonRenderer.FillColor = myMap.OverLayColor.ToUIColor ();
				polygonRenderer.StrokeColor = myMap.BoundaryColor.ToUIColor ();
				polygonRenderer.LineWidth = 1;
			} else
				polygonRenderer.FillColor = ColorHelper.GetTreeTypeColor (Int32.Parse (polygon.Title)).ToUIColor ();

			return polygonRenderer;
		}

		void AddFields ()
		{
			foreach (var field in myMap.Fields) {
				if (field.Blocks.Count != 0) {
					foreach (var block in field.Blocks) {
						if (block.BoundingCoordinates.Count != 0 && block.BoundingCoordinates.Count >= 3) {
							var rowpoints = convertCordinates (block.BoundingCoordinates);
							var rowpolygon = MKPolygon.FromCoordinates (rowpoints);
							rowpolygon.Title = block.TreeType.ID.ToString ();
							polygons.Add (rowpolygon);
						}
					}
				}

				if (field.BoundingCoordinates.Count != 0 && field.BoundingCoordinates.Count >= 3) {
					var points = convertCordinates (field.BoundingCoordinates);
					var polygon = MKPolygon.FromCoordinates (points);
					polygon.Title = "Field";
					polygons.Add (polygon);

					// AddFieldMapAnnotation (field); Not needed anymore
				}
			}
			mapView.AddOverlays (polygons.ToArray ());
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

		static CLLocationCoordinate2D[] convertCordinates (List<Position> Cordinates)
		{
			var points = new CLLocationCoordinate2D[Cordinates.Count + 1];
			var i = 0;
			foreach (var pos in Cordinates) {
				points [i] = new CLLocationCoordinate2D (pos.Latitude, pos.Longitude);
				i++;
			}
			points [i] = new CLLocationCoordinate2D (Cordinates [0].Latitude, Cordinates [0].Longitude);

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
