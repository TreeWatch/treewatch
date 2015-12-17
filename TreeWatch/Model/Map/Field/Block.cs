using System;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
    /// <summary>
    /// Model for a block in a Field.
    /// A block corrensponds to 6 rows in a field.
    /// </summary>
    public class Block : PolygonModel, IComparable
    {
        /// <summary>
        /// Gets or sets the field ID.
        /// </summary>
        /// <value>The field identifier.</value>
        [ForeignKey(typeof(Field))]
        public int FieldId { get; set; }

        /// <summary>
        /// Gets or sets the List of Todos.
        /// </summary>
        /// <value>To dos.</value>
        [ManyToMany(typeof(BlockToDo))]
        public List<ToDo> ToDos { get; set; }

        /// <summary>
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
        [ForeignKey(typeof(TreeType))]
        public int TreeTypeId{ get; set; }

        /// <summary>
        /// Gets or sets the type of the tree.
        /// </summary>
        /// <value>The type of the tree.</value>
        [OneToOne(CascadeOperations = CascadeOperation.CascadeRead | CascadeOperation.CascadeInsert)]
        public TreeType TreeType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.Block"/> class.
        /// </summary>
        /// <param name="boundingCoordinates">Bounding coordinates.</param>
        /// <param name="treeType">TreeType.</param>
        public Block(List<Position> boundingCoordinates, TreeType treeType)
        {
            BoundingCoordinates = boundingCoordinates;
            TreeType = treeType;
        }

        /// <summary>
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
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