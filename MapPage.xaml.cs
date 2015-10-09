using System;
using System.Collections.Generic;

using Xamarin.Forms;
#if __ANDROID__
using Xamarin.Forms.Maps;
#endif

namespace TreeWatch
{
	public partial class MapPage : ContentPage
	{
		public MapPage ()
		{
			InitializeComponent ();


			#if __ANDROID__
			this.Content = new ExtendedMap ();
			#endif
		}


	}
	#if __ANDROID__
	public class ExtendedMap : Map
	{
		public ExtendedMap ()
		{

		}

		public ExtendedMap (MapSpan region) : base (region)
		{

		}
	}
	#endif
}

