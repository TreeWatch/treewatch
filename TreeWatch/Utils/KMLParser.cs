using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace TreeWatch
{
    /// <summary>
    /// Get Information from KML fiels.
    /// </summary>
    public static class KMLParser
    {
        /// <summary>
        /// Generates a new unique Color.
        /// </summary>
        /// <returns>The tree type color.</returns>
        /// <param name="treeTypes">Tree types.</param>
        public static String NewTreeTypeColor(List<TreeType> treeTypes)
        {
            var colors = new List<String>();
            foreach (var item in treeTypes)
            {
                colors.Add(item.TreeColor);
            }

            string color;
            do
            {
                color = ColorHelper.RandomColor();
            } while (colors.Contains(color));

            return color;
        }

        /// <summary>
        /// Gets blocks from a KML file.
        /// </summary>
        /// <returns>The blocks.</returns>
        /// <param name="kml">Kmlfile</param>
        /// <param name="treeTypes">Existing Tree types</param>
        public static List<Block> GetBlocks(string kml, List<TreeType> treeTypes)
        {
            var allTreeTypes = new List<TreeType>();
            allTreeTypes.AddRange(treeTypes);

            var xml = XDocument.Parse(kml);

            var ns = xml.Root.Name.Namespace;

            var blocks = xml.Descendants(ns + "Placemark");
            var resultList = new List<Block>();

            foreach (var item in blocks)
            {
                var resultBlock = new Block();
                resultBlock.BoundingCoordinates = new List<Position>();
                var treetypeName = item.Descendants(ns + "SimpleData").FirstOrDefault();

                var treetypeNameString = treetypeName != null ? treetypeName.Value : "NOTDEFINED";

                if (allTreeTypes.All(treeType => treeType.Name != treetypeNameString))
                {
                    var gayTreeType = new TreeType(treetypeNameString, NewTreeTypeColor(allTreeTypes));
                    resultBlock.TreeType = gayTreeType;
                    allTreeTypes.Add(gayTreeType);
                }
                else
                {
                    resultBlock.TreeType = allTreeTypes[allTreeTypes.IndexOf(new TreeType(treetypeNameString))];
                }

                var cords = item.Descendants(ns + "coordinates");

                resultBlock.BoundingCoordinates = GetCoordinates(cords);

                resultList.Add(resultBlock);
            }

            return resultList;
        }

        /// <summary>
        /// Gets the field from a KML file.
        /// </summary>
        /// <returns>The field.</returns>
        /// <param name="kml">Kml file.</param>
        public static Field GetField(string kml)
        {
            var resultField = new Field();
            resultField.BoundingCoordinates = new List<Position>();
            var xml = XDocument.Parse(kml);

            var ns = xml.Root.Name.Namespace;

            var cords = xml.Descendants(ns + "coordinates");

            resultField.BoundingCoordinates = GetCoordinates(cords);

            return resultField;
        }

        /// <summary>
        /// Gets the heatmap rom a KML file.
        /// </summary>
        /// <returns>The heatmap.</returns>
        /// <param name="kml">Kml file.</param>
        public static HeatMap GetHeatmap(string kml)
        {
            var xml = XDocument.Parse(kml);
            var ns = xml.Root.Name.Namespace;

            var points = xml.Descendants(ns + "Placemark");
            var heatmap = new HeatMap();
            heatmap.Points = new List<HeatmapPoint>();

            foreach (var item in points)
            {
                var resultPoint = new HeatmapPoint();
                var cords = item.Descendants(ns + "coordinates");

                resultPoint.BoundingCoordinates = GetCoordinates(cords);

                var descendants = item.Descendants(ns + "SimpleData").ToList();
                resultPoint.RowID = Convert.ToDouble(descendants[1].Value, new CultureInfo("en-US"));
                resultPoint.FID = Convert.ToDouble(descendants[2].Value, new CultureInfo("en-US"));
                resultPoint.Mean = Convert.ToDouble(descendants[3].Value, new CultureInfo("en-US"));
                resultPoint.Std = Convert.ToDouble(descendants[4].Value, new CultureInfo("en-US"));

                heatmap.Points.Add(resultPoint);
            }

            return heatmap;
        }

        /// <summary>
        /// Loads the file.
        /// </summary>
        /// <returns>The file.</returns>
        /// <param name="resourcename">Resourcename.</param>
        public static string LoadFile(string resourcename)
        {
            var assembly = typeof(KMLParser).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("TreeWatch." + resourcename);
            string text;
            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }

            return text;
        }

        /// <summary>
        /// Gets the coordinates from XML.
        /// </summary>
        /// <returns>The coordinates.</returns>
        /// <param name="cords">Cords.</param>
        public static List<Position> GetCoordinates(IEnumerable<XElement> cords)
        {
            var listOfCords = cords.First().Value.Trim().Split(' ');
            var posList = new List<Position>();

            foreach (var cord in listOfCords)
            {
                var longitude = Convert.ToDouble(cord.Split(',')[0], new CultureInfo("en-US"));
                var latitude = Convert.ToDouble(cord.Split(',')[1], new CultureInfo("en-US"));
                var pos = new Position(latitude, longitude);
                posList.Add(pos);
            }

            return posList;
        }


    }
}
