// <copyright file="Field.cs" company="TreeWatch">
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
    using System.Collections.Generic;
    using SQLiteNetExtensions.Attributes;

    /// <summary>
    /// A Field.
    /// </summary>
    public class Field : PolygonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.Field"/> class.
        /// </summary>
        /// <param name="name">Name of the field.</param>
        /// <param name="boundingCoordinates">Bounding coordinates of the field.</param>
        /// <param name="blocks">Blocks of the field.</param>
        public Field(string name, List<Position> boundingCoordinates, List<Block> blocks)
        {
            this.Name = name;
            this.BoundingCoordinates = boundingCoordinates;
            this.Blocks = blocks;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.Field"/> class.
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
        public Field()
        {
        }

        /// <summary>
        /// Gets or sets the blocks.
        /// </summary>
        /// <value>The blocks.</value>
        [OneToMany(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
        public List<Block> Blocks { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
    }
}