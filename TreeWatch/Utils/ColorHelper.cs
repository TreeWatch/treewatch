using System;
using Xamarin.Forms;

namespace TreeWatch
{
    /// <summary>
    /// Helper class for color conversions.
    /// </summary>
    public static class ColorHelper
    {
        /// <summary>
        /// Gets the color of the treetype.
        /// </summary>
        /// <returns>The tree type color.</returns>
        /// <param name="type">Treetype as String.</param>
        public static Color GetTreeTypeColor(String type)
        {
            var treeType = (Int32.Parse(type));
            return ColorHelper.GetTreeTypeColor(treeType);
        }

        /// <summary>
        /// Gets the color of the tree type.
        /// </summary>
        /// <returns>The tree type color.</returns>
        /// <param name="id">TreeType ID</param>
        public static Color GetTreeTypeColor(int id)
        {
            var connection = new TreeWatchDatabase();
            TreeType type = new DBQuery<TreeType>(connection).GetByID(id);
            return type.ColorProp;
        }

        /// <summary>
        /// Convenerts color the hex value.
        /// </summary>
        /// <returns>The hex.</returns>
        /// <param name="color">Color.</param>
        public static string ToHex(Color color)
        {
            var A = (int)(255 * color.A);
            var R = (int)(255 * color.R);
            var G = (int)(255 * color.G);
            var B = (int)(255 * color.B);

            return "#" + A.ToString("X2") + R.ToString("X2") + G.ToString("X2") + B.ToString("X2");
        }

        /// <summary>
        /// Return a random hexcolor.
        /// </summary>
        /// <returns>The color.</returns>
        public static String RandomColor()
        {
            var random = new Random();
            var color = String.Format("#{0:X6}", random.Next(0x1000000));
            return color;
        }
    }
}