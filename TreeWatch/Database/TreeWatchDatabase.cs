using Xamarin.Forms;
using SQLite.Net;
using System;

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
			connection.CreateTable<Note> ();
			connection.CreateTable<UserToDo> ();
			connection.CreateTable<BlockToDo> ();
			connection.CreateTable<TreeType> ();
			connection.CreateTable<DatabaseConfig> ();
            connection.CreateTable<Heatmap>();
            connection.CreateTable<HeatmapPoint>();
			connection.InsertOrIgnore (new DatabaseConfig ());

		}

		public void ClearDataBase ()
		{
			connection.DeleteAll<ToDo> ();
			connection.DeleteAll<Block> ();
			connection.DeleteAll<Field> ();
			connection.DeleteAll<User> ();
			connection.DeleteAll<Hours> ();
			connection.DeleteAll<Note> ();
			connection.DeleteAll<UserToDo> ();
			connection.DeleteAll<BlockToDo> ();
			connection.DeleteAll<TreeType> ();
            connection.DeleteAll<Heatmap>();
            connection.DeleteAll<HeatmapPoint>();
			//connection.Execute ("DROP TABLE initialized");
		}

		public DatabaseConfig DBconfig{
			get{ return connection.Get<DatabaseConfig> (1); } set{ connection.Update (value); }
		}
			
	}

	public class DatabaseConfig : BaseModel{
		public bool init {get; set;}

		public DatabaseConfig()
		{
			ID = 1;
		}

		public DatabaseConfig(bool isset) : this(){
			init = isset;
		}
	}
}

