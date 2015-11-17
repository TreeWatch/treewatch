using SQLiteNetExtensions.Attributes;
using Xamarin.Forms.Maps;
using System.Collections.Generic;

namespace TreeWatch
{
	public abstract class PolygonModel : BaseModel
	{
		[TextBlob("BoundingCoordiantesBlob")]
		public List<Position> BoundingCoordinates { get; set; }

		public string BoundingCoordiantesBlob { get; set; }
	}
}

