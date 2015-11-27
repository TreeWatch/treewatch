using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System;

namespace TreeWatch
{
	public class InformationViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public InformationViewModel (Field field, Block block)
		{
			Field = field;
			Block = block;
		}

		public InformationViewModel (Field field) : this (field, null)
		{
		}

		public Field Field {
			get;
			set;
		}

		public Block Block {
			get;
			set;
		}

		public string FieldSize {
			get {
				return "0 qm";//GeoHelper.CalculateCenter(field.BoundingCoordinates)field.CalculateSize.ToString ();
			}
		}

		public void NavigateToBlocks ()
		{
			var navigationPage = (NavigationPage)Application.Current.MainPage;

			navigationPage.PushAsync (new BlocksInformationContentPage (this));
		}

		public void NavigateToBlock ()
		{
			var navigationPage = (NavigationPage)Application.Current.MainPage;

			navigationPage.PushAsync (new BlockInformationContentPage (this));
		}

		protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler (this, new PropertyChangedEventArgs (propertyName));

		}
	}
}

