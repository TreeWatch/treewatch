// <copyright file="Note.cs" company="TreeWatch">
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
    using SQLiteNetExtensions.Attributes;

    /// <summary>
    /// A Note
    /// </summary>
    public class Note : BaseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.Note"/> class.
        /// </summary>
        /// <param name="title">Title of the note.</param>
        /// <param name="description">Description of the note.</param>
        /// <param name="imagePath">Image path of the note.</param>
        /// <param name="timeStamp">Time stamp of the note.</param>
        /// <param name="position">Position of the note.</param>
        public Note(string title, string description, string imagePath, DateTime timeStamp, Position position)
        {
            this.Title = title;
            this.Description = description;
            this.ImagePath = imagePath;
            this.TimeStamp = timeStamp;
            this.Position = position;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.Note"/> class.
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
        public Note()
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
        /// Gets or sets the image path.
        /// </summary>
        /// <value>The image path.</value>
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the note was created.
        /// </summary>
        /// <value>The time stamp.</value>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        [TextBlob("PositionBlob")]
        public Position Position { get; set; }

        /// <summary>
        /// Gets or sets the position BLOB.
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
        /// <value>The position BLOB.</value>
        public string PositionBlob { get; set; }
    }
}