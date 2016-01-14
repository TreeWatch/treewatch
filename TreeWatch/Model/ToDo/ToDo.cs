// <copyright file="ToDo.cs" company="TreeWatch">
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
    /// To do.
    /// </summary>
    public class ToDo : BaseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.ToDo"/> class.
        /// </summary>
        /// <param name="title">Title of the todo.</param>
        /// <param name="description">Description of the todo.</param>
        public ToDo(string title, string description)
        {
            this.Description = description;
            this.Title = title;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.ToDo"/> class.
        /// </summary>
        public ToDo()
        {
        }

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
    }
}