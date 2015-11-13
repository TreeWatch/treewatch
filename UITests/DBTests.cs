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
		private TreeWatchDatabase _database;

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
			var newToDoTwo = new ToDo("Harvest","Harvest the trees");
<<<<<<< HEAD
			db.InsertToDo (newToDoTwo);
			var list = (List<ToDo>) db.GetToDos ();
=======
			App.Database.InsertToDo (newToDoOne);
			App.Database.InsertToDo (newToDoTwo);
			List<ToDo> list = (List<ToDo>) App.Database.GetToDos ();
>>>>>>> Stash

			Assert.IsNotEmpty(list);
			Assert.IsTrue(list.Contains (newToDoOne));
			Assert.IsTrue (list.Contains (newToDoTwo));

		}
	}
}

