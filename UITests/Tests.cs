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
		IApp app;
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
		public void HistoryDisplayed ()
		{
			app.Tap ("History");
			AppResult[] results = app.WaitForElement (c => c.Marked ("HistoryLabel"));
			app.Screenshot ("Map screen");
			Assert.IsTrue (results.Any ());
		}
	}
}

