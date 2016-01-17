// <copyright file="Block.cs" company="TreeWatch">
// Copyright © 2015 TreeWatch
// </copyright>
#region Copyright
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
#endregion
namespace TreeWatch
{
    using System;
    using System.Collections.Generic;
    using SQLiteNetExtensions.Attributes;

    /// <summary>
    /// Model for a block in a Field.
    /// A block corrensponds to 6 rows in a field.
    /// </summary>
    public class Block : PolygonModel, IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.Block"/> class.
        /// </summary>
        /// <param name="boundingCoordinates">Bounding coordinates of the block.</param>
        /// <param name="treeType">TreeType of the block.</param>
        public Block(List<Position> boundingCoordinates, TreeType treeType)
        {
            this.BoundingCoordinates = boundingCoordinates;
            this.TreeType = treeType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.Block"/> class.
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
        public Block()
        {
        }

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
        /// Gets or sets the tree type identifier.
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
        /// <value>The tree type identifier.</value>
        [ForeignKey(typeof(TreeType))]
        public int TreeTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the tree.
        /// </summary>
        /// <value>The type of the tree.</value>
        [OneToOne(CascadeOperations = CascadeOperation.CascadeRead | CascadeOperation.CascadeInsert)]
        public TreeType TreeType { get; set; }

        #region IComparable implementation

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <returns>The to.</returns>
        /// <param name="obj">Object that should be compared.</param>
        int IComparable.CompareTo(object obj)
        {
            var block = obj as Block;

            return string.Compare(TreeType.Name, block.TreeType.Name, StringComparison.Ordinal);
        }

        #endregion
    }
}