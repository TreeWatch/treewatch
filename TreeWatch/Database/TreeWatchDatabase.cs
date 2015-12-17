using SQLite.Net;

using Xamarin.Forms;

namespace TreeWatch
{
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
        /// Initializes a new instance of the <see cref="TreeWatch.TreeWatchDatabase"/>class.
        /// </summary>
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

        /// <summary>
        /// Clears the database.
        /// </summary>
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
        }

        /// <summary>
        /// Gets or sets the DBconfig.
        /// </summary>
        /// <value>The DBconfig.</value>
        public DatabaseConfig DBconfig
        {
            get{ return Connection.Get<DatabaseConfig>(1); }
            set{ Connection.Update(value); }
        }
    }

    /// <summary>
    /// Database config keeps information about the database.
    /// </summary>
    public class DatabaseConfig : BaseModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TreeWatch.DatabaseConfig"/> is initialized.
        /// </summary>
        /// <value><c>true</c> if init; otherwise, <c>false</c>.</value>
        public bool Init { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.DatabaseConfig"/> class.
        /// </summary>
        public DatabaseConfig()
        {
            ID = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.DatabaseConfig"/> class.
        /// </summary>
        /// <param name="isset">If set to <c>true</c> database is already initialized.</param>
        public DatabaseConfig(bool isset)
            : this()
        {
            Init = isset;
        }
    }
}
