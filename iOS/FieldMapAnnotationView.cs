using System;
using MapKit;

namespace TreeWatch.iOS
{
	public class FieldMapAnnotationView : MKAnnotationView
	{
		private readonly IMKAnnotation annotation;
		private readonly string annotationId;

		public FieldMapAnnotationView (IMKAnnotation annotation, string annotationId)
		{
			this.annotation = annotation;
			this.annotationId = annotationId;
			CanShowCallout = false;
		}

		public override void SetSelected(bool selected, bool animated)
		{
			base.SetSelected(selected, animated);

			if (selected)
			{
				// show custom ui view
			}
			else
			{
				// hide custom ui view
			}
		}
	}
}

