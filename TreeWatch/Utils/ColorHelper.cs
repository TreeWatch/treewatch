using System;
using Xamarin.Forms;

namespace TreeWatch
{
    public static class ColorHelper
    {
        public static Color GetTreeTypeColor(String type)
        {
            var treeType = (Int32.Parse(type));
            return ColorHelper.GetTreeTypeColor(treeType);
        }

        public static Color GetTreeTypeColor(int id)
        {
            var connection = new TreeWatchDatabase();
            TreeType type = new DBQuery<TreeType>(connection).GetByID(id);
            return type.ColorProp;
        }

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