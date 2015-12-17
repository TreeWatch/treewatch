using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
    /// <summary>
    /// Stores information belonging to a specific point
    /// </summary>
    public class HeatmapPoint : PolygonModel
    {
        /// <summary>
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
        [ForeignKey(typeof(HeatMap))]
        public int HeatmapId { get; set; }

        /// <summary>
        /// Gets or sets the rowID.
        /// </summary>
        /// <value>The rowID.</value>
        public double RowID { get; set; }

        /// <summary>
        /// Gets or sets the FieldID.
        /// </summary>
        /// <value>The FieldID.</value>
        public double FID { get; set; }

        /// <summary>
        /// Gets or sets the mean.
        /// </summary>
        /// <value>The mean.</value>
        public double Mean { get; set; }

        /// <summary>
        /// Gets or sets the std.
        /// </summary>
        /// <value>The std.</value>
        public double Std { get; set; }

        /// <summary>
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
        public HeatmapPoint()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.HeatmapPoint"/> class.
        /// </summary>
        /// <param name="rowId">RowID.</param>
        /// <param name="fID">FieldID</param>
        /// <param name="mean">Mean.</param>
        /// <param name="std">Std.</param>
        /// <param name="boundingCoordinates">Bounding coordinates.</param>
        public HeatmapPoint(double rowId, double fID, double mean, double std, List<Position> boundingCoordinates)
        {
            this.RowID = rowId;
            this.FID = fID;
            this.Mean = mean;
            this.Std = std;
            this.BoundingCoordinates = boundingCoordinates;
        }
    }
}

