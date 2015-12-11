// <copyright file="App.cs" company="TreeWatch">
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
namespace TreeWatch
{
    using Xamarin.Forms;

    /// <summary>
    /// This is the main class of this Application.
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// The database.
        /// </summary>
        private static TreeWatchDatabase database;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.App"/> class.
        /// </summary>
        public App()
        {
            // The root page of your application
            this.MainPage = !App.Database.DBconfig.Init ? new SplashPage() as Page : new NavigationPage(new CustomTabbedPage()); 
        }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>The database.</value>
        public static TreeWatchDatabase Database
        {
            get
            { 
                if (database == null)
                {
                    database = new TreeWatchDatabase();
                }

                return database; 
            }
        }
    }
}