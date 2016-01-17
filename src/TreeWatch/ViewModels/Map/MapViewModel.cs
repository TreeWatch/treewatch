// <copyright file="MapViewModel.cs" company="TreeWatch">
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
namespace TreeWatch
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using System.Windows.Input;
    using Xamarin.Forms;

    /// <summary>
    /// Map view model.
    /// </summary>
    public class MapViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The search text.
        /// </summary>
        private string searchText = string.Empty;

        /// <summary>
        /// The search command.
        /// </summary>
        private Command searchCommand;

        /// <summary>
        /// The selected field.
        /// </summary>
        private Field selectedField;

        /// <summary>
        /// The selected block.
        /// </summary>
        private Block selectedBlock;

        /// <summary>
        /// The field helper.
        /// </summary>
        private FieldHelper fieldHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.MapViewModel"/> class.
        /// </summary>
        public MapViewModel()
        {
            this.fieldHelper = FieldHelper.Instance;
            this.fieldHelper.MapTapped += this.MapTapped;
            this.fieldHelper.FieldSelected += this.FieldSelected;
            this.fieldHelper.BlockSelected += this.BlockSelected;
            this.Fields = new ObservableCollection<Field>(new DBQuery<Field>(App.Database).GetAll());
            this.selectedField = new Field("Dummy", new List<Position>(), new List<Block>());

            foreach (Field f in this.Fields)
            {
                var whc = GeoHelper.CalculateBoundingBox(f.BoundingCoordinates);
                double rad = (whc.WidthInMeters > whc.HeightInMeters) ? whc.WidthInMeters : whc.HeightInMeters;
                rad *= 0.5;
                CrossGeofence.Current.StartMonitoring(new GeofenceCircularRegion(f.Name, whc.Center.Latitude, whc.Center.Longitude, rad)
                    {
                        NotifyOnStay = false,
                        StayedInThresholdDuration = TimeSpan.FromMinutes(10)
                    });
            }
        }

        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the selected field.
        /// </summary>
        /// <value>The selected field.</value>
        public Field SelectedField
        {
            get
            { 
                return this.selectedField;
            }

            set
            {
                if (value != null && !value.Name.Equals(this.selectedField.Name))
                {
                    this.selectedField = value;
                    this.SearchText = string.Empty;
                    this.fieldHelper.FieldSelectedEvent(this.selectedField);
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected block.
        /// </summary>
        /// <value>The selected block.</value>
        public Block SelectedBlock
        {
            get
            {
                return this.selectedBlock;
            }

            set
            {
                if (value != null && value.ID != this.selectedBlock.ID)
                {
                    this.selectedBlock = value;
                    this.fieldHelper.BlockSelectedEvent(this.selectedBlock);
                }
            }
        }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>The search text.</value>
        public string SearchText
        {
            get
            { 
                return this.searchText;
            }

            set
            {
                if (this.searchText != value)
                {
                    this.searchText = value ?? string.Empty;
                    this.OnPropertyChanged("SearchText");

                    if (this.SearchCommand.CanExecute(null))
                    {
                        this.SearchCommand.Execute(null);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the search command.
        /// </summary>
        /// <value>The search command.</value>
        public ICommand SearchCommand
        {
            get
            {
                this.searchCommand = this.searchCommand ?? new Command(this.DoSearchCommand, MapViewModel.CanExecuteSearchCommand);

                return this.searchCommand;
            }
        }

        /// <summary>
        /// Gets the filtered fields.
        /// </summary>
        /// <value>The filtered fields.</value>
        public ObservableCollection<Field> FilteredFields
        {
            get
            {
                var filteredFields = new ObservableCollection<Field>();

                if (this.Fields != null)
                {
                    List<Field> entities = this.Fields.Where(x => x.Name.ToLower().Contains(this.searchText.ToLower())).ToList();
                    if (entities != null && entities.Any())
                    {
                        filteredFields = new ObservableCollection<Field>(entities);
                    }
                }

                return filteredFields;
            }
        }

        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <value>The fields.</value>
        public ObservableCollection<Field> Fields
        {
            get;
            private set;
        }

        /// <summary>
        /// Navigates to field.
        /// </summary>
        /// <param name="field">The field which information should be shown.</param>
        public static void NavigateToField(Field field)
        {
            var navigationPage = (NavigationPage)Application.Current.MainPage;

            var informationViewModel = new InformationViewModel(field);
            navigationPage.PushAsync(new FieldInformationContentPage(informationViewModel));
        }

        /// <summary>
        /// Gets the current device position.
        /// </summary>
        /// <returns>The current device position.</returns>
        public static Position GetCurrentDevicePosition()
        {
            // Todo: make this not static
            var pos = new Position();
            pos.Latitude = 51.39202;
            pos.Longitude = 6.04745;

            return pos;
        }

        /// <summary>
        /// Checks the field clicked.
        /// </summary>
        /// <returns>The field clicked.</returns>
        /// <param name="touchPos">Touch position.</param>
        public Field CheckFieldClicked(Position touchPos)
        {
            foreach (Field field in this.Fields)
            {
                if (GeoHelper.IsInsideCoords(field.BoundingCoordinates, touchPos))
                {
                    return field;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks the block clicked.
        /// </summary>
        /// <returns>The block clicked.</returns>
        /// <param name="touchPos">Touch position.</param>
        public Block CheckBlockClicked(Position touchPos)
        {
            foreach (Block block in this.SelectedField.Blocks)
            {
                if (GeoHelper.IsInsideCoords(block.BoundingCoordinates, touchPos))
                {
                    return block;
                }
            }

            return null;
        }

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Determines if can execute search command.
        /// </summary>
        /// <returns><c>true</c> if can execute search command; otherwise, <c>false</c>.</returns>
        private static bool CanExecuteSearchCommand()
        {
            return true;
        }

        /// <summary>
        /// Maps the tapped.
        /// </summary>
        /// <param name="sender">The sender of the tapped event inside the map.</param>
        /// <param name="e">The arguments of the map tapped event.</param>
        private void MapTapped(object sender, MapTappedEventArgs e)
        {
            var tappedField = this.CheckFieldClicked(e.Position);
            if (tappedField != null)
            {
                this.SelectedField = tappedField;
            }

            if (e.Zoomlevel > 15)
            {
                var tappedBlock = this.CheckBlockClicked(e.Position);
                if (tappedBlock != null)
                {
                    this.selectedBlock = tappedBlock;
                    var navigationPage = (NavigationPage)Application.Current.MainPage;

                    var informationViewModel = new InformationViewModel(this.SelectedField, this.SelectedBlock);
                    navigationPage.PushAsync(new BlockInformationContentPage(informationViewModel));
                }
            }
        }

        /// <summary>
        /// Fields the selected.
        /// </summary>
        /// <param name="sender">The sender of the selected field.</param>
        /// <param name="e">The arguments of the selected field event.</param>
        private void FieldSelected(object sender, FieldSelectedEventArgs e)
        {
            this.SelectedField = e.Field;
        }

        /// <summary>
        /// Blocks the selected.
        /// </summary>
        /// <param name="sender">The sender of the selection.</param>
        /// <param name="e">The arguments of the selection event.</param>
        private void BlockSelected(object sender, BlockSelectedEventArgs e)
        {
            this.SelectedBlock = e.Block;
        }

        /// <summary>
        /// Dos the search command.
        /// </summary>
        private void DoSearchCommand()
        {
            // Refresh the list, which will automatically apply the search text
            this.OnPropertyChanged("FilteredFields");
        }
    }
}