// <copyright file="Position.cs" company="TreeWatch">
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
    /// <summary>
    /// Class that has a latitude and longitude, stored as Doubles.
    /// Hides Xamarin.Forms.Maps.Position, since that can't be correctly serialized.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.Position"/> class.
        /// Only used for SQLite. Should not be called in any way outside of SQLite methods.
        /// </summary>
        public Position()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.Position"/> class.
        /// </summary>
        /// <param name="latitude">Latitude on the map.</param>
        /// <param name="longitude">Longitude on the map.</param>
        public Position(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude { get; set; }

        /// <summary>
        /// Convert Position to Xamarin.Forms.Maps.Position
        /// </summary>
        /// <param name="pos">Position of this class.</param>
        public static implicit operator Xamarin.Forms.Maps.Position(Position pos)
        {
            return new Xamarin.Forms.Maps.Position(pos.Latitude, pos.Longitude);
        }

        /// <summary>
        /// Convert Xamarin.Forms.Maps.Postion to Position
        /// </summary>
        /// <param name="pos">Position of xamarin forms maps.</param>
        public static implicit operator Position(Xamarin.Forms.Maps.Position pos)
        {
            return new Position(pos.Latitude, pos.Longitude);
        }
    }
}