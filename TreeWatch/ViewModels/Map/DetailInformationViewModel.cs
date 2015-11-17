using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System;

namespace TreeWatch
{
	public class DetailInformationViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		Field field;

		public DetailInformationViewModel (Field field)
		{
			this.field = field;
		}


		public string FieldName {
			get {
				return field.Name;
			}
		}

		public string FieldWidth {
			get {
				return field.FieldWidthLon.ToString ();
			}
		}

		public string FieldHeight {
			get {
				return field.FieldHeightLat.ToString ();
			}
		}

		public string FieldLatitude {
			get {
				return field.FieldPinPosition.Latitude.ToString ();
			}
		}

		public string FieldLongitude {
			get {
				return field.FieldPinPosition.Longitude.ToString ();
			}
		}

		public string FieldBlockCount {
			get {
				return field.Rows.Count.ToString ();
			}
		}

		protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler (this, new PropertyChangedEventArgs (propertyName));

		}
}

