using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using System.Collections.Generic;
using Xamarin.Forms.Maps;
using System.Diagnostics;
using System;

namespace TreeWatch
{
	public class SettingsViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public SettingsViewModel (MapContentPage mapContentPage)
		{
			FieldMap = mapContentPage.Content as FieldMap;
			this.MapTypes = new List<MType> ();

			foreach (var name in MapType.GetValues(typeof(MapType))) {
				this.MapTypes.Add (new MType (name.ToString ()));	
			}
		}

		public FieldMap FieldMap { get ; }

		public List<MType> MapTypes { get; }

		public void NavigateToSettings (object mType)
		{
			switch (((MType)mType).Name) {
			case "Satellite":
				FieldMap.MapType = MapType.Satellite;
				break;
			case "Street":
				FieldMap.MapType = MapType.Street;
				break;
			default:
				FieldMap.MapType = MapType.Hybrid;
				break;
			}


			var navigationPage = (NavigationPage)Application.Current.MainPage;

			navigationPage.PopToRootAsync ();
		}

		public void NavigateToMapType ()
		{
			var navigationPage = (NavigationPage)Application.Current.MainPage;

			navigationPage.PushAsync (new MapTypeContentPage (this));
		}

		protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler (this, new PropertyChangedEventArgs (propertyName));

		}

		public struct MType
		{
			public MType (string name)
			{
				this.Name = name;
			}

			public string Name { get; set; }
		}
	}
}

