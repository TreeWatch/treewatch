using System.Collections.Generic;
using Xamarin.Forms;
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
			var query3 = new DBQuery<Block> (connection);
            var query4 = new DBQuery<Heatmap>(connection);
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
			query3.InsertAllWithChildren (field.Blocks);
			query.InsertWithChildren(field, false);


			treetypes = query2.GetAllWithChildren ();
			field = KMLParser.GetField (KMLParser.LoadFile("KML.Fields.perceelscanHema.kml"));
			field.Name = "Hema";
			field.Blocks = KMLParser.GetBlocks (KMLParser.LoadFile("KML.Blocks.rassenmapHema.kml"), treetypes);
			query3.InsertAllWithChildren (field.Blocks);
			query.InsertWithChildren(field, false);

            /* SQLite on android can not handle this many entitys at once.
             * Therefore we can not add this field, also heatmaps dont work at all in android. 
             */

            if (TargetPlatform.iOS == Device.OS) 
            {
                var heatmap = KMLParser.GetHeatmap(KMLParser.LoadFile("KML.Heatmaps.Biomassa.kml"));
                heatmap.Name = "Biomassa";
                query4.InsertWithChildren(heatmap);

			    treetypes = query2.GetAllWithChildren ();
			    field = KMLParser.GetField (KMLParser.LoadFile("KML.Fields.perceelscanIkea.kml"));
			    field.Name = "Ikea";
			    field.Blocks = KMLParser.GetBlocks (KMLParser.LoadFile("KML.Blocks.rassenmapIkea.kml"), treetypes);
			    query3.InsertAllWithChildren (field.Blocks);
			    query.InsertWithChildren(field, false);
			
            }
		}
	}
}

