﻿using CoreGraphics;
using MapKit;

// Analysis disable once InconsistentNaming
namespace TreeWatch.iOS
{
    public class ColorPolygon
    {

        public CGColor FillColor
        {
            get;
            set;
        }

        public bool DrawOutlines
        {
            get;
            set;
        }

        public MKPolygon Polygon
        {
            get;
            set;
        }

        public ColorPolygon(MKPolygon polygon)
        {
            Polygon = polygon;
            DrawOutlines = true;
        }

        public static explicit operator ColorPolygon(MKPolygon polygon)
        {
            var d = new ColorPolygon(polygon);
            return d;
        }
    }
}
