using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms.Maps;
using System.Diagnostics;
using System.Windows.Input;
using System.Linq;

namespace TreeWatch
{
	public class MapViewModel: INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;

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
	}
}

