using System;
using MapKit;
using UIKit;

namespace TreeWatch.iOS
{
	public class FieldMapDelegate : MKMapViewDelegate
	{
		protected string annotationIdentifier = "FieldAnnotation";
		UIButton detailButton;

		public FieldMapDelegate ()
		{
		}
	}
}

