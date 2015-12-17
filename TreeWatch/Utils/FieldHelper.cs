// <copyright file="FieldHelper.cs" company="TreeWatch">
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
    using System;

    /// <summary>
    /// Helper for Field related stuff, like events and global caching of
    /// selected fields and blocks
    /// </summary>
    public class FieldHelper
    {
        /// <summary>
        /// The INSTANCE.
        /// </summary>
        private static readonly FieldHelper INSTANCE = new FieldHelper();

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
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static FieldHelper Instance
        {
            get { return INSTANCE; }
        }

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
        /// Maptapped event.
        /// </summary>
        /// <param name="pos">Position where the tapped event is raised.</param>
        /// <param name="zoomLevel">Zoom level where the tapped event is raised.</param>
        public void MapTappedEvent(Position pos, double zoomLevel)
        {
            if (this.MapTapped != null)
            {
                this.MapTapped(this, new MapTappedEventArgs(pos, zoomLevel));
            }
        }

        /// <summary>
        /// Field selected event.
        /// </summary>
        /// <param name="field">Field that raises the selected event.</param>
        public void FieldSelectedEvent(Field field)
        {
            if (this.FieldSelected != null)
            {
                FieldHelper.SelectedField = field;
                this.FieldSelected(this, new FieldSelectedEventArgs(field));
            }
        }

        /// <summary>
        /// Block selected event.
        /// </summary>
        /// <param name="block">Block that raises the selected event.</param>
        public void BlockSelectedEvent(Block block)
        {
            if (this.BlockSelected != null)
            {
                FieldHelper.SelectedBlock = block;
                this.BlockSelected(this, new BlockSelectedEventArgs(block));
            }
        }

        /// <summary>
        /// User postion centered event.
        /// </summary>
        public void CenterUserPostionEvent()
        {
            if (this.CenterUserPosition != null)
            {
                this.CenterUserPosition(this, EventArgs.Empty);
            }
        }
    }
}