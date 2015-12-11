using SQLite.Net.Attributes;

namespace TreeWatch
{
    /// <summary>
    /// Base model for all model. ID is the primary Key in the database
    /// </summary>
    public abstract class BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}