using System;
using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
    /// <summary>
    /// The hours a worker worked on a specifc todo. 
    /// Multiple hours per todo are possible.
    /// </summary>
    public class Hours : BaseModel
    {
        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>The start time.</value>
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>The end time.</value>
        public DateTime EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
        [ForeignKey(typeof(UserToDo))]
        public int UserToDoId
        {
            get;
            set;
        }
    }
}

