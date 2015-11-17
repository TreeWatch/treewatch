﻿using System;
using System.Collections.Generic;
using CoreGraphics;
using CoreLocation;
using MapKit;
using TreeWatch;
using TreeWatch.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

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

		public FieldMapRenderer ()
		{
			polygons = new List<MKPolygon> ();
			renderers = new List<MKPolygonRenderer> ();
			tapGesture = new UITapGestureRecognizer (MapTapped);
			tapGesture.NumberOfTapsRequired = 1;

		}

		protected override void OnElementChanged (ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement == null) {
				mapView = Control as MKMapView;
				mapView.AddGestureRecognizer (tapGesture);




				myMap = e.NewElement as FieldMap;

				mapView.OverlayRenderer = (m, o) => {
					polygonRenderer = new MKPolygonRenderer (o as MKPolygon);
					renderers.Add (polygonRenderer);
					polygonRenderer.FillColor = (o as MKPolygon).Title == "Field" ? myMap.OverLayColor.ToUIColor () : ColorHelper.GetTreeTypeColor ((o as MKPolygon).Title).ToUIColor ();
					return polygonRenderer;
				};

				foreach (var field in myMap.Fields) {
					if (field.Blocks.Count != 0) {
						foreach (var block in field.Blocks) {
							if (block.BoundingCordinates.Count != 0 && block.BoundingCordinates.Count >= 3) {
								var rowpoints = convertCordinates (block.BoundingCordinates);
								var rowpolygon = MKPolygon.FromCoordinates (rowpoints);
								rowpolygon.Title = ((int)block.TreeType).ToString ();
								polygons.Add (rowpolygon);
							}
						}
					}

					if (field.BoundingCordinates.Count != 0 && field.BoundingCordinates.Count >= 3) {
						var points = convertCordinates (field.BoundingCordinates);
						var polygon = MKPolygon.FromCoordinates (points);
						polygon.Title = "Field";
						polygons.Add (polygon);
					}
				}
				mapView.AddOverlays (polygons.ToArray ());
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
