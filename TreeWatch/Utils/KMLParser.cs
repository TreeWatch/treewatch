using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Globalization;

namespace TreeWatch
{
	public static class KMLParser
	{

		public static String RandomColor ()
		{
			var random = new Random ();
			var color = String.Format ("#{0:X6}", random.Next (0x1000000));
			return color;
		}

		public static String NewTreeTypeColor (List<TreeType> treeTypes)
		{
			var colors = new List<String> ();
			foreach (var item in treeTypes) {
				colors.Add (item.TreeColor);
			}

			var color = "";
			do {
				color = RandomColor ();
			} while (colors.Contains (color));

			return color;
		}

		public static List<Block> GetBlocks (string kml, List<TreeType> treeTypes)
		{
			var allTreeTypes = new List<TreeType> ();
			allTreeTypes.AddRange (treeTypes);

			var xml = XDocument.Parse (kml);

			var ns = xml.Root.Name.Namespace;

			var blocks = xml.Descendants (ns + "Placemark");
			var resultList = new List<Block> ();


			foreach (var item in blocks) {
				var resultBlock = new Block ();
				resultBlock.BoundingCoordinates = new List<Position> ();
				var treetypeName = item.Descendants (ns + "SimpleData").FirstOrDefault();

				var treetypeNameString = treetypeName != null ? treetypeName.Value : "NOTDEFINED";

				if (allTreeTypes.All (treeType => treeType.Name != treetypeNameString)) {
					var gayTreeType = new TreeType (treetypeNameString, NewTreeTypeColor (allTreeTypes));
					resultBlock.TreeType = gayTreeType;
					allTreeTypes.Add (gayTreeType);
				} else {
					resultBlock.TreeType = allTreeTypes [allTreeTypes.IndexOf (new TreeType (treetypeNameString))];
				}

				var cords = item.Descendants (ns + "coordinates");

				var listOfCords = cords.First ().Value.Trim().Split (' ');

				foreach (var cord in listOfCords) {
					var longitude = Convert.ToDouble (cord.Split (',') [0], new CultureInfo("en-US"));
					var latitude = Convert.ToDouble (cord.Split (',') [1], new CultureInfo("en-US"));
					var pos = new Position (latitude, longitude);
					resultBlock.BoundingCoordinates.Add (pos);
				}

				resultList.Add (resultBlock);
			}


			return resultList;
		}

		public static Field GetField (string kml)
		{
			var resultField = new Field ();
			resultField.BoundingCoordinates = new List<Position> ();
			var xml = XDocument.Parse (kml);

			var ns = xml.Root.Name.Namespace;

			var cords = xml.Descendants (ns + "coordinates");
		
			var listOfCords = cords.First ().Value.Trim().Split (' ');

			foreach (var item in listOfCords) {
				var longitude = Convert.ToDouble (item.Split (',') [0], new CultureInfo("en-US"));
				var latitude = Convert.ToDouble (item.Split (',') [1], new CultureInfo("en-US"));
				var pos = new Position (latitude, longitude);
				resultField.BoundingCoordinates.Add (pos);
			}


			return resultField;
		}

		public static string LoadFile (string resourcename)
		{
			var assembly = typeof(KMLParser).GetTypeInfo ().Assembly;
			Stream stream = assembly.GetManifestResourceStream ("TreeWatch." + resourcename);
			string text = "";
			using (var reader = new System.IO.StreamReader (stream)) {
				text = reader.ReadToEnd ();
			}

			return text;
		}
	}
}

