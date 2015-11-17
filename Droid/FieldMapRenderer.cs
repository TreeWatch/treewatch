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
				if (Field.Blocks.Count != 0) {
					foreach (var block in Field.Blocks) {
						if (block.BoundingCoordinates.Count != 0 && block.BoundingCoordinates.Count >= 3) {
							Map.AddPolygon (GetPolygon (FieldMapRenderer.ConvertCoordinates (block.BoundingCoordinates), 
								ColorHelper.GetTreeTypeColor (block.TreeType).ToAndroid ()));
						}

					}
				}

				if (Field.BoundingCoordinates.Count != 0 && Field.BoundingCoordinates.Count >= 3) {
					Map.AddPolygon (GetPolygon (FieldMapRenderer.ConvertCoordinates (Field.BoundingCoordinates),
						myMap.OverLayColor.ToAndroid ()));
				}
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
			AddFields ();
			var handler = MapReady;
			if (handler != null)
				handler (this, EventArgs.Empty);
		}
	}

}
