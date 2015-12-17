using CoreGraphics;
using MapKit;

// Analysis disable once InconsistentNaming
namespace TreeWatch.iOS
{
    /// <summary>
    /// Polygon with Support for Fillcolor 
    /// </summary>
    public class ColorPolygon
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.iOS.ColorPolygon"/> class.
        /// DrawOutlines is set to true;
        /// </summary>
        /// <param name="polygon">Polygon to extend</param>
        public ColorPolygon(MKPolygon polygon)
        {
            this.Polygon = polygon;
            this.DrawOutlines = true;
        }

        /// <summary>
        /// Gets or sets the fill color.
        /// </summary>
        /// <value>The color of the fill.</value>
        public CGColor FillColor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TreeWatch.iOS.ColorPolygon"/> draw outlines.
        /// </summary>
        /// <value><c>true</c> if draw outlines; otherwise, <c>false</c>.</value>
        public bool DrawOutlines { get; set; }

        /// <summary>
        /// Gets or sets the polygon.
        /// </summary>
        /// <value>The polygon.</value>
        public MKPolygon Polygon { get; set; }

        /// <summary>
        /// MKPolygon to ColorPolygon
        /// </summary>
        /// <param name="polygon">Polygon to convert.</param>
        public static explicit operator ColorPolygon(MKPolygon polygon)
        {
            var d = new ColorPolygon(polygon);
            return d;
        }
    }
}
