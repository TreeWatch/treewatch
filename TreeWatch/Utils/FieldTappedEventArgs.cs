using System;
using Xamarin.Forms.Maps;

namespace TreeWatch
{
	public class FieldTappedEventArgs : EventArgs
	{
		public FieldTappedEventArgs (Position pos)
		{
			this.Position = pos;
		}

		private Position pos;

		public Position Position 
		{
			get 
			{
				return pos;
			}
			private set
			{
				pos = value;
			}
		}
	}
}

