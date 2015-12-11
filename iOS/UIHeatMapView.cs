using System;
using UIKit;
using System.Collections.Generic;
using CoreLocation;
using Foundation;
using MapKit;

namespace TreeWatch.iOS
{
    public class UIHeatMapView : UIImageView
    {
        readonly CLLocation[] heatmapPositions;

        readonly NSNumber[] heatmapWeights;


        public UIHeatMapView(List<Position> positions, List<Double> weights, MKMapView view): base(view.Frame)
        {
            heatmapPositions = new CLLocation[positions.Count];
            heatmapWeights = new NSNumber[positions.Count];

            for (var i = 0; i < positions.Count; i++) {
                heatmapPositions [i] = new CLLocation (positions[i].Latitude, positions[i].Longitude);
                heatmapWeights[i] = weights[i];
            }
           
            /* Readd LFHeatMap project first
             * Found at https://github.com/TreeWatch/LFHeatMaps
             */
            //Image = LFHeatMap.LFHeatMap.HeatMapForMapView(view, 0.5F, heatmapPositions, heatmapWeights);
            ContentMode = UIViewContentMode.Center;
        }
 
        public void RefreshHeatMap(MKMapView view)
        {
            Frame = view.Frame;
            /* Readd LFHeatMap project first
             * Found at https://github.com/TreeWatch/LFHeatMaps
             */
            //Image = LFHeatMap.LFHeatMap.HeatMapForMapView(view, 0.5F, heatmapPositions, heatmapWeights);
        }

    }

}

