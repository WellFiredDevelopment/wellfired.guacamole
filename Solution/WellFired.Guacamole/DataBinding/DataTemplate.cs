using System;

namespace WellFired.Guacamole.DataBinding
{
    public class DataTemplate
    {
        public DataTemplate(Type type)
        {
            Type = type;
        }

        public Type Type { get; set; }
    }
}