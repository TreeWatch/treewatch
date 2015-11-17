using NUnit.Framework;
using System;
using System.Collections.Generic;
using Xamarin.UITest;
using TreeWatch;
using TreeWatch.UITests;

namespace TreeWatch.UITests
{
	[TestFixture (Platform.iOS)]
	//[TestFixture (Platform.Android)]
	public class DBTests
	{

		protected IApp app;
		Platform platform;

		public DBTests (Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest ()
		{
			app = AppInitializer.StartApp (platform);
		}

		[Test]
		public void TableToDoCreated ()
		{
			
			app.Tap ("ToDo");

			var newToDoOne = new ToDo ("Soilscan", "Make a new Soilscan on Field XYZ");
			var newToDoTwo = new ToDo ("Harvest", "Harvest the trees");
//			new DBQuery<ToDo> (App.Database).InsertWithChildren (newToDoOne);
//			new DBQuery<ToDo> (App.Database).InsertWithChildren (newToDoTwo);
//			var list = new DBQuery<ToDo> (App.Database).GetAllWithChildren ();
//
//			Assert.IsNotEmpty (list);
//			Assert.IsTrue (list.Contains (newToDoOne));
//			Assert.IsTrue (list.Contains (newToDoTwo));

		}
	}
}

