using SQLite;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;

namespace TreeWatch
{
	public class TreeWatchDatabase
	{
		readonly SQLiteConnection connection;

		public TreeWatchDatabase ()
		{
			connection = DependencyService.Get<ISQLite> ().GetConnection ();
			connection.DropTable<Field> ();
			connection.DropTable<Block> ();
			connection.DropTable<PositionModel> ();
			connection.DropTable<ToDo> ();
			connection.CreateTable<ToDo> ();
			connection.CreateTable<PositionModel> ();
			connection.CreateTable<Block> ();
			connection.CreateTable<Field> ();
			connection.CreateTable<BlockToDo> ();
		}


		public GetItem (int id){
			return connection.Table<T> ().FirstOrDefault (t => t.ID == id);
		}

		public List<T> GetAllItems( item){
			return connection.GetAllWithChildren<T> (null, true);
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

		public insertItem

		public void InsertField (Field field){
			connection.InsertWithChildren (field, true);
		}

		public IList<Field> GetFields(){
			return connection.GetAllWithChildren<Field> (null, true);
			//return (from t in connection.Table<Field> ()
			//        select t).ToList ();
		}

		public Field GetField(int id){
			return connection.Table<Field> ().FirstOrDefault (t => t.ID == id);
		}
			
	}
}

