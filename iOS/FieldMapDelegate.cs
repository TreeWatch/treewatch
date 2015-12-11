using MapKit;

// Analysis disable once InconsistentNaming
namespace TreeWatch.iOS
{
    public class FieldMapDelegate : MKMapViewDelegate
    {
        protected string annotationIdentifier = "FieldAnnotation";
        //FIXME can be deleted?
        //		UIButton detailButton;

        public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;

            if (!(annotation is FieldMapAnnotation))
                return annotationView;
            annotationView = mapView.DequeueReusableAnnotation(annotationIdentifier) ?? new FieldMapAnnotationView(annotation, annotationIdentifier);
            annotationView.CanShowCallout = true;

            return annotationView;
        }


    }
}