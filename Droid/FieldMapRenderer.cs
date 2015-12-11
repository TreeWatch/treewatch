using System;
using System.Collections.Generic;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using TreeWatch;
using TreeWatch.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer (typeof(FieldMap), typeof(FieldMapRenderer))]
namespace TreeWatch.Droid
{
	public class FieldMapRenderer : MapRenderer, IOnMapReadyCallback
	{
		MapView mapView;

		FieldMap myMap;

		FieldHelper fieldHelper;

		new public GoogleMap Map { get; private set; }

		public event EventHandler MapReady;

		public FieldMapRenderer ()
		{
			fieldHelper = FieldHelper.Instance;
			fieldHelper.FieldSelected += FieldSelected;
		}

		protected override void OnElementChanged (ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement == null) {

				mapView = Control as MapView;

				mapView.GetMapAsync (this);

				myMap = e.NewElement as FieldMap;
			}
		}

		void AddFields ()
		{
			foreach (var field in myMap.Fields) {
				var connection = new TreeWatchDatabase ();
				var query = new DBQuery<Field> (connection);
				query.GetChildren (field);
				if (field.Blocks.Count != 0) {
					foreach (var block in field.Blocks) {
						if (block.BoundingCoordinates.Count != 0 && block.BoundingCoordinates.Count >= 3) {
							Map.AddPolygon (GetPolygon (FieldMapRenderer.ConvertCoordinates (block.BoundingCoordinates),
								(block.TreeType.ColorProp).ToAndroid ()));
						}
					}
				}

				if (field.BoundingCoordinates.Count != 0 && field.BoundingCoordinates.Count >= 3) {
					Map.AddPolygon (GetPolygon (FieldMapRenderer.ConvertCoordinates (field.BoundingCoordinates),
						myMap.OverLayColor.ToAndroid (), myMap.BoundaryColor.ToAndroid ()));
				}


			}
		}

		void AddMarker (Field field)
		{
			var marker = new MarkerOptions ();
			marker.SetTitle (field.Name);
			marker.SetSnippet (string.Format ("Number of rows: {0}", field.Blocks.Count));
            var whc = GeoHelper.CalculateWidthHeight(field.BoundingCoordinates);
            marker.SetPosition (new LatLng (whc.Center.Latitude, whc.Center.Longitude));
            marker.SetIcon (BitmapDescriptorFactory.FromResource (Resource.Drawable.MapMarker));
			Map.AddMarker (marker);
		}

		static PolygonOptions GetPolygon (Java.Lang.IIterable coordinates, Android.Graphics.Color color)
		{
			var polygonOptions = new PolygonOptions ();
			polygonOptions.InvokeFillColor (color);
			polygonOptions.InvokeStrokeWidth (1);
			polygonOptions.InvokeStrokeColor (Color.Black.ToAndroid());
			polygonOptions.AddAll (coordinates);

			return polygonOptions;
		}

		static PolygonOptions GetPolygon (Java.Lang.IIterable cordinates, Android.Graphics.Color fillColor, Android.Graphics.Color boundaryColor)
		{
			var polygonOptions = new PolygonOptions ();
			polygonOptions.InvokeFillColor (fillColor);
			polygonOptions.InvokeStrokeWidth (4);
			polygonOptions.InvokeStrokeColor (boundaryColor);
			polygonOptions.AddAll (cordinates);

			return polygonOptions;
		}

		static Java.Util.ArrayList ConvertCoordinates (List<Position> coordinates)
		{
			var cords = new Java.Util.ArrayList ();
			foreach (var pos in coordinates) {
				cords.Add (new LatLng (pos.Latitude, pos.Longitude));

			}
			return cords;
		}

		public void OnMapReady (GoogleMap googleMap)
		{
			Map = googleMap;
			AddFields ();
			Map.MapClick += MapClicked;

			var handler = MapReady;
			if (handler != null)
				handler (this, EventArgs.Empty);
		}

		void InfoWindowClicked (object sender, GoogleMap.InfoWindowClickEventArgs e)
		{
			Marker marker = e.Marker;
			Field field = null;
			foreach (Field f in myMap.Fields) {
				if (f.Name.Equals (e.Marker.Title)) {
					field = f;
					break;
				}
			}
			if (field != null) {
				var navigationPage = (NavigationPage)Application.Current.MainPage;

				navigationPage.PushAsync (new FieldInformationContentPage (new InformationViewModel (field)));
			}
		}

		void MapClicked (Object sender, GoogleMap.MapClickEventArgs e)
		{
			FieldHelper.Instance.MapTappedEvent (new Position (e.Point.Latitude, e.Point.Longitude), Map.CameraPosition.Zoom);
		}

		static void MarkerClicked (object sender, GoogleMap.MarkerClickEventArgs e)
		{
			e.Marker.ShowInfoWindow ();
		}

		public void FieldSelected (object sender, FieldSelectedEventArgs e)
		{
			if (Map != null) {
				var builder = new LatLngBounds.Builder ();
				var whc = GeoHelper.CalculateWidthHeight (e.Field.BoundingCoordinates);
				double width = whc.Width * 0.95;
				double height = whc.Height * 0.59;
                builder.Include (new LatLng (whc.Center.Latitude - width, whc.Center.Longitude - height));
                builder.Include (new LatLng (whc.Center.Latitude + width, whc.Center.Longitude + height));
				var bounds = builder.Build ();
				Map.MoveCamera (CameraUpdateFactory.NewLatLngBounds (bounds, 0));
			}
		}

	}

}
