using System;

namespace TreeWatch
{
	public class FieldTappedEventArgs : EventArgs
	{
		public FieldTappedEventArgs (Position pos)
		{
			Position = pos;
		}

		public Position Position {
			get;
			private set;
		}
	}
}

