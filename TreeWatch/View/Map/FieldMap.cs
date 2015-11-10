using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TreeWatch
{

	public class FieldMap: Map
	{
		Color overlayColor;

		public  Color OverLayColor {
			get{ return overlayColor; }
		}


		public FieldMap (MapSpan region) : base (region)
		{
			Fields = new ObservableCollection<Field> ();
			overlayColor = new Color ((204.0 / 255), (40.0 / 255), (196.0 / 255), (127.0 / 255));
		}

		public ObservableCollection<Field> Fields {
			get;
			set;
		}
	}
}
