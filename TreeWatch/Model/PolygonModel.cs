using SQLiteNetExtensions.Attributes;
using Xamarin.Forms.Maps;
using System.Collections.Generic;

namespace TreeWatch
{
	public abstract class PolygonModel : BaseModel
	{
		[TextBlob("BoundingCordiantesBlob")]
		public List<Position> BoundingCordinates { get; set; }

		public string BoundingCordiantesBlob { get; set; }
	}
}

