﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TreeWatch
{

	public class FieldMap: Map
	{
		private Color overlayColor;

		public  Color OverLayColor {
			get{ return overlayColor; }
		}

		private ObservableCollection<Field> fields;

		public FieldMap (MapSpan region) : base (region)
		{
			fields = new ObservableCollection<Field> ();
			overlayColor = new Color ((204.0 / 255), (40.0 / 255), (196.0 / 255), (127.0 / 255));
		}

		public ObservableCollection<Field> Fields {
			get { return fields; }
			set { this.fields = value; }

		}
	}
}
