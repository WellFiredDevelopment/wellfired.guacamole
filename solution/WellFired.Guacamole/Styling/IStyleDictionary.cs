using System;

namespace WellFired.Guacamole.Styling
{
    public interface IStyleDictionary
    {
        void Add(Style aStyle, Type forViewType);
        Style Get(Type forViewType);
    }
}