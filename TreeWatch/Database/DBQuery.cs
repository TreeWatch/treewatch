using SQLite.Net;
using System.Collections.Generic;
using SQLiteNetExtensions.Extensions;

namespace TreeWatch
{
	public class DBQuery<T> where T : BaseModel
	{
		readonly SQLiteConnection connection;

		public DBQuery (TreeWatchDatabase db)
		{
			connection = db.connection;
		}

		public void Insert(T obj)
		{
			connection.Insert (obj);
		}

		public void InsertAll(IEnumerable<T> objs)
		{
			connection.InsertAll (objs);
		}

		public void InsertAllWithChildren(IEnumerable<T> objs)
		{
			connection.InsertAllWithChildren (objs, true);
		}

		public void InsertWithChildren(T obj)
		{
			connection.InsertWithChildren (obj, true);
		}

		public T GetByID (int id)
		{
			return connection.Table<T> ().Where (t => t.ID == id).FirstOrDefault();
		}

		public List<T> GetAllWithChildren()
		{
			return connection.GetAllWithChildren<T> (null, true);
		}

		public void Update(T obj)
		{
			connection.Update (obj);
		}

		public void UpdateWithChildren(T obj)
		{
			connection.UpdateWithChildren (obj);
		}

		public void UpdateAll(IEnumerable<T> objs)
		{
			connection.UpdateAll (objs);
		}

		public void Delete(T obj) 
		{
			connection.Delete (obj);
		}

		public void DeleteAll() 
		{
			connection.DeleteAll<T>();
		}
			
	}
}

