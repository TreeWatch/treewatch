using System.Collections.Generic;
using MapKit;

// Analysis disable once InconsistentNaming
namespace TreeWatch.iOS
{
    /// <summary>
    /// Multipolygon for faster rendering on the map contains multiple polygons.
    /// </summary>
    public class MultiPolygon : MKOverlay
    {
        /// <summary>
        /// The bounding rectangle.
        /// </summary>
        private MKMapRect boundingRect;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.iOS.MultiPolygon"/> class.
        /// </summary>
        /// <param name="polygons">List of Polygons to display.</param>
        public MultiPolygon(List<ColorPolygon> polygons)
        {
            this.Polygons = polygons;

            this.boundingRect.Origin = polygons[0].Polygon.BoundingMapRect.Origin;
            foreach (var item in polygons)
            {
                this.boundingRect = MKMapRect.Union(this.boundingRect, item.Polygon.BoundingMapRect);
            }
        }

        /// <summary>
        /// Gets or sets the polygons.
        /// </summary>
        /// <value>The polygons.</value>
        public List<ColorPolygon> Polygons
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the coordinate.
        /// </summary>
        /// <value>The coordinate.</value>
        public override CoreLocation.CLLocationCoordinate2D Coordinate
        {
            get
            {
                return new CoreLocation.CLLocationCoordinate2D(this.BoundingMapRect.MidX, this.BoundingMapRect.MidY);
            }
        }

        /// <summary>
        /// Gets the bounding map rect.
        /// </summary>
        /// <value>The bounding map rect.</value>
        public override MKMapRect BoundingMapRect
        {
            get { return this.boundingRect; }
        }
    }
}