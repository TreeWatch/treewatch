// <copyright file="TreeType.cs" company="TreeWatch">
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
    using SQLite.Net.Attributes;
    using Xamarin.Forms;

    /// <summary>
    /// Represents different types of trees on the field.
    /// </summary>
    public class TreeType : BaseModel, IEquatable<TreeType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.TreeType"/> class.
        /// </summary>
        /// <param name="name">Name of the tree type.</param>
        /// <param name="color">Color of the tree type.</param>
        public TreeType(string name, string color)
        {
            this.Name = name;
            this.TreeColor = color;
            this.ColorProp = Color.FromHex(color);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.TreeType"/> class.
        /// With default Color.
        /// </summary>
        /// <param name="name">Name of the tre type.</param>
        public TreeType(string name)
            : this(name, "#00FFFFFFF")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.TreeType"/> class.
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
        public TreeType()
        {
        }

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
            get
            { 
                return Color.FromHex(this.TreeColor);
            } 
            
            set
            { 
                this.TreeColor = ColorHelper.ToHex(value);
            } 
        }

        /// <summary>
        /// Determines whether the specified <see cref="TreeWatch.TreeType"/> is equal to the current <see cref="TreeWatch.TreeType"/>.
        /// </summary>
        /// <param name="other">The <see cref="TreeWatch.TreeType"/> to compare with the current <see cref="TreeWatch.TreeType"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="TreeWatch.TreeType"/> is equal to the current
        /// <see cref="TreeWatch.TreeType"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(TreeType other)
        {
            return string.Compare(this.Name, other.Name, StringComparison.Ordinal) == 0;
        }
    }
}