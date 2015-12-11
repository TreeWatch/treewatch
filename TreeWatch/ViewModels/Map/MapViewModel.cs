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
		Block selectedBlock;
		FieldHelper fieldHelper;

		public MapViewModel ()
		{
			fieldHelper = FieldHelper.Instance;
			fieldHelper.MapTapped += MapTapped;
			fieldHelper.FieldSelected += FieldSelected;
			fieldHelper.BlockSelected += BlockSelected;
			Fields = new ObservableCollection<Field> (new DBQuery<Field> (App.Database).GetAll());
			selectedField = new Field ("Dummy", new List<Position> (), new List<Block> ());
            foreach (Field f in Fields)
            {
                var whc = GeoHelper.CalculateWidthHeight(f.BoundingCoordinates);
                double rad = (whc.WidthMeters > whc.HeightMeters) ? whc.WidthMeters : whc.HeightMeters;
                rad *= 0.5;
                Debug.WriteLine("Field: {0}, Center: {1}-{2}, Radius: {3},  Width: {4}, Height: {5}", f.Name, whc.Center.Latitude, whc.Center.Longitude, rad, whc.WidthMeters, whc.HeightMeters);
                CrossGeofence.Current.StartMonitoring(new GeofenceCircularRegion (f.Name, whc.Center.Latitude, whc.Center.Longitude, rad) {

                    NotifyOnStay=false,
                    StayedInThresholdDuration=TimeSpan.FromMinutes(10)

                });
            }
		}

		public Field SelectedField {
			set {
				if (value != null && !value.Name.Equals (selectedField.Name)) {
					selectedField = value;
					SearchText = string.Empty;
					fieldHelper.FieldSelectedEvent (selectedField);
				}
			}
			get { return selectedField; }
		}

		public Block SelectedBlock {
			set {
				if (value != null && value.ID != selectedBlock.ID) {
					selectedBlock = value;
					fieldHelper.BlockSelectedEvent (selectedBlock);
				}
			}
			get { return selectedBlock; }			
        }

        void MapTapped(object sender, MapTappedEventArgs e)
        {
            var tappedField = CheckFieldClicked(e.Position);
            if (tappedField != null)
            {
                SelectedField = tappedField;
            }

            if (e.Zoomlevel > 15)
            {
                var tappedBlock = CheckBlockClicked(e.Position);
                if (tappedBlock != null)
                {
                    selectedBlock = tappedBlock;
                    var navigationPage = (NavigationPage)Application.Current.MainPage;

                    var informationViewModel = new InformationViewModel(SelectedField, SelectedBlock);
                    navigationPage.PushAsync(new BlockInformationContentPage(informationViewModel));

                }
            }

        }

        void FieldSelected(object sender, FieldSelectedEventArgs e)
        {
            SelectedField = e.Field;
        }

        void BlockSelected(object sender, BlockSelectedEventArgs e)
        {
            SelectedBlock = e.Block;
        }

        public void NavigateToField(Field field)
        {
            var navigationPage = (NavigationPage)Application.Current.MainPage;

            var informationViewModel = new InformationViewModel(field);
            navigationPage.PushAsync(new FieldInformationContentPage(informationViewModel));
        }

        public string SearchText
        {
            get { return this.searchText; }
            set
            {
                if (searchText != value)
                { 
                    searchText = value ?? string.Empty;
                    OnPropertyChanged("SearchText");
                    if (SearchCommand.CanExecute(null))
                    {
                        SearchCommand.Execute(null);
                    }
                }
            }
        }

        public ObservableCollection<Field> FilteredFields
        {
            get
            {
                var filteredFields = new ObservableCollection<Field>();

                if (Fields != null)
                {
                    List<Field> entities = Fields.Where(x => x.Name.ToLower().Contains(searchText.ToLower())).ToList(); 
                    if (entities != null && entities.Any())
                    {
                        filteredFields = new ObservableCollection<Field>(entities);
                    }
                }
                return filteredFields;
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                searchCommand = searchCommand ?? new Command(DoSearchCommand, CanExecuteSearchCommand);
                return searchCommand;
            }
        }

        void DoSearchCommand()
        {
            // Refresh the list, which will automatically apply the search text
            OnPropertyChanged("FilteredFields");
        }

        static bool CanExecuteSearchCommand()
        {
            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));

        }

        public Position getCurrentDevicePosition()
        {
            // Todo: make this not static
            var pos = new Position();
            pos.Latitude = 51.39202;
            pos.Longitude = 6.04745;

            return pos;
        }

        public ObservableCollection<Field> Fields
        {
            get;
            private set;
        }

        public Field CheckFieldClicked(Position touchPos)
        {
            foreach (Field field in Fields)
            {
                if (GeoHelper.IsInsideCoords(field.BoundingCoordinates, touchPos))
                {
                    return field;
                }
            }
            return null;
        }

        public Block CheckBlockClicked(Position touchPos)
        {
            foreach (Block block in SelectedField.Blocks)
            {
                if (GeoHelper.IsInsideCoords(block.BoundingCoordinates, touchPos))
                {
                    return block;
                }
            }
            return null;
        }
			
    }
}

