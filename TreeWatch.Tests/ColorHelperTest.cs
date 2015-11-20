using NUnit.Framework;
using Xamarin.Forms;


namespace TreeWatch.Tests
{
	[TestFixture ()]
	public class ColorHelperTest
	{
		[Test()]
		public void TestColorToHex()
		{
			Assert.AreEqual (ColorHelper.ToHex (Color.FromHex ("#FF6082B6")), "#FF6082B6");
			Assert.AreEqual (ColorHelper.ToHex (Color.Red), "#FFFF0000");
		}
	}
}

