using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
    /// <summary>
    /// Base model for models that are represented as a polygon.
    /// </summary>
    public abstract class PolygonModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the bounding cordinates of the polygon.
        /// </summary>
        /// <value>The bounding cordinates.</value>
        [TextBlob("BoundingCoordiantesBlob")]
        public List<Position> BoundingCoordinates { get; set; }

        /// <summary>
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
        public string BoundingCoordiantesBlob { get; set; }
    }
}