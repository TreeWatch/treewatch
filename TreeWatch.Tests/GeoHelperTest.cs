using NUnit.Framework;
using System.Collections.Generic;
using Xamarin.Forms.Maps;


namespace TreeWatch.Tests
{
	[TestFixture ()]
	public class GeoHelperTest
	{
		[Test ()]
		public void TestPositionIsWithinRectangle ()
		{
			var fieldcords = new List<PositionModel>();
			fieldcords.Add (new PositionModel (51.39202, 6.04745));
			fieldcords.Add (new PositionModel (51.39202, 6.05116));
			fieldcords.Add (new PositionModel (51.38972, 6.05116));
			fieldcords.Add (new PositionModel (51.38972, 6.04745));

			var posInsideOne = new PositionModel (51.39143, 6.04817);
			var posInsideTwo = new PositionModel (51.39024, 6.04982);
			var posOutsideOne = new PositionModel (51.39220, 6.04683);
			var posOutsideTwo = new PositionModel (51.38949, 6.05168);

			Assert.IsTrue (GeoHelper.isInsideCoords (fieldcords, posInsideOne));
			Assert.IsTrue (GeoHelper.isInsideCoords (fieldcords, posInsideTwo));
			Assert.IsFalse (GeoHelper.isInsideCoords (fieldcords, posOutsideOne));
			Assert.IsFalse (GeoHelper.isInsideCoords (fieldcords, posOutsideTwo));
		}

		[Test ()]
		public void TestPositionIsWithinCustomField()
		{
			var fieldcords = new List<PositionModel> ();

			fieldcords.Add (new PositionModel (51.39119041516444, 6.049730268624431));
			fieldcords.Add (new PositionModel (51.39106952240859, 6.047530312029856));
			fieldcords.Add (new PositionModel (51.38961006108631, 6.047658976140742));
			fieldcords.Add (new PositionModel (51.39041211982926, 6.050987587115393));
			fieldcords.Add (new PositionModel (51.39196163027054, 6.051018116425213));
			fieldcords.Add (new PositionModel (51.39198165427415, 6.049658554735924));
			fieldcords.Add (new PositionModel (51.39119041516444, 6.049730268624431));

			var posInsideOne = new PositionModel (51.39082462, 6.050752777);
			var posInsideTwo = new PositionModel (51.3904837, 6.04767631);
			var posOutsideOne = new PositionModel (52.39220, 7.04683);
			var posOutsideTwo = new PositionModel (50.38949, 5.05168);
			var posOutsideExtrem = new PositionModel (51.39124639281435, 6.049579664911691);

			Assert.IsTrue (GeoHelper.isInsideCoords (fieldcords, posInsideOne));
			Assert.IsTrue (GeoHelper.isInsideCoords (fieldcords, posInsideTwo));
			Assert.IsFalse (GeoHelper.isInsideCoords (fieldcords, posOutsideOne));
			Assert.IsFalse (GeoHelper.isInsideCoords (fieldcords, posOutsideTwo));
			Assert.IsFalse (GeoHelper.isInsideCoords (fieldcords, posOutsideExtrem));
		}
	}
}

