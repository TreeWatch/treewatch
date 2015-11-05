using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms.Maps;

namespace TreeWatch
{
	public class MapViewModel: INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler (this, new PropertyChangedEventArgs (propertyName));
		}

		public Position getCurrentDevicePosition ()
		{
			// Todo: make this not static
			const double latitude = 51.39202;
			const double longitude = 6.04745;

			return new Position (latitude, longitude);
		}
	}
}

