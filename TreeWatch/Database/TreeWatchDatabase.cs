using Xamarin.Forms;
using SQLite.Net;

namespace TreeWatch
{
	public class TreeWatchDatabase
	{
		public readonly SQLiteConnection connection;

		public TreeWatchDatabase ()
		{
			connection = DependencyService.Get<ISQLite> ().GetConnection ();
			connection.CreateTable<ToDo> ();
			connection.CreateTable<Block> ();
			connection.CreateTable<Field> ();
			connection.CreateTable<User> ();
			connection.CreateTable<Hours> ();
			connection.CreateTable<Note>();
			connection.CreateTable<UserToDo> ();
			connection.CreateTable<BlockToDo> ();
		}

		public void ClearDataBase()
		{
			connection.DeleteAll<ToDo> ();
			connection.DeleteAll<Block> ();
			connection.DeleteAll<Field> ();
			connection.DeleteAll<User> ();
			connection.DeleteAll<Hours> ();
			connection.DeleteAll<Note> ();
			connection.DeleteAll<UserToDo> ();
			connection.DeleteAll<BlockToDo> ();
		}
			
	}
}

