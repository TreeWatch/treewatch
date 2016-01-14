// <copyright file="Tests.cs" company="TreeWatch">
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
    using System.Linq;
    using NUnit.Framework;
    using Xamarin.UITest;
    using Xamarin.UITest.Queries;

    /// <summary>
    /// Tests class.
    /// </summary>
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
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
        /// Initializes a new instance of the <see cref="TreeWatch.UITests.Tests"/> class.
        /// </summary>
        /// <param name="platform">Platform object.</param>
        public Tests(Platform platform)
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
        /// Maps the displayed.
        /// </summary>
        [Test]
        public void MapDisplayed()
        {
            this.app.Tap("Map");
            AppResult[] results = this.app.WaitForElement(c => c.Marked("MapView"));
            this.app.Screenshot("Map screen");
            Assert.IsTrue(results.Any());
        }

        /// <summary>
        /// Histories the displayed.
        /// </summary>
        [Test]
        public void HistoryDisplayed()
        {
            this.app.Tap("History");
            AppResult[] results = this.app.WaitForElement(c => c.Marked("HistoryView"));
            this.app.Screenshot("History screen");
            Assert.IsTrue(results.Any());
        }

        /// <summary>
        /// Todos the displayed.
        /// </summary>
        [Test]
        public void TodoDisplayed()
        {
            this.app.Tap("ToDo");
            AppResult[] results = this.app.WaitForElement(c => c.Marked("TodoView"));
            this.app.Screenshot("Todo screen");
            Assert.IsTrue(results.Any());
        }

        /// <summary>
        /// Settingses the displayed.
        /// </summary>
        [Test]
        public void SettingsDisplayed()
        {
            if (this.platform == Platform.Android)
            {
                this.app.Tap("History"); // Workaround for Android to make Settingstab visible
            }

            this.app.Tap("Settings");
            AppResult[] results = this.app.WaitForElement(c => c.Marked("SettingsView"));
            this.app.Screenshot("Settings screen");
            Assert.IsTrue(results.Any());
        }

        /// <summary>
        /// Notes the displayed.
        /// </summary>
        [Test]
        public void NoteDisplayed()
        {
            this.app.Tap("Note");
            AppResult[] results = this.app.WaitForElement(c => c.Marked("NoteView"));
            this.app.Screenshot("Note screen");
            Assert.IsTrue(results.Any());
        }

        /// <summary>
        /// Maps the master detail menu is diplayed.
        /// </summary>
        [Test]
        public void MapMasterDetailMenuIsDiplayed()
        {
            this.app.Tap("Map");
            AppResult[] results = this.app.WaitForElement(c => c.Marked("MapMasterDetailPage"));
            this.app.Screenshot("MapMasterDetailPage screen");
            this.app.Tap("Menu");
            Assert.IsTrue(results.Any());
        }
    }
}