using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace TreeWatch
{
	/// <summary>
	/// Base model for models that are represented as a polygon.
	/// </summary>
	public abstract class PolygonModel : BaseModel
	{
		/// <summary>
		/// Gets or sets the bounding cordinates of the polygon.
		/// </summary>
		/// <value>The bounding cordinates.</value>
		[TextBlob("BoundingCoordiantesBlob")]
		public List<Position> BoundingCoordinates { get; set; }

		/// <summary>
		/// Gets or sets the bounding cordiantes BLOB.
		/// <remarks>
		/// Should only be used by SQLite.
		/// </remarks> 
		/// </summary>
		/// <value>The bounding cordiantes serialized as BLOB.</value>
		public string BoundingCoordiantesBlob { get; set; }
	}
}

