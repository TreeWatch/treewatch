using SQLite.Net;

namespace TreeWatch
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}