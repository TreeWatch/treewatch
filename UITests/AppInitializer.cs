using Xamarin.UITest;

namespace TreeWatch.UITests
{
	public static class AppInitializer
	{
		public static IApp StartApp (Platform platform)
		{
			if (platform == Platform.Android) {
				return ConfigureApp.Android.StartApp();
			}

			return ConfigureApp.iOS.StartApp ();
		}
	}
}

