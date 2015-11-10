using System;
using SQLite;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace TreeWatch
{
	public class TreeWatchDatabase
	{
		private SQLiteConnection _connection;

		public TreeWatchDatabase ()
		{
			_connection = DependencyService.Get<ISQLite> ().GetConnection ();
			_connection.CreateTable<ToDo> ();
		}

		public IEnumerable<ToDo> GetToDos() {
			return (from t in _connection.Table<ToDo> ()
				select t).ToList ();
		}

		public ToDo GetToDo(int id) {
			return _connection.Table<ToDo> ().FirstOrDefault (t => t.ID == id);
		}

		public void InsertToDo (ToDo item){
			_connection.Insert (item);
		}
	}
}

