using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
    /// <summary>
    /// To do.
    /// </summary>
    public class ToDo : BaseModel
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the blocks this todo belongs to.
        /// </summary>
        /// <value>The blocks.</value>
        [ManyToMany(typeof(BlockToDo))]
        public List<Block> Blocks { get; set; }

        /// <summary>
        /// Gets or sets todos.
        /// </summary>
        /// <value>To dos.</value>
        [OneToMany(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
        public List<UserToDo> ToDos
        {
            get;
            set;
        }

        public ToDo(string title, string description)
        {
            Description = description;
            Title = title;
        }

        public ToDo()
        {
        }
    }
}