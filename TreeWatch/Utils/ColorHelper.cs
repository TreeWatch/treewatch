// <copyright file="ColorHelper.cs" company="TreeWatch">
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
    using System;

    using Xamarin.Forms;

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
        public static Color GetTreeTypeColor(string type)
        {
            var treeType = int.Parse(type);
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
        /// Converts color into a hex value.
        /// </summary>
        /// <returns>A hex value.</returns>
        /// <param name="color">Color that should be transformed.</param>
        public static string ToHex(Color color)
        {
            var a = (int)(255 * color.A);
            var r = (int)(255 * color.R);
            var g = (int)(255 * color.G);
            var b = (int)(255 * color.B);

            return "#" + a.ToString("X2") + r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
        }

        /// <summary>
        /// Return a random hexcolor.
        /// </summary>
        /// <returns>The color.</returns>
        public static string RandomColor()
        {
            var random = new Random();
            var color = string.Format("#{0:X6}", random.Next(0x1000000));

            return color;
        }
    }
}