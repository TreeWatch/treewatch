using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TreeWatch
{

    public class FieldMap: Map
    {
        readonly Color overlayColor;

        readonly Color boundaryColor;

        public  Color OverLayColor
        {
            get{ return overlayColor; }
        }

        public  Color BoundaryColor
        {
            get{ return boundaryColor; }
        }

        public FieldMap()
            : this(MapSpan.FromCenterAndRadius(new Position(), Distance.FromKilometers(1)))
        {
        }

        public FieldMap(MapSpan region)
            : base(region)
        {
            Fields = new ObservableCollection<Field>();
            //overlayColor = new Color ((204.0 / 255), (40.0 / 255), (196.0 / 255), (127.0 / 255));
            overlayColor = Color.Transparent;
            boundaryColor = Color.FromHex("#ff8400");
        }

        public ObservableCollection<Field> Fields
        {
            get;
            set;
        }
    }
}
