using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System;

namespace TreeWatch
{
	public class MapViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		String searchText = string.Empty;
		Command searchCommand;
		Field selectedField;
		FieldHelper fieldHelper;

		public MapViewModel ()
		{
			fieldHelper = FieldHelper.Instance;
			fieldHelper.FieldTapped += FieldTapped;
			fieldHelper.FieldSelected += FieldSelected;
			Fields = new ObservableCollection<Field> ();
			SetUpMockData ();
			selectedField = new Field ("Dummy");
		}

		public Field SelectedField {
			set {
				if (value != null && !value.Name.Equals (selectedField.Name))
				{
					selectedField = value;
					SearchText = string.Empty;
					fieldHelper.FieldSelectedEvent (selectedField);
				}
			}
			get { return selectedField; }
		}

		private void FieldTapped (object sender, FieldTappedEventArgs e)
		{
			Field tappedField = CheckFieldClicked (e.Position);
			if (tappedField != null)
			{
				SelectedField = tappedField;
			}
		}

		public void FieldSelected (object sender, FieldSelectedEventArgs e)
		{
			SelectedField = e.Field;
		}

		public string SelectedFieldName {
			get {
				return SelectedField.Name;
			}
		}

		public string SelectedFieldWidth {
			get {
				return SelectedField.FieldWidthLon.ToString ();
			}
		}

		public string SelectedFieldHeight {
			get {
				return SelectedField.FieldHeightLat.ToString ();
			}
		}

		public string SelectedFieldLatitude {
			get {
				return SelectedField.FieldPinPosition.Latitude.ToString ();
			}
		}

		public string SelectedFieldLongitude {
			get {
				return SelectedField.FieldPinPosition.Longitude.ToString ();
			}
		}

		public string SelectedFieldBlockCount {
			get {
				return SelectedField.Rows.Count.ToString ();
			}
		}

		void SetUpMockData ()
		{
			Fields.Add (new Field ("Ajax", new List<Position> {
				new Position (51.395390, 6.056181),
				new Position (51.392672, 6.056074),
				new Position (51.392766, 6.053628),
				new Position (51.395189, 6.054014)
			}));

			Fields.Add (new Field ("PSV", new List<Position> {
				new Position (51.487109, 4.464810),
				new Position (51.486474, 4.466023),
				new Position (51.485038, 4.463276),
				new Position (51.486167, 4.461914),
				new Position (51.486454, 4.462643),
				new Position (51.486347, 4.462761)
			}));

			Fields.Add (new Field ("Roda jc", new List<Position> {
				new Position (51.372129, 6.046075),
				new Position (51.369650, 6.047126),
				new Position (51.369476, 6.045667),
				new Position (51.369918, 6.045259),
				new Position (51.371131, 6.042325)
			}));

			Fields.Add (new Field ("VVV", new List<Position> {
				new Position (51.387718, 6.040184),
				new Position (51.386620, 6.036065),
				new Position (51.389485, 6.038983)
			}));

			var field = new Field ("Hertog Jan", new List<Position> {
				new Position (51.389619, 6.047791),
				new Position (51.391065, 6.047748),
				new Position (51.391213, 6.049744),
				new Position (51.391936, 6.049722),
				new Position (51.391922, 6.050967),
				new Position (51.390450, 6.050924)
			});
			Fields.Add (field);

			field.Rows = new List<Row> { new Row (new Position (51.39082462477471, 6.050752777777778), new Position (51.3904837408623, 6.047676310228867), TreeType.APPLE) };
		}

		public string SearchText {
			get { return this.searchText; }
			set {
				if (searchText != value)
				{ 
					searchText = value ?? string.Empty;
					OnPropertyChanged ("SearchText");
					if (SearchCommand.CanExecute (null))
					{
						SearchCommand.Execute (null);
					}
				}
			}
		}

		public ObservableCollection<Field> FilteredFields {
			get {
				var filteredFields = new ObservableCollection<Field> ();

				if (Fields != null)
				{
					List<Field> entities = Fields.Where (x => x.Name.ToLower ().Contains (searchText.ToLower ())).ToList (); 
					if (entities != null && entities.Any ())
					{
						filteredFields = new ObservableCollection<Field> (entities);
					}
				}
				return filteredFields;
			}
		}

		public ICommand SearchCommand {
			get {
				searchCommand = searchCommand ?? new Command (DoSearchCommand, CanExecuteSearchCommand);
				return searchCommand;
			}
		}

		void DoSearchCommand ()
		{
			// Refresh the list, which will automatically apply the search text
			OnPropertyChanged ("FilteredFields");
		}

		static bool CanExecuteSearchCommand ()
		{
			return true;
		}

		protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler (this, new PropertyChangedEventArgs (propertyName));

		}

		public Position getCurrentDevicePosition ()
		{
			// Todo: make this not static
			const double latitude = 51.39202;
			const double longitude = 6.04745;

			return new Position (latitude, longitude);
		}

		public ObservableCollection<Field> Fields {
			get;
			private set;
		}

		public Field CheckFieldClicked (Position touchPos)
		{
			foreach (Field field in Fields)
			{
				if (GeoHelper.isInsideCoords (field.BoundingCordinates, touchPos))
				{
					return field;
				}
			}
			return null;
		}
			
	}
}

