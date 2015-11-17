using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using Xamarin.Forms;

namespace TreeWatch
{
	public class MapViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		String searchText = String.Empty;
		Command searchCommand;
		Field selectedField;
		FieldHelper fieldHelper;

		public MapViewModel ()
		{
			fieldHelper = FieldHelper.Instance;
			fieldHelper.FieldTapped += FieldTapped;
			fieldHelper.FieldSelected += FieldSelected;
			Fields = new ObservableCollection<Field> (new DBQuery<Field> (App.Database).GetAllWithChildren ());
			selectedField = new Field ("Dummy", new List<Position> (), new List<Block> ());
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

		public ICommand SelectFieldCommand { private set; get; }

		public string SearchText {
			get { return this.searchText; }
			set {
				if (searchText != value) { 
					searchText = value ?? string.Empty;
					OnPropertyChanged ("SearchText");
					if (SearchCommand.CanExecute (null)) {
						SearchCommand.Execute (null);
					}
				}
			}
		}

		public ObservableCollection<Field> FilteredFields {
			get {
				var filteredFields = new ObservableCollection<Field> ();

				if (Fields != null) {
					List<Field> entities = Fields.Where (x => x.Name.ToLower ().Contains (searchText.ToLower ())).ToList (); 
					if (entities != null && entities.Any ()) {
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
			var pos = new Position ();
			pos.Latitude = 51.39202;
			pos.Longitude = 6.04745;

			return pos;
		}

		public ObservableCollection<Field> Fields {
			get;
			private set;
		}

		public Field CheckFieldClicked (Position touchPos)
		{
			foreach (Field field in Fields)
			{
				if (GeoHelper.IsInsideCoords (field.BoundingCoordinates, touchPos))
				{
					return field;
				}
			}
			return null;
		}
			
	}
}

