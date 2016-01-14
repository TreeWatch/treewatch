// <copyright file="DBQuery.cs" company="TreeWatch">
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
    using System.Collections.Generic;
    using SQLite.Net;
    using SQLiteNetExtensions.Extensions;

    /// <summary>
    /// DB query.
    /// </summary>
    /// <typeparam name="T">The second generic type parameter.</typeparam>
    public class DBQuery<T> where T : BaseModel
    {
        /// <summary>
        /// The connection.
        /// </summary>
        private readonly SQLiteConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.DBQuery{T}"/> class.
        /// </summary>
        /// <param name="db">Database connection.</param>
        public DBQuery(TreeWatchDatabase db)
        {
            this.connection = db.Connection;
        }

        /// <summary>
        /// Insert the specified object.
        /// </summary>
        /// <param name="obj">Object that should be inserted.</param>
        public void Insert(T obj)
        {
            this.connection.Insert(obj);
        }

        /// <summary>
        /// Inserts all Objects of type T
        /// </summary>
        /// <param name="objs">Objects that should be inserted.</param>
        public void InsertAll(IEnumerable<T> objs)
        {
            this.connection.InsertAll(objs);
        }

        /// <summary>
        /// Inserts all Objects of type T with children.
        /// </summary>
        /// <param name="objs">Objects to be inserted.</param>
        public void InsertAllWithChildren(IEnumerable<T> objs)
        {
            this.connection.InsertAllWithChildren(objs, true);
        }

        /// <summary>
        /// Inserts the object with children.
        /// </summary>
        /// <param name="obj">Children of an object.</param>
        public void InsertWithChildren(T obj)
        {
            this.connection.InsertWithChildren(obj, true);
        }

        /// <summary>
        /// Inserts the Object with children.
        /// </summary>
        /// <param name="obj">Object to be inserted.</param>
        /// <param name="recursive">If set to <c>true</c> recursive inserts childern.</param>
        public void InsertWithChildren(T obj, bool recursive)
        {
            this.connection.InsertWithChildren(obj, recursive);
        }

        /// <summary>
        /// Gets the obejct by ID.
        /// </summary>
        /// <returns>The object matching ID.</returns>
        /// <param name="id">ID of Object in Database.</param>
        public T GetByID(int id)
        {
            return this.connection.Table<T>().Where(t => t.ID == id).FirstOrDefault();
        }

        /// <summary>
        /// Gets all objects of type T.
        /// </summary>
        /// <returns>The all.</returns>
        public List<T> GetAll()
        {
            return this.connection.GetAllWithChildren<T>(null, false);
        }

        /// <summary>
        /// Gets all objects of type T with children.
        /// </summary>
        /// <returns>The all with children.</returns>
        public List<T> GetAllWithChildren()
        {
            return this.connection.GetAllWithChildren<T>(null, true);
        }

        /// <summary>
        /// Gets the children of item.
        /// </summary>
        /// <param name="item">A child item.</param>
        public void GetChildren(T item)
        {
            this.connection.GetChildren<T>(item, true);
        }

        /// <summary>
        /// Update the specified object.
        /// </summary>
        /// <param name="obj">Object to be updated</param>
        public void Update(T obj)
        {
            this.connection.Update(obj);
        }

        /// <summary>
        /// Updates the specified object with children.
        /// </summary>
        /// <param name="obj">Children of Object.</param>
        public void UpdateWithChildren(T obj)
        {
            this.connection.UpdateWithChildren(obj);
        }

        /// <summary>
        /// Updates all objects in objs.
        /// </summary>
        /// <param name="objs">List of Objects to be updated</param>
        public void UpdateAll(IEnumerable<T> objs)
        {
            this.connection.UpdateAll(objs);
        }

        /// <summary>
        /// Updates all objects in objs with children.
        /// </summary>
        /// <param name="objs">List of Objects to be updated</param>
        public void UpdateAllWithChildren(IEnumerable<T> objs)
        {
            foreach (var item in objs)
            {
                this.connection.UpdateWithChildren(item);
            }
        }

        /// <summary>
        /// Delete the specified object.
        /// </summary>
        /// <param name="obj">Object to be deleted.</param>
        public void Delete(T obj)
        {
            this.connection.Delete(obj);
        }

        /// <summary>
        /// Deletes all objects in objs.
        /// </summary>
        /// <param name="objs">Objects to be deleted.</param>
        public void DeleteAll(IEnumerable<T> objs)
        {
            this.connection.Delete(objs);
        }

        /// <summary>
        /// Deletes all objects.
        /// </summary>
        public void DeleteAll()
        {
            this.connection.DeleteAll<T>();
        }
    }
}