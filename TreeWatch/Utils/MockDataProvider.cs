using System.Collections.Generic;
using Xamarin.Forms;

namespace TreeWatch
{
    /// <summary>
    /// Generates example data for the app.
    /// </summary>
    public static class MockDataProvider
    {
        /// <summary>
        /// Sets up mock data.
        /// </summary>
        public static void SetUpMockData()
        {
            var connection = new TreeWatchDatabase();
            connection.ClearDataBase();

            var fieldQuery = new DBQuery<Field>(connection);
            var treetypeQuery = new DBQuery<TreeType>(connection);
            var blockQuery = new DBQuery<Block>(connection);
            var heatmapQuery = new DBQuery<HeatMap>(connection);
            var treetypes = treetypeQuery.GetAllWithChildren();

            var field = KMLParser.GetField(KMLParser.LoadFile("KML.Fields.perceelscanKarwei.kml"));
            field.Name = "Karwei";
            fieldQuery.InsertWithChildren(field);

            field = KMLParser.GetField(KMLParser.LoadFile("KML.Fields.perceelscanPraxis.kml"));
            field.Name = "Praxis";
            fieldQuery.InsertWithChildren(field);

            field = KMLParser.GetField(KMLParser.LoadFile("KML.Fields.perceelscanSligro.kml"));
            field.Name = "Sligro";
            fieldQuery.InsertWithChildren(field);


            field = KMLParser.GetField(KMLParser.LoadFile("KML.Fields.perceelscanGrutto.kml"));
            field.Name = "Grutto";
            field.Blocks = KMLParser.GetBlocks(KMLParser.LoadFile("KML.Blocks.rassenmapGrutto.kml"), treetypes);
            blockQuery.InsertAllWithChildren(field.Blocks);
            fieldQuery.InsertWithChildren(field, false);


            treetypes = treetypeQuery.GetAllWithChildren();
            field = KMLParser.GetField(KMLParser.LoadFile("KML.Fields.perceelscanHema.kml"));
            field.Name = "Hema";
            field.Blocks = KMLParser.GetBlocks(KMLParser.LoadFile("KML.Blocks.rassenmapHema.kml"), treetypes);
            blockQuery.InsertAllWithChildren(field.Blocks);
            fieldQuery.InsertWithChildren(field, false);

            /* SQLite on android can not handle this many entitys at once.
             * Therefore we can not add this field, also heatmaps dont work at all in android.
             */

            if (TargetPlatform.iOS == Device.OS)
            {
                var heatmap = KMLParser.GetHeatmap(KMLParser.LoadFile("KML.Heatmaps.Biomassa.kml"));
                heatmap.Name = "Biomassa";
                heatmapQuery.InsertWithChildren(heatmap);

                treetypes = treetypeQuery.GetAllWithChildren();
                field = KMLParser.GetField(KMLParser.LoadFile("KML.Fields.perceelscanIkea.kml"));
                field.Name = "Ikea";
                field.Blocks = KMLParser.GetBlocks(KMLParser.LoadFile("KML.Blocks.rassenmapIkea.kml"), treetypes);
                blockQuery.InsertAllWithChildren(field.Blocks);
                fieldQuery.InsertWithChildren(field, false);

            }
        }
    }
}
