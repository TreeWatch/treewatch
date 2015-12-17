using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
    /// <summary>
    /// DatabaseHelperclass for mapping Todos to Blocks
    /// </summary>
    public class BlockToDo
    {
        
        /// <summary>
        /// Gets or sets the block identifier.
        /// </summary>
        /// <value>The block identifier.</value>
        [ForeignKey(typeof(Block))]
        public int BlockId { get; set; }

        /// <summary>
        /// Gets or sets todo identifier.
        /// </summary>
        /// <value>To do identifier.</value>
        [ForeignKey(typeof(ToDo))]
        public int ToDoId { get; set; }
    }
}