using NUnit.Framework;
using System;
using System.Collections.Generic;
using Xamarin.UITest;
using TreeWatch;
using TreeWatch.UITests;

namespace TreeWatch.Tests
{
	[TestFixture ()]
	public class DBTests
	{

		protected IApp app;
		Platform platform;

		public DBTests (Platform platform)
		{
			this.platform = platform;
			app = AppInitializer.StartApp (platform);
		}

		[Test]
		public void TableToDoCreated ()
		{
			var db = new TreeWatchDatabase ();
			var newToDoOne = new ToDo ("Soilscan", "Make a new Soilscan on Field XYZ");
			db.InsertToDo (newToDoOne);
			var newToDoTwo = new ToDo("Harvest","Harvest the trees");
			db.InsertToDo (newToDoTwo);
			List<ToDo> list = (List<ToDo>) db.GetToDos ();

			Assert.IsNotEmpty(list);
			Assert.IsTrue(list.Contains (newToDoOne));
			Assert.IsTrue (list.Contains (newToDoTwo));

		}
	}
}

