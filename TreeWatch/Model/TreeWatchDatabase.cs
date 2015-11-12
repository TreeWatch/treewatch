using SQLite;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace TreeWatch
{
	public class TreeWatchDatabase
	{
		readonly SQLiteConnection connection;

		public TreeWatchDatabase ()
		{
			connection = DependencyService.Get<ISQLite> ().GetConnection ();
			connection.CreateTable<ToDo> ();
		}

		public IEnumerable<ToDo> GetToDos() {
			return (from t in connection.Table<ToDo> ()
				select t).ToList ();
		}

		public ToDo GetToDo(int id) {
			return connection.Table<ToDo> ().FirstOrDefault (t => t.ID == id);
		}

		public void InsertToDo (ToDo item){
			connection.Insert (item);
		}
	}
}

