using System;
using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
	public class Hours : BaseModel
	{
		public DateTime StartTime {
			get;
			set;
		}
		public DateTime EndTime {
			get;
			set;
		}

		[ForeignKey (typeof(UserToDo))]
		public int UserToDoId {
			get;
			set;
		}

		public Hours ()
		{
		}
	}
}

