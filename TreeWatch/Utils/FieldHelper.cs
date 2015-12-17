using System;

namespace TreeWatch
{
    /// <summary>
    /// Helper for Field related stuff, like events and global caching of
    /// selected fields and blocks
    /// </summary>
    public class FieldHelper
    {
        static readonly FieldHelper instance = new FieldHelper();

        /// <summary>
        /// Gets or sets the selected field.
        /// </summary>
        /// <value>The selected field.</value>
        public static Field SelectedField { get; set; }

        /// <summary>
        /// Gets or sets the selected block.
        /// </summary>
        /// <value>The selected block.</value>
        public static Block SelectedBlock { get; set; }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static FieldHelper Instance
        {
            get { return  instance; }
        }

        /// <summary>
        /// Occurs when map tapped.
        /// </summary>
        public event EventHandler<MapTappedEventArgs> MapTapped;

        /// <summary>
        /// Occurs when field is selected.
        /// </summary>
        public event EventHandler<FieldSelectedEventArgs> FieldSelected;

        /// <summary>
        /// Occurs when block is selected.
        /// </summary>
        public event EventHandler<BlockSelectedEventArgs> BlockSelected;

        /// <summary>
        /// Occurs when user position is centered.
        /// </summary>
        public event EventHandler<EventArgs> CenterUserPosition;

        /// <summary>
        /// Maptapped event.
        /// </summary>
        /// <param name="pos">Position.</param>
        /// <param name="zoomLevel">Zoom level.</param>
        public void MapTappedEvent(Position pos, double zoomLevel)
        {
            if (MapTapped != null)
            {
                MapTapped(this, new MapTappedEventArgs(pos, zoomLevel));
            }
        }

        /// <summary>
        /// Field selected event.
        /// </summary>
        /// <param name="field">Field.</param>
        public void FieldSelectedEvent(Field field)
        {
            if (FieldSelected != null)
            {
                SelectedField = field;
                FieldSelected(this, new FieldSelectedEventArgs(field));
            }
        }

        /// <summary>
        /// Block selected event.
        /// </summary>
        /// <param name="block">Block.</param>
        public void BlockSelectedEvent(Block block)
        {
            if (BlockSelected != null)
            {
                SelectedBlock = block;
                BlockSelected(this, new BlockSelectedEventArgs(block));
            }
        }

        /// <summary>
        /// User postion centered event.
        /// </summary>
        public void CenterUserPostionEvent()
        {
            if (CenterUserPosition != null)
            {
                CenterUserPosition(this, EventArgs.Empty);
            }
        }
    }
}
