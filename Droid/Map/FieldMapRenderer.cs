// <copyright file="FieldMapRenderer.cs" company="TreeWatch">
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
using TreeWatch;
using TreeWatch.Droid;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(FieldMap), typeof(FieldMapRenderer))]

namespace TreeWatch.Droid
{
    using System;
    using System.Collections.Generic;
    using Android.Gms.Maps;
    using Android.Gms.Maps.Model;
    using Xamarin.Forms.Maps.Android;
    using Xamarin.Forms.Platform.Android;

    /// <summary>
    /// Field map renderer.
    /// </summary>
    public class FieldMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        /// <summary>
        /// The map view.
        /// </summary>
        private MapView mapView;

        /// <summary>
        /// My map.
        /// </summary>
        private FieldMap myMap;

        /// <summary>
        /// The field helper.
        /// </summary>
        private FieldHelper fieldHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.Droid.FieldMapRenderer"/> class.
        /// </summary>
        public FieldMapRenderer()
        {
            this.fieldHelper = FieldHelper.Instance;
            this.fieldHelper.FieldSelected += this.FieldSelected;
        }

        /// <summary>
        /// Occurs when map ready.
        /// </summary>
        public event EventHandler MapReady;

        /// <summary>
        /// Gets the map.
        /// </summary>
        /// <value>The map.</value>
        public new GoogleMap Map
        {
            get;
            private set;
        }

        /// <summary>
        /// Raises the map ready event.
        /// </summary>
        /// <param name="googleMap">Google map.</param>
        public void OnMapReady(GoogleMap googleMap)
        {
            this.Map = googleMap;
            this.AddFields();
            this.Map.MapClick += this.MapClicked;
            this.Map.MyLocationEnabled = true;
            this.Map.UiSettings.MyLocationButtonEnabled = true;
            this.Map.MyLocationChange += this.SetUserPositionOnce;
            var handler = this.MapReady;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Fields the selected.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">E event.</param>
        public void FieldSelected(object sender, FieldSelectedEventArgs e)
        {
            if (this.Map != null)
            {
                var builder = new LatLngBounds.Builder();
                var whc = GeoHelper.CalculateBoundingBox(e.Field.BoundingCoordinates);
                double width = whc.Width * 0.95;
                double height = whc.Height * 0.59;
                builder.Include(new LatLng(whc.Center.Latitude - width, whc.Center.Longitude - height));
                builder.Include(new LatLng(whc.Center.Latitude + width, whc.Center.Longitude + height));
                var bounds = builder.Build();
                this.Map.MoveCamera(CameraUpdateFactory.NewLatLngBounds(bounds, 0));
            }
        }

        /// <summary>
        /// Raises the element changed event.
        /// </summary>
        /// <param name="e">E event.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                this.mapView = Control as MapView;
                this.mapView.GetMapAsync(this);

                this.myMap = e.NewElement as FieldMap;
            }
        }

        /// <summary>
        /// Sets the user position once.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">E event.</param>
        protected void SetUserPositionOnce(object sender, GoogleMap.MyLocationChangeEventArgs e)
        {
            this.Map.MyLocationChange -= this.SetUserPositionOnce;
            var latlng = new LatLng(e.Location.Latitude, e.Location.Longitude);
            this.Map.MoveCamera(CameraUpdateFactory.NewLatLng(latlng));
        }

        /// <summary>
        /// Gets the polygon.
        /// </summary>
        /// <returns>The polygon.</returns>
        /// <param name="coordinates">Coordinates iterable.</param>
        /// <param name="color">Android Color.</param>
        private static PolygonOptions GetPolygon(Java.Lang.IIterable coordinates, Android.Graphics.Color color)
        {
            var polygonOptions = new PolygonOptions();
            polygonOptions.InvokeFillColor(color);
            polygonOptions.InvokeStrokeWidth(1);
            polygonOptions.InvokeStrokeColor(Color.Black.ToAndroid());
            polygonOptions.AddAll(coordinates);

            return polygonOptions;
        }

        /// <summary>
        /// Gets the polygon.
        /// </summary>
        /// <returns>The polygon.</returns>
        /// <param name="coordinates">Cordinates iterable.</param>
        /// <param name="fillColor">Fill color.</param>
        /// <param name="boundaryColor">Boundary color.</param>
        private static PolygonOptions GetPolygon(Java.Lang.IIterable coordinates, Android.Graphics.Color fillColor, Android.Graphics.Color boundaryColor)
        {
            var polygonOptions = new PolygonOptions();
            polygonOptions.InvokeFillColor(fillColor);
            polygonOptions.InvokeStrokeWidth(4);
            polygonOptions.InvokeStrokeColor(boundaryColor);
            polygonOptions.AddAll(coordinates);

            return polygonOptions;
        }

        /// <summary>
        /// Converts the coordinates.
        /// </summary>
        /// <returns>The coordinates.</returns>
        /// <param name="coordinates">Coordinates list.</param>
        private static Java.Util.ArrayList ConvertCoordinates(List<Position> coordinates)
        {
            var coords = new Java.Util.ArrayList();
            foreach (var pos in coordinates)
            {
                coords.Add(new LatLng(pos.Latitude, pos.Longitude));
            }

            return coords;
        }

        /// <summary>
        /// Markers the clicked.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">E event.</param>
        private static void MarkerClicked(object sender, GoogleMap.MarkerClickEventArgs e)
        {
            Android.Util.Log.Verbose("Sender", sender.ToString());
            e.Marker.ShowInfoWindow();
        }

        /// <summary>
        /// Infos the window clicked.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">E event.</param>
        private void InfoWindowClicked(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            Android.Util.Log.Verbose("Sender", sender.ToString());
            Marker marker = e.Marker;
            Field field = null;
            foreach (Field f in this.myMap.Fields)
            {
                if (f.Name.Equals(e.Marker.Title))
                {
                    field = f;
                    break;
                }
            }

            if (field != null)
            {
                var navigationPage = (NavigationPage)Application.Current.MainPage;

                navigationPage.PushAsync(new FieldInformationContentPage(new InformationViewModel(field)));
            }
        }

        /// <summary>
        /// Adds the fields.
        /// </summary>
        private void AddFields()
        {
            foreach (var field in this.myMap.Fields)
            {
                var connection = new TreeWatchDatabase();
                var query = new DBQuery<Field>(connection);
                query.GetChildren(field);
                if (field.Blocks.Count != 0)
                {
                    foreach (var block in field.Blocks)
                    {
                        if (block.BoundingCoordinates.Count != 0 && block.BoundingCoordinates.Count >= 3)
                        {
                            this.Map.AddPolygon(GetPolygon(
                                    FieldMapRenderer.ConvertCoordinates(block.BoundingCoordinates),
                                    block.TreeType.ColorProp.ToAndroid()));
                        }
                    }
                }

                if (field.BoundingCoordinates.Count != 0 && field.BoundingCoordinates.Count >= 3)
                {
                    this.Map.AddPolygon(GetPolygon(
                            FieldMapRenderer.ConvertCoordinates(field.BoundingCoordinates),
                            this.myMap.OverLayColor.ToAndroid(),
                            this.myMap.BoundaryColor.ToAndroid()));
                }
            }
        }

        /// <summary>
        /// Maps the clicked.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">E event.</param>
        private void MapClicked(object sender, GoogleMap.MapClickEventArgs e)
        {
            FieldHelper.Instance.MapTappedEvent(new Position(e.Point.Latitude, e.Point.Longitude), this.Map.CameraPosition.Zoom);
        }
    }
}