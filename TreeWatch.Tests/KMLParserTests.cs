// <copyright file="GeoHelperTest.cs" company="TreeWatch">
// Copyright © 2015 TreeWatch
// </copyright>
using System;


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
namespace TreeWatch.Tests
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using System.Linq;


    /// <summary>
    /// KMLParser tests.
    /// </summary>
    [TestFixture]
    public class KMLParserTests
    {

        /// <summary>
        /// Tests the position is within rectangle.
        /// </summary>
        [Test]
        public void TestGetField()
        {
            var kml = "<kml" +
                "xmlns=\"http://earth.google.com/kml/2.1\">" +
                "<Folder>" +
                "<name />" +
                "<Placemark>" +
                "<name>Grutto</name>" +
                "<MultiGeometry>" +
                "<Polygon>" +
                "<outerBoundaryIs>" +
                "<LinearRing>" +
                "<coordinates>6.0476651,51.3896597,1 6.0473710,51.3918162,1 6.0495220,51.3919244,1 6.0495271,51.3920493,1 6.0509313,51.3920026,1 6.0510568,51.3919528,1 6.0512439,51.3919579,1 6.0511644,51.3905310,1 6.0476651,51.3896597,1</coordinates>" +
            "</LinearRing>" +
            "</outerBoundaryIs>" +
            "</Polygon>" +
            "</MultiGeometry>" +
            "</Placemark>" +
           "</Folder>" +
           "</kml>";
            
            var coords = new List<Position>{
                new Position(51.3896597, 6.0476651),
                new Position(51.3918162, 6.0473710),
                new Position(51.3919244, 6.0495220),
                new Position(51.3920493, 6.0495271),
                new Position(51.3920026, 6.0509313),
                new Position(51.3919528, 6.0510568),
                new Position(51.3919579, 6.0512439),
                new Position(51.3905310, 6.0511644),
                new Position(51.3896597, 6.0476651)
            };
            
            var field = KMLParser.GetField(kml);
            Assert.AreEqual(field.Name, "Grutto");
            Assert.AreEqual(field.BoundingCoordinates, coords);
        }


    }
}