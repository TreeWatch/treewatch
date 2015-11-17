using Xamarin.Forms;

namespace TreeWatch
{
	public class ToDoNavigationPage : NavigationPage
	{
		public ToDoNavigationPage (Page root) : base (root)
		{
			Title = root.Title;
			if (root.Icon != null) {
				Icon = root.Icon;
			}
		}
	}
}


