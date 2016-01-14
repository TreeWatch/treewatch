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

    /// <summary>
    /// Geo helper test.
    /// </summary>
    [TestFixture]
    public class GeoHelperTest
    {
        /// <summary>
        /// Tests the position is within rectangle.
        /// </summary>
        [Test]
        public void TestPositionIsWithinRectangle()
        {
            var fieldcoords = new List<Position>();

            fieldcoords.Add(new Position(51.39202, 6.04745));
            fieldcoords.Add(new Position(51.39202, 6.05116));
            fieldcoords.Add(new Position(51.38972, 6.05116));
            fieldcoords.Add(new Position(51.38972, 6.04745));

            var posInsideOne = new Position(51.39143, 6.04817);
            var posInsideTwo = new Position(51.39024, 6.04982);
            var posOutsideOne = new Position(51.39220, 6.04683);
            var posOutsideTwo = new Position(51.38949, 6.05168);

            Assert.IsTrue(GeoHelper.IsInsideCoords(fieldcoords, posInsideOne));
            Assert.IsTrue(GeoHelper.IsInsideCoords(fieldcoords, posInsideTwo));
            Assert.IsFalse(GeoHelper.IsInsideCoords(fieldcoords, posOutsideOne));
            Assert.IsFalse(GeoHelper.IsInsideCoords(fieldcoords, posOutsideTwo));
        }

        /// <summary>
        /// Tests the position is within custom field.
        /// </summary>
        [Test]
        public void TestPositionIsWithinCustomField()
        {
            var fieldcoords = new List<Position>();

            fieldcoords.Add(new Position(51.39119041516444, 6.049730268624431));
            fieldcoords.Add(new Position(51.39106952240859, 6.047530312029856));
            fieldcoords.Add(new Position(51.38961006108631, 6.047658976140742));
            fieldcoords.Add(new Position(51.39041211982926, 6.050987587115393));
            fieldcoords.Add(new Position(51.39196163027054, 6.051018116425213));
            fieldcoords.Add(new Position(51.39198165427415, 6.049658554735924));
            fieldcoords.Add(new Position(51.39119041516444, 6.049730268624431));

            var posInsideOne = new Position(51.39082462, 6.050752777);
            var posInsideTwo = new Position(51.3904837, 6.04767631);
            var posOutsideOne = new Position(52.39220, 7.04683);
            var posOutsideTwo = new Position(50.38949, 5.05168);
            var posOutsideExtrem = new Position(51.39124639281435, 6.049579664911691);

            Assert.IsTrue(GeoHelper.IsInsideCoords(fieldcoords, posInsideOne));
            Assert.IsTrue(GeoHelper.IsInsideCoords(fieldcoords, posInsideTwo));
            Assert.IsFalse(GeoHelper.IsInsideCoords(fieldcoords, posOutsideOne));
            Assert.IsFalse(GeoHelper.IsInsideCoords(fieldcoords, posOutsideTwo));
            Assert.IsFalse(GeoHelper.IsInsideCoords(fieldcoords, posOutsideExtrem));
        }

        /// <summary>
        /// Tests the DistanceInMeters between two points.
        /// </summary>
        [Test]
        public void TestDistanceInMeters()
        {
            var positionOne = new Position(51.39082462, 6.050752777);
            var positionTwo = new Position(51.3904837, 6.04767631);


            Assert.AreEqual(GeoHelper.DistanceInMeters(positionOne.Latitude, positionOne.Longitude, 
                positionTwo.Latitude, positionTwo.Longitude), 216.8);
            
            Assert.AreEqual(GeoHelper.DistanceInMeters(positionTwo.Latitude, positionTwo.Longitude, 
                positionOne.Latitude, positionOne.Longitude), 216.8);
            
            Assert.AreEqual(GeoHelper.DistanceInMeters(positionOne.Latitude, positionOne.Longitude, 
                positionTwo.Latitude, positionTwo.Longitude),
                GeoHelper.DistanceInMeters(positionTwo.Latitude, positionTwo.Longitude, 
                    positionOne.Latitude, positionOne.Longitude));
        }

        /// <summary>
        /// Tests convertion to radians.
        /// </summary>
        [Test]
        public void TestToRadians()
        {
            Assert.AreEqual(Math.Round(GeoHelper.ToRadians(10.0), 10), 1.5707963268);
            Assert.AreEqual(Math.Round(GeoHelper.ToRadians(180.0), 10), 3.1415926536);
            Assert.AreEqual(Math.Round(GeoHelper.ToRadians(-90.0), 10), -1.5707963268);
        }
    }
}