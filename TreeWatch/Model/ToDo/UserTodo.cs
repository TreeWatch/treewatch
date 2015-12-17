using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
    /// <summary>
    /// Represents the relation betwen users and todos.
    /// One Todo can be assigned to multiple Users and the other way around.
    /// </summary>
    public class UserToDo
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        [ForeignKey(typeof(User))]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets to do ID.
        /// </summary>
        /// <value>To do identifier.</value>
        [ForeignKey(typeof(ToDo))]
        public int ToDoId{ get; set; }

        /// <summary>
        /// Gets or sets the hours.
        /// </summary>
        /// <value>The hours.</value>
        [OneToMany(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
        public List<Hours> Hours{ get; set; }

    }
}