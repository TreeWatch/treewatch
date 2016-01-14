
using System;
using NUnit.Framework;
using TreeWatch.iOS;
using System.Collections.Generic;

namespace TreeWatch.iOS.Tests
{
    [TestFixture]
    public class FieldMapRenderTests
    {

        [Test]
        public void testConverCoordinates()
        {
            var fieldcoords = new List<Position>();

            fieldcoords.Add(new Position(51.39202, 6.04745));
            fieldcoords.Add(new Position(51.39202, 6.05116));
            fieldcoords.Add(new Position(51.38972, 6.05116));
            fieldcoords.Add(new Position(51.38972, 6.04745));

            var result = FieldMapRenderer.ConvertCoordinates(fieldcoords);

            for (int i = 0; i < result.Length-1 ; i++)
            {
                Assert.False(!((result[i].Latitude == fieldcoords[i].Latitude) 
                    && (result[i].Longitude == fieldcoords[i].Longitude)));
            }
        }


    }
}
