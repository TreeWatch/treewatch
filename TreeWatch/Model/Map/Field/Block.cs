using System;
using System.Collections.Generic;

using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
    public class Block : PolygonModel, IComparable
    {
        [ForeignKey(typeof(Field))]
        public int FieldId { get; set; }

        [ManyToMany(typeof(BlockToDo))]
        public List<ToDo> ToDos { get; set; }

        [ForeignKey(typeof(TreeType))]
        public int TreeTypeId{ get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.CascadeRead | CascadeOperation.CascadeInsert)]
        public TreeType TreeType { get; set; }

        public Block(List<Position> boundingCoordinates, TreeType treeType)
        {
            BoundingCoordinates = boundingCoordinates;
            TreeType = treeType;
        }

        public Block()
        {
        }

        #region IComparable implementation

        int IComparable.CompareTo(object obj)
        {
            var block = obj as Block;

            return String.Compare(TreeType.Name, block.TreeType.Name, StringComparison.Ordinal);
        }

        #endregion
    }
}