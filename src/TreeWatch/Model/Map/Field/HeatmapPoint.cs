// <copyright file="HeatmapPoint.cs" company="TreeWatch">
// Copyright © 2015 TreeWatch
// </copyright>
#region Copyright
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
#endregion
namespace TreeWatch
{
    using System.Collections.Generic;
    using SQLiteNetExtensions.Attributes;

    /// <summary>
    /// Stores information belonging to a specific point
    /// </summary>
    public class HeatmapPoint : PolygonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.HeatmapPoint"/> class.
        /// </summary>
        /// <param name="rowId">RowID of the heatmap point.</param>
        /// <param name="fID">FieldID of the heatmap point.</param>
        /// <param name="mean">Mean of the heatmap point.</param>
        /// <param name="std">Std of the heatmap point.</param>
        /// <param name="boundingCoordinates">Bounding coordinates of the heatmap point.</param>
        public HeatmapPoint(double rowId, double fID, double mean, double std, List<Position> boundingCoordinates)
        {
            this.RowID = rowId;
            this.FID = fID;
            this.Mean = mean;
            this.Std = std;
            this.BoundingCoordinates = boundingCoordinates;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.HeatmapPoint"/> class.
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
        public HeatmapPoint()
        {
        }

        /// <summary>
        /// Gets or sets the heatmap identifier.
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
        /// <value>The heatmap identifier.</value>
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
    }
}