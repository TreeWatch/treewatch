using System;
using System.Collections.Generic;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using TreeWatch;
using TreeWatch.Droid;
using Xamarin.Forms;
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

			foreach (var Field in myMap.Fields) {
				if (Field.Blocks.Count != 0) {
					foreach (var block in Field.Blocks) {
						if (block.BoundingCoordinates.Count != 0 && block.BoundingCoordinates.Count >= 3) {
							Map.AddPolygon (GetPolygon (FieldMapRenderer.ConvertCoordinates (block.BoundingCoordinates), 
								(block.TreeType.ColorProp).ToAndroid ()));
						}
					}
				}

				if (Field.BoundingCoordinates.Count != 0 && Field.BoundingCoordinates.Count >= 3) {
					Map.AddPolygon (GetPolygon (FieldMapRenderer.ConvertCoordinates (Field.BoundingCoordinates),
						myMap.OverLayColor.ToAndroid (), myMap.BoundaryColor.ToAndroid()));
				}

				MarkerOptions marker = new MarkerOptions ();
				marker.SetTitle (Field.Name);
				marker.SetSnippet(string.Format("Number of rows: {0}", Field.Blocks.Count));
				marker.SetPosition (new LatLng(Field.CalculatePinPosition.Latitude, Field.CalculatePinPosition.Longitude));
				Map.AddMarker (marker);
			}
		}

		public PolygonOptions GetPolygon(Java.Util.ArrayList cordinates, Android.Graphics.Color fillColor)
		{
			var polygonOptions = new PolygonOptions ();
			polygonOptions.InvokeFillColor (fillColor);
			polygonOptions.InvokeStrokeWidth (0);
			polygonOptions.AddAll (cordinates);
			return polygonOptions;
		}

		public PolygonOptions GetPolygon(Java.Util.ArrayList cordinates, Android.Graphics.Color fillColor, Android.Graphics.Color boundaryColor)
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
			cords.Add (new LatLng (coordinates [0].Latitude, coordinates [0].Longitude));
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
			Field field = null;
			foreach (Field f in myMap.Fields) 
			{
				if (f.Name.Equals (e.Marker.Title)) 
				{
					field = f;
					break;
				}
			}
			if (field != null) 
			{
				var customTabbedPage = (CustomTabbedPage)Xamarin.Forms.Application.Current.MainPage;
				var masterDetailPage = (MasterDetailPage)customTabbedPage.CurrentPage;
				var mapNavigationPage = (MapNavigationPage)masterDetailPage.Detail;

				mapNavigationPage.PushAsync (new DetailedInformationContentPage (new DetailInformationViewModel (field)));
			}
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
