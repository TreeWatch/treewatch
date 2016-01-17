using System;
using System.Collections.Generic;

namespace TreeWatch
{
	public class Fields : List<Field>
	{
		public Fields ()
		{
			var loc = new List<Field> () 
			{
				new Field ("Ajax"),
				new Field ("PSV"),
				new Field ("Roda jc"),
				new Field ("VVV"),
				new Field ("Hertog Jan"),
				new Field ("Twente")
			};
			this.AddRange (loc);
		}
	}
}

