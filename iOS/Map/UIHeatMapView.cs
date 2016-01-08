// <copyright file="UIHeatMapView.cs" company="TreeWatch">
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
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", MessageId = "Ctl", Scope = "namespace", Target = "Assembly name", Justification = "Auto generated name")]

// Analysis disable once InconsistentNaming
namespace TreeWatch.iOS
{
    using UIKit;

    /// <summary>
    /// View for a HeatMap.
    /// </summary>
    public class UIHeatMapView : UIImageView
    {
        /* TODO Readd LFHeatMap project first
         * Found at https://github.com/TreeWatch/LFHeatMaps
         *
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
           

            Image = LFHeatMap.LFHeatMap.HeatMapForMapView(view, 0.5F, heatmapPositions, heatmapWeights);
            ContentMode = UIViewContentMode.Center;
        }
 
        public void RefreshHeatMap(MKMapView view)
        {
            Frame = view.Frame;
            Image = LFHeatMap.LFHeatMap.HeatMapForMapView(view, 0.5F, heatmapPositions, heatmapWeights);
        }
        */
    }
}