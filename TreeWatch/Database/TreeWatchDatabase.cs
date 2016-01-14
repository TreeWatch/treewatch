// <copyright file="TreeWatchDatabase.cs" company="TreeWatch">
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
    using SQLite.Net;

    using Xamarin.Forms;

    /// <summary>
    /// TreeWatch database.
    /// </summary>
    public class TreeWatchDatabase
    {
        /// <summary>
        /// The connection.
        /// </summary>
        public readonly SQLiteConnection Connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.TreeWatchDatabase"/> class.
        /// </summary>
        public TreeWatchDatabase()
        {
            this.Connection = DependencyService.Get<ISQLite>().GetConnection();
            this.Connection.CreateTable<ToDo>();
            this.Connection.CreateTable<Block>();
            this.Connection.CreateTable<Field>();
            this.Connection.CreateTable<User>();
            this.Connection.CreateTable<Hours>();
            this.Connection.CreateTable<Note>();
            this.Connection.CreateTable<UserToDo>();
            this.Connection.CreateTable<BlockToDo>();
            this.Connection.CreateTable<TreeType>();
            this.Connection.CreateTable<DatabaseConfig>();
            this.Connection.InsertOrIgnore(new DatabaseConfig());
        }

        /// <summary>
        /// Gets or sets the DBconfig.
        /// </summary>
        /// <value>The DBconfig.</value>
        public DatabaseConfig DBconfig
        {
            get
            { 
                return this.Connection.Get<DatabaseConfig>(1);
            }

            set
            { 
                this.Connection.Update(value);
            }
        }

        /// <summary>
        /// Clears the database.
        /// </summary>
        public void ClearDataBase()
        {
            this.Connection.DeleteAll<ToDo>();
            this.Connection.DeleteAll<Block>();
            this.Connection.DeleteAll<Field>();
            this.Connection.DeleteAll<User>();
            this.Connection.DeleteAll<Hours>();
            this.Connection.DeleteAll<Note>();
            this.Connection.DeleteAll<UserToDo>();
            this.Connection.DeleteAll<BlockToDo>();
            this.Connection.DeleteAll<TreeType>();
        }
    }
}