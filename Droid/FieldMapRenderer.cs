using System;
using System.Collections.Generic;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using TreeWatch;
using TreeWatch.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;
using Java.Security.Spec;

[assembly: ExportRenderer (typeof(FieldMap), typeof(FieldMapRenderer))]
namespace TreeWatch.Droid
{
	public class FieldMapRenderer : MapRenderer, IOnMapReadyCallback
	{
		MapView mapView;
		FieldMap myMap;

		new public GoogleMap Map { get; private set; }

		public event EventHandler MapReady;

		protected override void OnElementChanged (ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement == null) {

				mapView = Control as MapView;


				mapView.GetMapAsync (this);

				myMap = e.NewElement as FieldMap;
			}
		}

		public void AddFields ()
		{
			var polygon = new PolygonOptions ();
			polygon.InvokeFillColor (myMap.OverLayColor.ToAndroid ());
			polygon.InvokeStrokeWidth (0);

			foreach (var Field in myMap.Fields) {

				foreach (var block in Field.Blocks) {
					if (block.BoundingCoordinates.Count != 0 && block.BoundingCoordinates.Count >= 3) {
						var rowpoints = FieldMapRenderer.ConvertCordinates (block.BoundingCoordinates);
						polygon.InvokeFillColor (ColorHelper.GetTreeTypeColor(block.TreeType).ToAndroid ());
						polygon.AddAll (rowpoints);
						Map.AddPolygon (polygon);
					}

				}
				if (Field.BoundingCoordinates.Count != 0 && Field.BoundingCoordinates.Count >= 3) {
					var polygonpoints = FieldMapRenderer.ConvertCordinates (Field.BoundingCoordinates);
					polygon.InvokeFillColor (myMap.OverLayColor.ToAndroid ());
					polygon.AddAll (polygonpoints);
					Map.AddPolygon (polygon);
				}

				MarkerOptions marker = new MarkerOptions ();
				marker.SetTitle (field.Name);
				marker.SetSnippet(string.Format("Number of rows: {0}", field.Rows.Count));
				marker.SetPosition (new LatLng(field.FieldPinPosition.Latitude, field.FieldPinPosition.Longitude));
				Map.AddMarker (marker);
			}
		}

		public PolygonOptions GetPolygon(Java.Util.ArrayList cordinates, Android.Graphics.Color color)
		{
			var polygonOptions = new PolygonOptions ();
			polygonOptions.InvokeFillColor (color);
			polygonOptions.InvokeStrokeWidth (0);
			polygonOptions.AddAll (cordinates);
			return polygonOptions;
		}

		static Java.Util.ArrayList ConvertCordinates (List<Position> cordinates)
		{
			var cords = new Java.Util.ArrayList ();
			foreach (var pos in cordinates) {
				cords.Add (new LatLng (pos.Latitude, pos.Longitude));
			}
			cords.Add (new LatLng (cordinates [0].Latitude, cordinates [0].Longitude));
			return cords;
		}
		public void OnMapReady (GoogleMap googleMap)
		{
			Map = googleMap;
			Map.SetInfoWindowAdapter (new FieldInfoWindow ());
			AddFields ();
			Map.MarkerClick += MarkerClicked;
			Map.MapClick += MapClicked;
			Map.InfoWindowClick += InfoWindowClicked;
			var handler = MapReady;
			if (handler != null)
				handler (this, EventArgs.Empty);
		}
        
		private void InfoWindowClicked(object sender, GoogleMap.InfoWindowClickEventArgs e)
		{
			Marker marker = e.Marker;
			Field field = new Field("Dummy");
			foreach (Field f in myMap.Fields) 
			{
				if (f.Name.Equals (e.Marker.Title)) 
				{
					field = f;
					break;
				}
			}
			Console.WriteLine ("Selected field: {0}", field.Name);
		}

		private void MapClicked(Object sender, GoogleMap.MapClickEventArgs e)
		{
			FieldHelper.Instance.FieldTappedEvent (new Position(e.Point.Latitude, e.Point.Longitude));
		}

		private void MarkerClicked(object sender, GoogleMap.MarkerClickEventArgs e)
		{
			e.Marker.ShowInfoWindow ();
		}
	}

}
