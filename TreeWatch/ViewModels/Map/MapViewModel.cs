using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TreeWatch
{
	public class MapViewModel
	{
		ObservableCollection<Field> fields;

		public MapViewModel(){

			fields = new ObservableCollection<Field>();
			var field = new Field("TestField");
			var fieldcords = new List<Position>();

			fieldcords.Add (new Position (51.39202, 6.04745));
			fieldcords.Add (new Position (51.39202, 6.05116));
			fieldcords.Add (new Position (51.38972, 6.05116));
			fieldcords.Add (new Position (51.38972, 6.04745));
			field.BoundingCordinates = fieldcords;

			var row = new List<Row> ();
			row.Add (new Row(new Position (51.39082462477471, 6.050752777777778), new Position (51.3904837408623, 6.047676310228867), TreeType.APPLE));
			//row.Add (new Row(new Position (51.38968639396759, 6.047689303201045), new Position (51.38999818532967,6.047698038109381), TreeType.APPLE));
			field.Rows = row;
			Fields.Add (field);
		}

		public Position getCurrentDevicePosition ()
		{
			// Todo: make this not static
			const double latitude = 51.39202;
			const double longitude = 6.04745;

			return new Position (latitude, longitude);
		}

		public ObservableCollection<Field> Fields {
			get { return fields; }
			private set { this.fields = value; }
		}
			
	}
}

