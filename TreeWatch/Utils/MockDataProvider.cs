using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace TreeWatch
{
	public static class MockDataProvider
	{

		public static void SetUpMockData ()
		{
			var connection = new TreeWatchDatabase ();
			connection.ClearDataBase ();

			var query = new DBQuery<Field> (connection);
			var query2 = new DBQuery<TreeType> (connection);
			var treetypes = query2.GetAllWithChildren ();

			var field = KMLParser.GetField (KMLParser.LoadFile("KML.Fields.perceelscanKarwei.kml"));
			field.Name = "Karwei";
			query.InsertWithChildren (field);

			field = KMLParser.GetField (KMLParser.LoadFile("KML.Fields.perceelscanPraxis.kml"));
			field.Name = "Praxis";
			query.InsertWithChildren (field);

			field = KMLParser.GetField (KMLParser.LoadFile("KML.Fields.perceelscanSligro.kml"));
			field.Name = "Sligro";
			query.InsertWithChildren (field);


			field = KMLParser.GetField (KMLParser.LoadFile("KML.Fields.perceelscanGrutto.kml"));
			field.Name = "Grutto";
			field.Blocks = KMLParser.GetBlocks (KMLParser.LoadFile("KML.Blocks.rassenmapGrutto.kml"), treetypes);
			query.InsertWithChildren(field);


			treetypes = query2.GetAllWithChildren ();
			field = KMLParser.GetField (KMLParser.LoadFile("KML.Fields.perceelscanHema.kml"));
			field.Name = "Hema";
			field.Blocks = KMLParser.GetBlocks (KMLParser.LoadFile("KML.Blocks.rassenmapHema.kml"), treetypes);
			query.InsertWithChildren(field);

			treetypes = query2.GetAllWithChildren ();
			field = KMLParser.GetField (KMLParser.LoadFile("KML.Fields.perceelscanIkea.kml"));
			field.Name = "Ikea";
			field.Blocks = KMLParser.GetBlocks (KMLParser.LoadFile("KML.Blocks.rassenmapIkea.kml"), treetypes);
			query.InsertWithChildren(field);

		}
	}
}

