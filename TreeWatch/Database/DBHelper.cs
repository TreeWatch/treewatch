using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
    public class BlockToDo
    {
        [ForeignKey(typeof(Block))]
        public int BlockId { get; set; }

        [ForeignKey(typeof(ToDo))]
        public int ToDoId { get; set; }
    }
}