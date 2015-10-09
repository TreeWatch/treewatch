using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Gms.Maps;
using Android.OS;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps.Model;
using TreeWatch.Droid;


[assembly:ExportRenderer(typeof(TreeWatch.ExtendedMap), typeof(MapProvider))]

namespace TreeWatch.Droid
{
	public class MapProvider: MapRenderer, IOnMapReadyCallback
	{
		private GoogleMap _map;

		public void OnMapReady(GoogleMap googleMap)
		{
			_map = googleMap;

		}

		protected override void OnElementChanged(ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged(e);
			if (Control != null)
				((MapView) Control).GetMapAsync(this);
		}
			
	}
			
}

