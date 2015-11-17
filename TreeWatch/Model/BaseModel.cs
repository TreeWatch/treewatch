using SQLite.Net.Attributes;

namespace TreeWatch
{
	public abstract class BaseModel
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
	}
}

