using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using CoreLocation;
using MapKit;
using ObjCRuntime;
using TreeWatch;
using TreeWatch.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FieldMap), typeof(FieldMapRenderer))]

// Analysis disable once InconsistentNaming
namespace TreeWatch.iOS
{
    /// <summary>
    /// iOS specific MapRender
    /// </summary>
    public class FieldMapRenderer : MapRenderer
    {
        /// <summary>
        /// The mercator radius.
        /// </summary>
        private const double MercatorRadius = 85445659.44705395;

        /// <summary>
        /// The max google zoom levels.
        /// </summary>
        private const int MaxGoogleLevels = 20;

        /// <summary>
        /// The map view.
        /// </summary>
        private MKMapView mapView;

        /// <summary>
        /// The FieldMap containing data for the map.
        /// </summary>
        private FieldMap myMap;

        /// <summary>
        /// The tap gesture.
        /// </summary>
        private UITapGestureRecognizer tapGesture;

        /// <summary>
        /// The field helper.
        /// </summary>
        private FieldHelper fieldHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.iOS.FieldMapRenderer"/> class.
        /// </summary>
        public FieldMapRenderer()
        {
            this.tapGesture = new UITapGestureRecognizer(this.MapTapped);
            this.tapGesture.NumberOfTapsRequired = 1;
            this.fieldHelper = FieldHelper.Instance;
            this.fieldHelper.FieldSelected += this.FieldSelected;
            this.fieldHelper.CenterUserPosition += this.CenterOnUserPosition;
        }

        /// <summary>
        /// Converts the coordinates from Postion to CLLocationCoordinate2D.
        /// </summary>
        /// <returns>The coordinates.</returns>
        /// <param name="Coordinates">Coordinates to convert.</param>
        public static CLLocationCoordinate2D[] ConvertCoordinates(List<Position> coordinates)
        {
            var points = new CLLocationCoordinate2D[coordinates.Count];
            var i = 0;
            foreach (var pos in coordinates)
            {
                points[i] = new CLLocationCoordinate2D(pos.Latitude, pos.Longitude);
                i++;
            }

            return points;
        }

        /// <summary>
        /// Calculates the zoomlevel.
        /// </summary>
        /// <returns>The level.</returns>
        /// <param name="mapView">Map view.</param>
        private static double ZoomLevel(MKMapView mapView)
        {
            var longitudeDelta = mapView.Region.Span.LongitudeDelta;
            var mapWidthInPixels = mapView.Bounds.Size.Width;

            double zoomScale = longitudeDelta * MercatorRadius * Math.PI / (180.0 * mapWidthInPixels);
            double zoomLevel = MaxGoogleLevels - Math.Log(zoomScale, 2.0);
            if (zoomLevel < 0)
            {
                zoomLevel = 0;
            }

            return zoomLevel;
        }

        /// <summary>
        /// Gets called when a field is selected.
        /// </summary>
        /// <param name="sender">Sender who fired the event.</param>
        /// <param name="e">Event Arguments.</param>
        protected void FieldSelected(object sender, FieldSelectedEventArgs e)
        {
            if (this.mapView != null)
            {
                var widthHeight = GeoHelper.CalculateBoundingBox(e.Field.BoundingCoordinates);
                var center = widthHeight.Center;
                var coords = new CLLocationCoordinate2D(center.Latitude, center.Longitude);
                var span = new MKCoordinateSpan(widthHeight.Width * 1.1, widthHeight.Height * 1.1);
                this.mapView.Region = new MKCoordinateRegion(coords, span);
            }
        }

        /// <summary>
        /// Get calles when Map is displayed.
        /// </summary>
        /// <param name="e">Event Arguments.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                this.mapView = Control as MKMapView;
                this.mapView.ShowsUserLocation = true;
                this.mapView.DidUpdateUserLocation += this.SetUserPostionOnce;
                this.mapView.AddGestureRecognizer(this.tapGesture);

                /* Readd LFHeatMap project first
                 * Found at https://github.com/TreeWatch/LFHeatMaps
                 * Code:
                 * mapView.RegionChanged += ChangeRegion;
                 */

                this.myMap = e.NewElement as FieldMap;
                this.mapView.OverlayRenderer = this.GetOverlayRender;

