using System;
using System.Collections.Generic;
using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
    /// <summary>
    /// A Field.
    /// </summary>
    public class Field : PolygonModel
    {
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
        public String Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.Field"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="boundingCoordinates">Bounding coordinates.</param>
        /// <param name="blocks">Blocks.</param>
        public Field(string name, List<Position> boundingCoordinates, List<Block> blocks)
        {
            this.Name = name;
            this.BoundingCoordinates = boundingCoordinates;
            this.Blocks = blocks;
        }
        /// <summary>
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
        public Field()
        {
        }
    }
}