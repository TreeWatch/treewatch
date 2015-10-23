using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using TreeWatch.Droid;
using TreeWatch;

[assembly: ExportRenderer (typeof(FieldMap), typeof(FieldMapRenderer))]
namespace TreeWatch.Droid
{
	public class FieldMapRenderer : MapRenderer
	{
		MapView mapView;
		GoogleMap map;
		FieldMap myMap;

		public FieldMapRenderer ()
		{
		}

		protected override void OnElementChanged (Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Xamarin.Forms.View> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement == null) {

				mapView = Control as MapView;
				map = mapView.Map;

				myMap = e.NewElement as FieldMap;

				PolygonOptions polygon = new PolygonOptions ();
				polygon.InvokeFillColor (myMap.OverLayColor.ToAndroid ());
				polygon.InvokeStrokeWidth (0);

				foreach (var Field in myMap.Fields) {

					foreach (var pos in Field.BoundingCordinates) {
						polygon.Add (new LatLng (pos.Latitude, pos.Longitude));
					}
					polygon.Add (new LatLng (Field.BoundingCordinates [0].Latitude, Field.BoundingCordinates [0].Longitude));

					map.AddPolygon (polygon);
				}
			}
		}
	}
}





