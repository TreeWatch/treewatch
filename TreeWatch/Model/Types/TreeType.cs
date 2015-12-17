using System;
using SQLite.Net.Attributes;
using Xamarin.Forms;


namespace TreeWatch
{
    public class TreeType : BaseModel, IEquatable<TreeType>
    {
        public string Name { get; set; }

        public string TreeColor { get; set; }

        [Ignore]
        public Color ColorProp{ get { return Color.FromHex(TreeColor); } set { TreeColor = ColorHelper.ToHex(value); } }

        public TreeType()
        {
        }

        public TreeType(string name, string color)
        {
            Name = name;
            TreeColor = color;
            ColorProp = Color.FromHex(color);

        }

        public TreeType(string name)
            : this(name, "#00FFFFFFF")
        {
        }


        public bool Equals(TreeType other)
        {
            return Name == other.Name;
        }
    }

}

