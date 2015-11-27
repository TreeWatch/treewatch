using System;

namespace TreeWatch
{
	public class MapTappedEventArgs : EventArgs
	{
		public MapTappedEventArgs (Position pos, double zoomLevel)
		{
			Position = pos;
			Zoomlevel = zoomLevel;
		}

		public Position Position { get; private set; }

		public Double Zoomlevel{ get; private set; }
	}
}

