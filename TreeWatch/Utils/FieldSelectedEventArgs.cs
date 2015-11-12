using System;
using TreeWatch;

namespace TreeWatch
{
	public class FieldSelectedEventArgs : EventArgs
	{
		public FieldSelectedEventArgs (Field field)
		{
			this.field = field;
		}

		private Field field;

		public Field Field
		{
			get{ return field; }
			private set 
			{
				field = value;
			}
		}
	}
}

