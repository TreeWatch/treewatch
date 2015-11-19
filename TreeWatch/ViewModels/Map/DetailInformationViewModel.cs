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

		public string FieldSize {
			get {
				return "0 qm";//GeoHelper.CalculateCenter(field.BoundingCoordinates)field.CalculateSize.ToString ();
			}
		}

		public string FieldLatitude {
			get {
				return GeoHelper.CalculateCenter(field.BoundingCoordinates).Latitude.ToString ().Split ('.') [0] + "." + GeoHelper.CalculateCenter(field.BoundingCoordinates).Latitude.ToString ().Split ('.') [1].Substring (0, 6);
			}
		}

		public string FieldLongitude {
			get {
				return GeoHelper.CalculateCenter(field.BoundingCoordinates).Longitude.ToString ().Split ('.') [0] + "." + GeoHelper.CalculateCenter(field.BoundingCoordinates).Longitude.ToString ().Split ('.') [1].Substring (0, 6);
			}
		}

		public string FieldSeaLevel {
			get {
				return "0 m above sea level";//field.SeaLevel ();
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

