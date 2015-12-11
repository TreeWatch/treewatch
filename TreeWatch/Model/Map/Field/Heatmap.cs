using System;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
    public class Heatmap : BaseModel
    {
        [OneToMany (CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
        public List<HeatmapPoint> Points { get; set; }

        public String Name { get; set; }

        public Heatmap()
        {
        }

        public Heatmap (String name, List<HeatmapPoint> points)
        {
            this.Name = name;
            this.Points = points;
        }
    }
}

