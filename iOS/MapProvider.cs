using System;
using Google.Maps;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreLocation;
using CoreGraphics;
using Xamarin.Forms;

[assembly:ExportRenderer(typeof(TreeWatch.MapContentPage), typeof(TreeWatch.MapProvider))]

namespace TreeWatch
{
	public class MapProvider: PageRenderer
	{
		public MapProvider ()
		{
			
		}
		MapView mapView;

		public override void LoadView ()
		{
			base.LoadView ();

			CameraPosition camera = CameraPosition.FromCamera (51.335573, 6.099319, 15);

			mapView = MapView.FromCamera (CGRect.Empty, camera);
			mapView.MyLocationEnabled = true;

			var xamMarker = new Marker () {
				Title = "Fleuren HQ",
				Snippet = "Where the magic happens.",
				Position = new CLLocationCoordinate2D (51.335573, 6.099319),
				Map = mapView
			};

			View = mapView;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{	
			base.ViewWillDisappear (animated);
		}
	}
}

