using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.Views;
using Android.Widget;

namespace TreeWatch.Droid
{
    public class FieldInfoWindow : Java.Lang.Object, Android.Gms.Maps.GoogleMap.IInfoWindowAdapter
    {

        public View GetInfoContents(Marker marker)
        {
            var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;

            View v = inflater.Inflate(Resource.Layout.field_info_window, null);

            var title = v.FindViewById(Resource.Id.textViewName) as TextView;
            title.Text = marker.Title;

            var description = v.FindViewById(Resource.Id.textViewRows) as TextView;
            description.Text = marker.Snippet;

            return v;
        }

        public View GetInfoWindow(Marker marker)
        {
            return null;
        }
    }
}

