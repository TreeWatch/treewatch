// <copyright file="DBTests.cs" company="TreeWatch">
// Copyright © 2015 TreeWatch
// </copyright>
#region Copyright
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
#endregion
namespace TreeWatch.UITests
{
    using NUnit.Framework;
    using TreeWatch.UITests;
    using Xamarin.UITest;

    /// <summary>
    /// DB tests.
    /// </summary>
    [TestFixture(Platform.iOS)]
    ////[TestFixture (Platform.Android)]
    public class DBTests
    {
        /// <summary>
        /// The app.
        /// </summary>
        private IApp app;

        /// <summary>
        /// The platform.
        /// </summary>
        private Platform platform;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.UITests.DBTests"/> class.
        /// </summary>
        /// <param name="platform">Platform object.</param>
        public DBTests(Platform platform)
        {
            this.platform = platform;
        }

        /// <summary>
        /// Befores the each test.
        /// </summary>
        [SetUp]
        public void BeforeEachTest()
        {
            this.app = AppInitializer.StartApp(this.platform);
        }

        /// <summary>
        /// Tables to do created.
        /// </summary>
        [Test]
        public void TableToDoCreated()
        {
            this.app.Tap("ToDo");

////          var newToDoOne = new ToDo ("Soilscan", "Make a new Soilscan on Field XYZ");
////          var newToDoTwo = new ToDo ("Harvest", "Harvest the trees");
////          new DBQuery<ToDo> (App.Database).InsertWithChildren (newToDoOne);
////          new DBQuery<ToDo> (App.Database).InsertWithChildren (newToDoTwo);
////          var list = new DBQuery<ToDo> (App.Database).GetAllWithChildren ();
////
////          Assert.IsNotEmpty (list);
////          Assert.IsTrue (list.Contains (newToDoOne));
////          Assert.IsTrue (list.Contains (newToDoTwo));
        }
    }
}