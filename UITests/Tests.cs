using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace TreeWatch.UITests
{
	[TestFixture (Platform.Android)]
	[TestFixture (Platform.iOS)]
	public class Tests
	{
		protected IApp _app;
		Platform platform;

		public Tests (Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest ()
		{
			_app = AppInitializer.StartApp (platform);
		}

		[Test]
		public void MapDisplayed ()
		{
			_app.Tap ("Map");
			AppResult[] results = _app.WaitForElement (c => c.Marked ("MapView"));
			_app.Screenshot ("Map screen");
			Assert.IsTrue (results.Any ());
		}

		[Test]
		public void HistoryDisplayed ()
		{
			_app.Tap ("History");
			AppResult[] results = _app.WaitForElement (c => c.Marked ("HistoryLabel"));
			_app.Screenshot ("history screen");
			Assert.IsTrue (results.Any ());
		}

		public void TodoDisplayed ()
		{
			_app.Tap ("Todo");
			AppResult[] results = _app.WaitForElement (c => c.Marked ("TodoLabel"));
			_app.Screenshot ("Todo screen");
			Assert.IsTrue (results.Any ());
		}

		public void SettingsDisplayed ()
		{
			_app.Tap ("Settings");
			AppResult[] results = _app.WaitForElement (c => c.Marked ("SettingsLabel"));
			_app.Screenshot ("Settings screen");
			Assert.IsTrue (results.Any ());
		}

		public void NoteDisplayed ()
		{
			_app.Tap ("Note");
			AppResult[] results = _app.WaitForElement (c => c.Marked ("NoteLabel"));
			_app.Screenshot ("Note screen");
			Assert.IsTrue (results.Any ());
		}
	}
}

