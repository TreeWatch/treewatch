using System;
using Xamarin.Forms;
using System.Diagnostics;

namespace TreeWatch
{
	public static class ColorHelper
	{

		public static Color GetTreeTypeColor (String type)
		{
			var treeType = (Int32.Parse(type));
			return ColorHelper.GetTreeTypeColor (treeType);
		}

		public static Color GetTreeTypeColor(int ID){
			var connection = new TreeWatchDatabase ();
			TreeType type = new DBQuery<TreeType> (connection).GetByID (ID);
			return type.ColorProp;
		}

		public static string ToHex(Color color)
		{
			var A = (int)(255 * color.A);
			var R = (int)(255 * color.R);
			var G = (int)(255 * color.G);
			var B = (int)(255 * color.B);

			return "#" + A.ToString ("X2") + R.ToString ("X2") + G.ToString ("X2") + B.ToString ("X2");
		}
	}
}

