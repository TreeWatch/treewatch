using MapKit;

// Analysis disable once InconsistentNaming
namespace TreeWatch.iOS
{
    public class FieldMapAnnotationView : MKAnnotationView
    {
        readonly IMKAnnotation annotation;
        readonly string annotationId;

        public FieldMapAnnotationView(IMKAnnotation annotation, string annotationId)
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
                //TODO show custom ui view
            }
            // Analysis disable once RedundantIfElseBlock
            else
            {
                //TODO hide custom ui view
            }
        }
    }
}

