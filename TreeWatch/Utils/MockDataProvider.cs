using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

namespace TreeWatch
{
	public static class MockDataProvider
	{

		public static void SetUpMockData ()
		{
			var connection = new TreeWatchDatabase ();
			connection.ClearDataBase ();

			var query = new DBQuery<Field> (connection);

//			query.InsertWithChildren(new Field ("Ajax", new List<Position> (), new List<Block> ()));
//			query.InsertWithChildren(new Field ("PSV", new List<Position> (), new List<Block> ()));
//			query.InsertWithChildren(new Field ("Roda jc", new List<Position> (), new List<Block> ()));
//			query.InsertWithChildren(new Field ("VVV", new List<Position> (), new List<Block> ()));
//			query.InsertWithChildren(new Field ("Hertog Jan", new List<Position> (), new List<Block> ()));
//			query.InsertWithChildren(new Field ("Twente", new List<Position> (), new List<Block> ()));

			var fieldcords = new List<Position> ();
			fieldcords.Add (new Position (51.39202, 6.04745));
			fieldcords.Add (new Position (51.39202, 6.05116));
			fieldcords.Add (new Position (51.38972, 6.05116));
			fieldcords.Add (new Position (51.38972, 6.04745));


			var blocks = new List<Block> ();
			blocks.Add (new Block (new  List<Position> { 
				new Position (51.38972, 6.04745),
				new Position (51.38972, 6.05116),
				new Position (51.39082462477471, 6.050752777777778), 
				new Position (51.3904837408623, 6.047676310228867)
			}, 
				new TreeType ("Apple", "#CC33cc6b")));

			var testfield = new Field ("TestField", fieldcords, blocks);
			query.InsertWithChildren (testfield);
		}
	}
}

