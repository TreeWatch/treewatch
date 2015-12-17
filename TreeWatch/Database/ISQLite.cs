using SQLite.Net;

namespace TreeWatch
{
    /// <summary>
    /// SQLite interface.
    /// </summary>
    public interface ISQLite
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns>The connection.</returns>
        SQLiteConnection GetConnection();
    }
}