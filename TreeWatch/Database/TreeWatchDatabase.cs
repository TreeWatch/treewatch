using SQLite.Net;

using Xamarin.Forms;

namespace TreeWatch
{
    public class TreeWatchDatabase
    {
        public readonly SQLiteConnection Connection;

        public TreeWatchDatabase()
        {
            Connection = DependencyService.Get<ISQLite>().GetConnection();
            Connection.CreateTable<ToDo>();
            Connection.CreateTable<Block>();
            Connection.CreateTable<Field>();
            Connection.CreateTable<User>();
            Connection.CreateTable<Hours>();
            Connection.CreateTable<Note>();
            Connection.CreateTable<UserToDo>();
            Connection.CreateTable<BlockToDo>();
            Connection.CreateTable<TreeType>();
            Connection.CreateTable<DatabaseConfig>();
            Connection.InsertOrIgnore(new DatabaseConfig());
        }

        public void ClearDataBase()
        {
            Connection.DeleteAll<ToDo>();
            Connection.DeleteAll<Block>();
            Connection.DeleteAll<Field>();
            Connection.DeleteAll<User>();
            Connection.DeleteAll<Hours>();
            Connection.DeleteAll<Note>();
            Connection.DeleteAll<UserToDo>();
            Connection.DeleteAll<BlockToDo>();
            Connection.DeleteAll<TreeType>();
            //Connection.Execute ("DROP TABLE initialized");
        }

        public DatabaseConfig DBconfig
        {
            get{ return Connection.Get<DatabaseConfig>(1); }
            set{ Connection.Update(value); }
        }

    }

    public class DatabaseConfig : BaseModel
    {
        public bool Init { get; set; }

        public DatabaseConfig()
        {
            ID = 1;
        }

        public DatabaseConfig(bool isset)
            : this()
        {
            Init = isset;
        }
    }
}
