using CoreLocation;
using MapKit;
using TreeWatch;

// Analysis disable once InconsistentNaming
namespace TreeWatch.iOS
{
    public class FieldMapAnnotation : MKAnnotation
    {

        string title, subtitle;
        CLLocationCoordinate2D coordinate;

        public Field Field { get; private set; }

        public override string Title { get { return title; } }

        public override string Subtitle { get { return subtitle; } }

        public override CLLocationCoordinate2D Coordinate { get { return coordinate; } }

        public FieldMapAnnotation(Field field)
        {
            coordinate = new CLLocationCoordinate2D(GeoHelper.CalculateCenter(field.BoundingCoordinates).Latitude, GeoHelper.CalculateCenter(field.BoundingCoordinates).Longitude);
            Field = field;
            title = field.Name;
            subtitle = string.Format("Number of blocks: {0}", field.Blocks.Count);
        }
    }
}
