using System;
using System.Dynamic;
using Xamarin.Forms.Maps;
using System.Diagnostics;

namespace TreeWatch
{
	public class FieldHelper
	{
		private static readonly FieldHelper instance = new FieldHelper();
		public static Field SelectedField { get; set;}

		private FieldHelper ()
		{
			
		}

		public static FieldHelper Instance
		{
			get { return  instance; }
		}

		public event EventHandler<FieldTappedEventArgs> FieldTapped;
		public event EventHandler<FieldSelectedEventArgs> FieldSelected;

		public void FieldTappedEvent(Position pos)
		{
			if (FieldTapped != null) 
			{
				FieldTapped (this, new FieldTappedEventArgs (pos));
			}
		}

		public void FieldSelectedEvent(Field field)
		{
			if (FieldSelected != null) 
			{
				SelectedField = field;
				FieldSelected (this, new FieldSelectedEventArgs (field));
			}
		}
	}
}

