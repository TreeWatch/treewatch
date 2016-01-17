// <copyright file="MockDataProvider.cs" company="TreeWatch">
// Copyright © 2015 TreeWatch
// </copyright>
#region Copyright
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
#endregion
namespace TreeWatch
{
    using System.Collections.Generic;

    using Xamarin.Forms;

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