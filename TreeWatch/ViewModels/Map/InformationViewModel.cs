using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using System.Collections.ObjectModel;
using System;

namespace TreeWatch
{
    public class InformationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public InformationViewModel(Field field, Block block)
        {
            Field = field;
            Block = block;

            if (Field != null)
            {
                VarietyGroups = new ObservableCollection<Varieties>();

                Field.Blocks.Sort();
                Varieties varieties = null;
                foreach (var item in Field.Blocks)
                {
                    if ((VarietyGroups.Count == 0 && varieties == null) || String.Compare(varieties.Variety, item.TreeType.Name) != 0)
                    {
                        varieties = new Varieties(item.TreeType.Name, item.TreeType.ID.ToString(), item.TreeType.ColorProp);

                        varieties.Add(item);

                        VarietyGroups.Add(varieties);
                    }
                    else if (VarietyGroups.Contains(varieties))
                    {
                        varieties.Add(item);
                    }
                }
            }
        }

        public InformationViewModel(Field field)
            : this(field, null)
        {
        }

        public Field Field
        {
            get;
            set;
        }

        public string FieldSize
        {
            get
            {
                return "0 m";//GeoHelper.CalculateCenter(field.BoundingCoordinates)field.CalculateSize.ToString ();
            }
        }

        public ObservableCollection<Varieties> VarietyGroups
        {
            get;
            set;
        }

        public Varieties Variety
        {
            get;
            set;
        }

        public Block Block
        {
            get;
            set;
        }

        public void NavigateToVarieties()
        {
            var navigationPage = (NavigationPage)Application.Current.MainPage;

            navigationPage.PushAsync(new VarietiesInformationContentPage(this));
        }

        public void NavigateToBlocks()
        {
            var navigationPage = (NavigationPage)Application.Current.MainPage;

            navigationPage.PushAsync(new BlocksInformationContentPage(this));
        }

        public void NavigateToBlock()
        {
            var navigationPage = (NavigationPage)Application.Current.MainPage;

            navigationPage.PushAsync(new BlockInformationContentPage(this));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));

        }

        public class Varieties : ObservableCollection<Block>
        {
            public string Variety
            {
                get;
                private set;
            }

            public string ShortVariety
            {
                get;
                private set;
            }

            public Color Color
            {
                get;
                private set;
            }

            public Varieties(string variety, string shortVariety, Color color)
            {
                Variety = variety;
                ShortVariety = shortVariety;
                Color = color;
            }
        }
    }
}

