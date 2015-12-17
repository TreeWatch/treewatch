using System;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
    /// <summary>
    /// Heatmap for a <see cref="TreeWatch.Field"/>, uses Points which are weighted.
    /// The Number of Points and Weights must always be the same.
    /// </summary>
    public class HeatMap : BaseModel
    {
        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>The points.</value>
        [OneToMany(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
        public List<HeatmapPoint> Points { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public String Name { get; set; }

        /// <summary>
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
        public HeatMap()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.Heatmap"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="points">Points.</param>
        public HeatMap(String name, List<HeatmapPoint> points)
        {
            this.Name = name;
            this.Points = points;
        }
    }
}

