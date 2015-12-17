using System;
using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
    /// <summary>
    /// A Note
    /// </summary>
    public class Note : BaseModel
    {
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

        [TextBlob("PositionBlob")]
        public Position Position { get; set; }

        /// <summary>
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
        public String PositionBlob { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.Note"/> class.
        /// </summary>
        /// <param name="title">Title.</param>
        /// <param name="description">Description.</param>
        /// <param name="imagePath">Image path.</param>
        /// <param name="timeStamp">Time stamp.</param>
        /// <param name="position">Position.</param>
        public Note(string title, string description, string imagePath, DateTime timeStamp, Position position)
        {
            Title = title;
            Description = description;
            ImagePath = imagePath;
            TimeStamp = timeStamp;
            Position = position;
        }

        /// <summary>
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
        public Note()
        {
        }
    }
}

