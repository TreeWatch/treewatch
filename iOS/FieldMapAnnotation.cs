using System;
using MapKit;
using CoreLocation;
using Xamarin.Forms.Maps;
using System.Diagnostics;

namespace TreeWatch.iOS
{
	public class FieldMapAnnotation : MKAnnotation
	{
		
		string title, subtitle;
		CLLocationCoordinate2D coordinate;

		public Field Field { get; private set; }
		public override string Title {get { return title; } }
		public override string Subtitle {get { return subtitle; } }
		public override CLLocationCoordinate2D Coordinate { get { return this.coordinate; } }

		public FieldMapAnnotation (Field field)
		{
			this.coordinate = new CLLocationCoordinate2D( field.FieldPinPosition.Latitude, field.FieldPinPosition.Longitude);
			this.Field = field;
			this.title = field.Name;
			this.subtitle = string.Format ("Number of rows: {0}", field.Rows.Count);
		}
	}
}

