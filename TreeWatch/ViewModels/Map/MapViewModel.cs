using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TreeWatch
{
	public class MapViewModel
	{
		ObservableCollection<Field> fields;

		private string _searchText = string.Empty;
		private Fields _fields = new TreeWatch.Fields();
		private Command _searchCommand;
		private Field _selectedField;

		public MapViewModel()
		{
			Debug.WriteLine ("Creating SelectFieldCommand");

			SelectFieldCommand = new Command<Field>((field) =>
				{
					if(typeof(Field) == field.GetType()){
						_selectedField = field;
						Debug.WriteLine(">>>>>>>>>> " + field.Name);
					}
				});

				fields = new ObservableCollection<Field>();
				var testfield = new Field("TestField");
				var fieldcords = new List<Position>();

				fieldcords.Add (new Position (51.39202, 6.04745));
				fieldcords.Add (new Position (51.39202, 6.05116));
				fieldcords.Add (new Position (51.38972, 6.05116));
				fieldcords.Add (new Position (51.38972, 6.04745));
				testfield.BoundingCordinates = fieldcords;

				var row = new List<Row> ();
				row.Add (new Row(new Position (51.39082462477471, 6.050752777777778), new Position (51.3904837408623, 6.047676310228867), TreeType.APPLE));
				testfield.Rows = row;
				Fields.Add (testfield);
		}

		public ICommand SelectFieldCommand { private set; get; }

		public string SearchText
		{
			get { return _searchText; }
			set 
			{ 
				if(_searchText != value) 
				{ 
					_searchText = value ?? string.Empty;
					OnPropertyChanged("SearchText");
					if (SearchCommand.CanExecute(null))
					{
						SearchCommand.Execute(null);
					}
				}
			}
		}

		public ObservableCollection<Field> Fields
		{
			get
			{
				ObservableCollection<Field> theCollection = new ObservableCollection<Field>();

				if (_fields != null)
				{
					List<Field> entities = _fields.Where (x => x.Name.ToLower ().Contains (_searchText.ToLower())).ToList(); 
					if (entities != null && entities.Any())
					{
						theCollection = new ObservableCollection<Field>(entities);
					}
				}
				return theCollection;
			}
		}

		public ICommand SearchCommand
		{
			get
			{
				_searchCommand = _searchCommand ?? new Xamarin.Forms.Command(DoSearchCommand, CanExecuteSearchCommand);
				return _searchCommand;
			}
		}

		private void DoSearchCommand()
		{
			// Refresh the list, which will automatically apply the search text
			OnPropertyChanged("Fields");
		}

		private bool CanExecuteSearchCommand()
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
			get { return fields; }
			private set { this.fields = value; }
		}
			
	}
}

