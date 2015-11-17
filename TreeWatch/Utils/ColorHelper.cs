using System;
using Xamarin.Forms;

namespace TreeWatch
{
	public static class ColorHelper
	{
		/// <summary>
		/// Gets the color of the tree type.
		/// </summary>
		/// <returns>The tree type color.</returns>
		/// <param name="type">TreeType</param>
		public static Color GetTreeTypeColor (TreeType type)
		{
			switch (type) {
			case TreeType.APPLE:
				return Color.Lime;
			case TreeType.CHERRY:
				return Color.Red;
			case TreeType.PEAR:
				return Color.Maroon;
			case TreeType.PLUM:
				return Color.Purple;
			case TreeType.NOTDEFINED:
				return Color.Black;
			default:
				return Color.Blue;
			}
		}

		/// <summary>
		/// Gets the color of the TreeType represented as Integer in a String.
		/// </summary>
		/// <returns>The TreeType color.</returns>
		/// <param name="type">TreeType</param>
		public static Color GetTreeTypeColor (String type)
		{
			var treeType = (TreeType)(Int32.Parse (type));
			return ColorHelper.GetTreeTypeColor (treeType);
		}
	}
}