                this.AddFields();
            }
        }

        /// <summary>
        /// Sets the user postion once.
        /// </summary>
        /// <param name="sender">Sender who fired the event.</param>
        /// <param name="e">Event Arguments.</param>
        protected void SetUserPostionOnce(object sender, MKUserLocationEventArgs e)
        {
            this.mapView.DidUpdateUserLocation -= this.SetUserPostionOnce;
            this.mapView.SetCenterCoordinate(e.UserLocation.Location.Coordinate, true);
        }

        /// <summary>
        /// Centers on the user position.
        /// </summary>
        /// <param name="sender">Sender who fired the event.</param>
        /// <param name="e">Event Arguments.</param>
        protected void CenterOnUserPosition(object sender, EventArgs e)
        {
            if (this.mapView != null && this.mapView.UserLocation.Location != null)
            {
                this.mapView.SetCenterCoordinate(this.mapView.UserLocation.Location.Coordinate, true);
            }
            else
            {
                this.mapView.DidUpdateUserLocation += this.SetUserPostionOnce;
            }
        }

        /* TODO Readd LFHeatMap project first
         * Found at https://github.com/TreeWatch/LFHeatMaps
         * Code:
        void ChangeRegion(object sender, MKMapViewChangeEventArgs e){

            foreach (var item in mapView.Subviews)
            {
                var heatMap = item as UIHeatMapView;
                if (heatMap != null)
                    heatMap.RefreshHeatMap(mapView);
            }
        }
        */

        /// <summary>
        /// Gets the overlay render for a overlay.
        /// </summary>
        /// <returns>The overlay render.</returns>
        /// <param name="m">The Mapview.</param>
        /// <param name="o">The Overlay.</param>
        private MKOverlayRenderer GetOverlayRender(MKMapView m, IMKOverlay o)
        {
            var overlay = Runtime.GetNSObject(o.Handle) as MKPolygon;
            if (overlay != null)
            {
                var polygon = overlay;
                var polygonRenderer = new MKPolygonRenderer(polygon);

                if (polygon.Title == "Field")
                {
                    polygonRenderer.FillColor = this.myMap.OverLayColor.ToUIColor();
                    polygonRenderer.StrokeColor = this.myMap.BoundaryColor.ToUIColor();
                    polygonRenderer.LineWidth = 1;
                }

                return polygonRenderer;
            }
            else if (o is MultiPolygon)
            {
                return new MultiPolygonView(o);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds the fields to the map.
        /// </summary>
        private void AddFields()
        {
            var connection = new TreeWatchDatabase();
            foreach (var field in this.myMap.Fields)
            {
                var query = new DBQuery<Field>(connection);
                var blockPolygons = new List<ColorPolygon>();
                query.GetChildren(field);
                if (field.Blocks.Count != 0)
                {
                    foreach (var block in field.Blocks)
                    {
                        if (block.BoundingCoordinates.Count != 0 && block.BoundingCoordinates.Count >= 3)
                        {
                            var blockPoints = ConvertCoordinates(block.BoundingCoordinates);
                            var blockPolygon = (ColorPolygon)MKPolygon.FromCoordinates(blockPoints);
                            blockPolygon.FillColor = block.TreeType.ColorProp.ToCGColor();
                            blockPolygons.Add(blockPolygon);
                        }
                    }

                    var blockMultiPolygon = new MultiPolygon(blockPolygons);

                    this.mapView.AddOverlay(blockMultiPolygon);
                }

                if (field.BoundingCoordinates.Count != 0 && field.BoundingCoordinates.Count >= 3)
                {
                    var points = ConvertCoordinates(field.BoundingCoordinates);
                    var polygon = MKPolygon.FromCoordinates(points);
                    polygon.Title = "Field";
                    this.mapView.AddOverlay(polygon);
                }
            }

            var query2 = new DBQuery<HeatMap>(connection);
            var heatmaps = query2.GetAllWithChildren();
            var heatmap = heatmaps[0];

            this.AddHeatMap(heatmap.Points);
        }

        /// <summary>
        /// Adds a HeatMap to the map.
        /// </summary>
        /// <param name="points">Points for the Heatmap.</param>
        private void AddHeatMap(List<HeatmapPoint> points)
        {
            var polygons = new List<ColorPolygon>();

            var max = points.Max(r => r.Mean);
            var min = points.Min(r => r.Mean);

            var difference = max - min;

            foreach (var item in points)
            {
                var singlepolygon = (ColorPolygon)MKPolygon.FromCoordinates(ConvertCoordinates(item.BoundingCoordinates));
                var red = ((((item.Mean - min) / difference) * 245) + 10) / 255;
                var color = Color.FromRgb(red, 0, 1 - red);
                singlepolygon.FillColor = color.ToCGColor();
                singlepolygon.DrawOutlines = false;
                polygons.Add(singlepolygon);
            }

            var heatmap = new MultiPolygon(polygons);

            this.mapView.AddOverlay(heatmap);

            /* Showing a 'Real' heatmap using just points
             * current Version is using multiple polygons
             * TODO Readd LFHeatMap project first
             * Found at https://github.com/TreeWatch/LFHeatMaps
             * Code :
            var positions = new List<Position>();
            var weights = new List<Double>();

            foreach (var item in points)
            {

                foreach (var pos in item.BoundingCoordinates) {
                    positions.Add(pos);
                        weights.Add(item.Mean);
                }
            }
            var view = new UIHeatMapView(positions, weights, mapView);
            mapView.AddSubview(view); */
        }

        /// <summary>
        /// Gets called when the Map is clicked.
        /// </summary>
        /// <param name="sender">Sender who fired the event.</param>
        private void MapTapped(UIGestureRecognizer sender)
        {
            CGPoint pointInView = sender.LocationInView(this.mapView);
            CLLocationCoordinate2D touchCoordinates = this.mapView.ConvertPoint(pointInView, this.mapView);

            FieldHelper.Instance.MapTappedEvent(new Position(touchCoordinates.Latitude, touchCoordinates.Longitude), ZoomLevel(this.mapView));
        }


    }
}
