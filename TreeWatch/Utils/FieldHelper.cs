using System;

namespace TreeWatch
{
	public class FieldHelper
	{
		static readonly FieldHelper instance = new FieldHelper ();

		public static Field SelectedField { get; set; }

		public static Block SelectedBlock { get; set; }

		public static FieldHelper Instance {
			get { return  instance; }
		}

		public event EventHandler<MapTappedEventArgs> MapTapped;

		public event EventHandler<FieldSelectedEventArgs> FieldSelected;

		public event EventHandler<BlockSelectedEventArgs> BlockSelected;

        public event EventHandler<EventArgs> CenterUserPosition;

		public void MapTappedEvent (Position pos, double zoomLevel)
		{
			if (MapTapped != null) {
				MapTapped (this, new MapTappedEventArgs (pos, zoomLevel));
			}
		}

		public void FieldSelectedEvent (Field field)
		{
			if (FieldSelected != null) {
				SelectedField = field;
				FieldSelected (this, new FieldSelectedEventArgs (field));
			}
		}

		public void BlockSelectedEvent (Block block)
		{
			if (BlockSelected != null) {
				SelectedBlock = block;
				BlockSelected (this, new BlockSelectedEventArgs (block));
			}
		}

        public void CenterUserPostionEvent ()
        {
            if (CenterUserPosition != null) {
                CenterUserPosition (this, EventArgs.Empty);
            }
        }
	}
}

