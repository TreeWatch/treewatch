
﻿using System;
using System.Collections.Generic;
using CoreGraphics;
using CoreLocation;
using Foundation;
using MapKit;
using TreeWatch;
using TreeWatch.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using System.Runtime.CompilerServices;

[assembly: ExportRenderer (typeof(FieldMap), typeof(FieldMapRenderer))]

namespace TreeWatch.iOS
{
	public class FieldMapRenderer : MapRenderer
	{
		MKMapView mapView;
		FieldMap myMap;
		List<MKPolygon> polygons;
		List<MKPolygonRenderer> renderers;
		MKPolygonRenderer polygonRenderer;
		UITapGestureRecognizer tapGesture;
		FieldHelper fieldHelper;

		public FieldMapRenderer ()
		{
			polygons = new List<MKPolygon> ();
			renderers = new List<MKPolygonRenderer> ();
			tapGesture = new UITapGestureRecognizer (MapTapped);
			tapGesture.NumberOfTapsRequired = 1;
			fieldHelper = FieldHelper.Instance;
			fieldHelper.FieldSelected += FieldSelected;
		}

		protected void FieldSelected(object sender, FieldSelectedEventArgs e)
		{
			if (mapView != null) 
			{
				var coords = new CLLocationCoordinate2D (e.Field.FieldPinPosition.Latitude, e.Field.FieldPinPosition.Longitude);
				var span = new MKCoordinateSpan (e.Field.FieldHeightLat * 1.1, e.Field.FieldWidthLon * 1.1);
				mapView.Region = new MKCoordinateRegion (coords, span);
				//mapView.SetCenterCoordinate (coords, true);
			}

		}

		protected override void OnElementChanged (ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement == null) {
				mapView = Control as MKMapView;
				mapView.AddGestureRecognizer (tapGesture);
				mapView.GetViewForAnnotation = (mapview, anno) => 
				{
					try
					{
						const string annoId = "pin";
						var ca = (FieldMapAnnotation)anno;
						var aview = (MKPinAnnotationView)mapview.DequeueReusableAnnotation(annoId);
						if (aview == null)
						{
							aview = new MKPinAnnotationView(ca, annoId);
						}
						else 
						{
							aview.Annotation = ca;
						}
						aview.AnimatesDrop = true;
						aview.Selected = true;
						aview.PinColor = MKPinAnnotationColor.Red;
						aview.CanShowCallout = true;

						UIButton detailButton = UIButton.FromType(UIButtonType.DetailDisclosure);

						detailButton.TouchUpInside += (s, ev) => { 
							Console.WriteLine ("Clicked field: {0}", ca.Field.Name);
						};

						aview.RightCalloutAccessoryView = detailButton;

						return aview;
					} catch (Exception)
					{
						return null;
					}
				};
				myMap = e.NewElement as FieldMap;
				mapView.OverlayRenderer = (m, o) => {
					polygonRenderer = new MKPolygonRenderer (o as MKPolygon);
					renderers.Add (polygonRenderer);
					polygonRenderer.FillColor = (o as MKPolygon).Title == "Field" ? myMap.OverLayColor.ToUIColor () : ColorHelper.GetTreeTypeColor ((o as MKPolygon).Title).ToUIColor ();
					return polygonRenderer;
				};

				foreach (var field in myMap.Fields) 
				{

					foreach (var row in field.Rows) {
						if (row.BoundingRectangle.Count != 0) {
							var rowpoints = convertCordinates (row.BoundingRectangle);
							var rowpolygon = MKPolygon.FromCoordinates (rowpoints);
							rowpolygon.Title = ((int)row.TreeType).ToString ();
							polygons.Add (rowpolygon);
						}
					}

					if (field.BoundingCordinates.Count != 0) {
						var points = convertCordinates (field.BoundingCordinates);
						var polygon = MKPolygon.FromCoordinates (points);
						polygon.Title = "Field";
						polygons.Add (polygon);
						var annotation = new FieldMapAnnotation (field);
						mapView.AddAnnotation (annotation);
					}
				}
				mapView.AddOverlays (polygons.ToArray ());
			}
		}

		public MKAnnotationView GetViewForAnnotation (MKMapView mapView, NSObject annotation)
		{
			try
			{
				var ca = (FieldMapAnnotation)annotation;
				var aview = (MKPinAnnotationView)mapView.DequeueReusableAnnotation("pin");
				if (aview == null)
				{
					aview = new MKPinAnnotationView(ca, "pin");
				}
				else 
				{
					aview.Annotation = ca;
				}
				aview.AnimatesDrop = true;
				aview.PinColor = MKPinAnnotationColor.Purple;
				aview.CanShowCallout = true;

				//					UIButton rightCallout = UIButton.FromType(UIButtonType.DetailDisclosure);
				//					rightCallout.Frame = new RectangleF(250,8f,25f,25f);
				//					rightCallout.TouchDown += delegate 
				//					{
				//						NSUrl url = new NSUrl("http://maps.google.com/maps?q=" + ca.Coordinate.ToLL()  );
				//						UIApplication.SharedApplication.OpenUrl(url);
				//					};
				//					aview.RightCalloutAccessoryView = rightCallout;

				return aview;
			} catch (Exception)
			{
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

		private void MapTapped(UITapGestureRecognizer sender)
		{
			CGPoint pointInView = sender.LocationInView (mapView);
			CLLocationCoordinate2D touchCoordinates = mapView.ConvertPoint (pointInView, this.mapView);
			FieldHelper.Instance.FieldTappedEvent(new Position(touchCoordinates.Latitude, touchCoordinates.Longitude));
		}


	}
}
