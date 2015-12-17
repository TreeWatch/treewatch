using System;
using SQLite.Net.Attributes;
using Xamarin.Forms;


namespace TreeWatch
{
    /// <summary>
    /// Represents different types of trees on the field.
    /// </summary>
    public class TreeType : BaseModel, IEquatable<TreeType>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the color of the tree as HEX value.
        /// </summary>
        /// <value>The color of the tree.</value>
        public string TreeColor { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color property.</value>
        [Ignore]
        public Color ColorProp
        {
            get { return Color.FromHex(TreeColor); } 
            set { TreeColor = ColorHelper.ToHex(value); } 
        }

        /// <summary>
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
        public TreeType()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.TreeType"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="color">Color.</param>
        public TreeType(string name, string color)
        {
            Name = name;
            TreeColor = color;
            ColorProp = Color.FromHex(color);

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.TreeType"/> class.
        /// With default Color.
        /// </summary>
        /// <param name="name">Name.</param>
        public TreeType(string name)
            : this(name, "#00FFFFFFF")
        {
        }

        /// <summary>
        /// Determines whether the specified <see cref="TreeWatch.TreeType"/> is equal to the current <see cref="TreeWatch.TreeType"/>.
        /// </summary>
        /// <param name="other">The <see cref="TreeWatch.TreeType"/> to compare with the current <see cref="TreeWatch.TreeType"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="TreeWatch.TreeType"/> is equal to the current
        /// <see cref="TreeWatch.TreeType"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(TreeType other)
        {
            return Name == other.Name;
        }
    }

}

