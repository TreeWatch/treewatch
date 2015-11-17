using System.ComponentModel;
using System.Runtime.CompilerServices;

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
				return field.CalculateWidthHeight.Width.ToString ();
			}
		}

		public string FieldHeight {
			get {
				return field.CalculateWidthHeight.Height.ToString ();
			}
		}

		public string FieldLatitude {
			get {
				return field.CalculatePinPosition.Latitude.ToString ();
			}
		}

		public string FieldLongitude {
			get {
				return field.CalculatePinPosition.Longitude.ToString ();
			}
		}

		public string FieldBlockCount {
			get {
				return field.Blocks.Count.ToString ();
			}
		}

		protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler (this, new PropertyChangedEventArgs (propertyName));

		}
	}
}

