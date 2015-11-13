using Xamarin.Forms;
using System.Diagnostics;

namespace TreeWatch
{
	public partial class NoteContentPage : ContentPage
	{
		public NoteContentPage ()
		{
			// initialize component
			InitializeComponent ();

			picture.GestureRecognizers.Add (new TapGestureRecognizer () {
				Command = new Command (async () => {
					// ToDo
					Debug.WriteLine ("Take a Picture");
				})
			});
		}
	}
}

