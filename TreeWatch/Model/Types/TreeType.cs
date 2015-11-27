using Xamarin.Forms;
using SQLite.Net.Attributes;


namespace TreeWatch
{
	public class TreeType : BaseModel
	{
		public string Name { get; set; }

		public string TreeColor { get; set; }

		[Ignore]
		public Color ColorProp{ get { return Color.FromHex (TreeColor); } set{ TreeColor = ColorHelper.ToHex (value); } }

		public TreeType()
		{
		}

		public TreeType(string name, string color)
		{
			Name = name;
			TreeColor = color;
			ColorProp = Color.FromHex (color);

		}
	}

}

