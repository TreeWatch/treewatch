using System;
using MapKit;
using UIKit;

namespace TreeWatch.iOS
{
	public class FieldMapDelegate : MKMapViewDelegate
	{
		protected string annotationIdentifier = "FieldAnnotation";
		UIButton detailButton;

		public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
		{
			MKAnnotationView annotationView = null;

			if (!(annotation is FieldMapAnnotation))
				return annotationView;
			annotationView = mapView.DequeueReusableAnnotation (annotationIdentifier) ?? new FieldMapAnnotationView (annotation, annotationIdentifier);
			annotationView.CanShowCallout = true;

			return annotationView;
		}


	}
}

