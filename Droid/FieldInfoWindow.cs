using System;
using Android.Gms.Maps.Model;
using Android.Widget;
using Android.Views;
using Java.Util.Zip;
using Android.App;
using Android.Content;
using Android.Test.Suitebuilder;

namespace TreeWatch.Droid
{
	public class FieldInfoWindow : Java.Lang.Object, Android.Gms.Maps.GoogleMap.IInfoWindowAdapter
	{

		public FieldInfoWindow ()
		{
		}

		public Android.Views.View GetInfoContents (Marker marker)
		{
			var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;

			View v = inflater.Inflate(Resource.Layout.field_info_window, null);

			TextView title = (TextView) v.FindViewById(Resource.Id.textViewName);
			title.Text = marker.Title;

			TextView description = (TextView) v.FindViewById(Resource.Id.textViewRows);
			description.Text = marker.Snippet;

			return v;
		}
		public Android.Views.View GetInfoWindow (Marker marker)
		{
			return null;
		}
	}
}

