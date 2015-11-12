using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System;

namespace TreeWatch.UITests
{
	[TestFixture (Platform.Android)]
	[TestFixture (Platform.iOS)]
	public class Tests
	{
		protected IApp app;
		Platform platform;

		public Tests (Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest ()
		{
			app = AppInitializer.StartApp (platform);
		}

		[Test]
		public void MapDisplayed ()
		{
			app.Tap ("Map");
			AppResult[] results = app.WaitForElement (c => c.Marked ("MapView"));
			app.Screenshot ("Map screen");
			Assert.IsTrue (results.Any ());
		}

		[Test]
		public void TodoDisplayed ()
		{
			app.Tap ("ToDo");
			AppResult[] results = app.WaitForElement (c => c.Marked ("TodoView"));
			app.Screenshot ("Todo screen");
			Assert.IsTrue (results.Any ());
		}

		[Test]
		public void SettingsDisplayed ()
		{
			if (platform == Platform.Android) {
				app.Tap ("History"); // Workaround for Android to make Settingstab visible
			} 
			app.Tap ("Settings");
			AppResult[] results = app.WaitForElement (c => c.Marked ("SettingsView"));
			app.Screenshot ("Settings screen");
			Assert.IsTrue (results.Any ());
		}

		[Test]
		public void NoteDisplayed ()
		{
			app.Tap ("Note");
			AppResult[] results = app.WaitForElement (c => c.Marked ("NoteView"));
			app.Screenshot ("Note screen");
			Assert.IsTrue (results.Any ());
		}

		[Test]
		public void MapMasterDetailMenuIsDiplayed ()
		{
			app.Tap ("Map");
			AppResult[] results = app.WaitForElement (c => c.Marked ("MapMasterDetailPage"));
			app.Screenshot ("MapMasterDetailPage screen");
			app.Tap ("Menu");
			Assert.IsTrue (results.Any ());
		}
	}
}

