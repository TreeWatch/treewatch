using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
    public class HeatmapPoint : PolygonModel
    {
        [ForeignKey(typeof(Heatmap))]
        public int HeatmapId { get; set; }

        public double RowID { get; set; }

        public double FID { get; set; }

        public double Mean { get; set; }

        public double Std { get; set; }

        public HeatmapPoint()
        {
        }

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

