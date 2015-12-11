using System;

using TreeWatch;

namespace TreeWatch
{
    public class FieldSelectedEventArgs : EventArgs
    {
        public FieldSelectedEventArgs(Field field)
        {
            Field = field;
        }

        public Field Field
        {
            get;
            private set;
        }
    }
}