using System.Collections.Generic;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;

namespace TreeWatch
{
    /// <summary>
    /// Providing methods for a model of type <see cref="TreeWatch.BaseModel"/>to retrieve or 
    /// update informatio in the database.
    /// </summary>
    public class DBQuery<T> where T : BaseModel
    {
        readonly SQLiteConnection connection;

        /// <summary>
        /// Sets the database connection used by <see cref="TreeWatch.DBQuery`1"/>.
        /// </summary>
        /// <param name="db">Database connection</param>

        public DBQuery(TreeWatchDatabase db)
        {
            connection = db.Connection;
        }

        /// <summary>
        /// Insert the specified object.
        /// </summary>
        /// <param name="obj">Object.</param>
        public void Insert(T obj)
        {
            connection.Insert(obj);
        }

        /// <summary>
        /// Inserts all Objects of type T
        /// </summary>
        /// <param name="objs">Objects.</param>
        public void InsertAll(IEnumerable<T> objs)
        {
            connection.InsertAll(objs);
        }

        /// <summary>
        /// Inserts all Objects of type T with children.
        /// </summary>
        /// <param name="objs">Objects to be inserted.</param>
        public void InsertAllWithChildren(IEnumerable<T> objs)
        {
            connection.InsertAllWithChildren(objs, true);
        }

        /// <summary>
        /// Inserts the object with children.
        /// </summary>
        /// <param name="obj">Object.</param>
        public void InsertWithChildren(T obj)
        {
            connection.InsertWithChildren(obj, true);
        }

        /// <summary>
        /// Inserts the Object with children.
        /// </summary>
        /// <param name="obj">Object to be inserted.</param>
        /// <param name="recursive">If set to <c>true</c> recursive inserts childern.</param>
        public void InsertWithChildren(T obj, bool recursive)
        {
            connection.InsertWithChildren(obj, recursive);
        }

        /// <summary>
        /// Gets the obejct by ID.
        /// </summary>
        /// <returns>The object matching ID.</returns>
        /// <param name="id">ID of Object in Database.</param>
        public T GetByID(int id)
        {
            return connection.Table<T>().Where(t => t.ID == id).FirstOrDefault();
        }

        /// <summary>
        /// Gets all objects of type T.
        /// </summary>
        /// <returns>The all.</returns>
        public List<T> GetAll()
        {
            return connection.GetAllWithChildren<T>(null, false);
        }

        /// <summary>
        /// Gets all objects of type T with children.
        /// </summary>
        /// <returns>The all with children.</returns>
        public List<T> GetAllWithChildren()
        {
            return connection.GetAllWithChildren<T>(null, true);
        }

        /// <summary>
        /// Gets the children of item.
        /// </summary>
        /// <param name="item">Item.</param>
        public void GetChildren(T item)
        {
            connection.GetChildren<T>(item, true);
        }

        /// <summary>
        /// Update the specified object.
        /// </summary>
        /// <param name="obj">Object to be updated</param>
        public void Update(T obj)
        {
            connection.Update(obj);
        }

        /// <summary>
        /// Updates the  specified object with children.
        /// </summary>
        /// <param name="obj">Object.</param>
        public void UpdateWithChildren(T obj)
        {
            connection.UpdateWithChildren(obj);
        }

        /// <summary>
        /// Updates all objects in objs.
        /// </summary>
        /// <param name="objs">List of Objects to be updated</param>
        public void UpdateAll(IEnumerable<T> objs)
        {
            connection.UpdateAll(objs);
        }

        /// <summary>
        /// Updates all objects in objs with children.
        /// </summary>
        /// <param name="objs">List of Objects to be updated</param>
        public void UpdateAllWithChildren(IEnumerable<T> objs)
        {
            foreach (var item in objs)
            {
                connection.UpdateWithChildren(item);
            }
        }

        /// <summary>
        /// Delete the specified object.
        /// </summary>
        /// <param name="obj">Object.</param>
        public void Delete(T obj)
        {
            connection.Delete(obj);
        }

        /// <summary>
        /// Deletes all objects in objs.
        /// </summary>
        /// <param name="objs">Objects.</param>
        public void DeleteAll(IEnumerable<T> objs)
        {
            connection.Delete(objs);
        }

        /// <summary>
        /// Deletes all objects.
        /// </summary>
        public void DeleteAll()
        {
            connection.DeleteAll<T>();
        }
    }
}