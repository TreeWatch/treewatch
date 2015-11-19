using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TreeWatch
{
	public class FieldInformationViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		Field field;

		public FieldInformationViewModel (Field field)
		{
			this.field = field;
		}


		public string FieldName {
			get {
				return field.Name;
			}
		}

		public string FieldSize {
			get {
				return "0 qm";//GeoHelper.CalculateCenter(field.BoundingCoordinates)field.CalculateSize.ToString ();
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

