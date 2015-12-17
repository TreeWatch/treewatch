﻿using System;

namespace TreeWatch
{
    public class BlockSelectedEventArgs : EventArgs
    {
        public BlockSelectedEventArgs(Block block)
        {
            Block = block;
        }

        public Block Block
        {
            get;
            private set;
        }
    }
}