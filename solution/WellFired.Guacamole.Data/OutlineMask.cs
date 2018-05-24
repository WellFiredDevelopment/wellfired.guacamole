using System;

namespace WellFired.Guacamole.Data
{
    [Flags]
    public enum OutlineMask
    {
        None = 0,
        
        Top = 1 << 0,
        Right = 1 << 1,
        Bottom = 1 << 2,
        Left = 1 << 3,

        All = Top | Right | Bottom | Left,
    }
}