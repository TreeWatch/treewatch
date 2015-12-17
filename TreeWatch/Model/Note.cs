using System;
using SQLiteNetExtensions.Attributes;

namespace TreeWatch
{
    public class Note : BaseModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public DateTime TimeStamp { get; set; }

        [TextBlob("PositionBlob")]
        public Position Position { get; set; }

        /// <summary>
        /// Gets or sets the position BLOB.
        /// </summary>
        /// <remarks>
        /// Should only be used by SQLite.
        /// </remarks> 
        /// <value>The position serialized as BLOB.</value>
        public String PositionBlob { get; set; }

        public Note(string title, string description, string imagePath, DateTime timeStamp, Position position)
        {
            Title = title;
            Description = description;
            ImagePath = imagePath;
            TimeStamp = timeStamp;
            Position = position;
        }

        public Note()
        {
        }
    }
}

