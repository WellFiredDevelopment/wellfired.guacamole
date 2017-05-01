using System;

namespace WellFired.Guacamole.DataBinding
{
    public class DataTemplate
    {
        private DataTemplate(Type type)
        {
            Type = type;
        }

        public Type Type { get; private set; }

        public static DataTemplate Of(Type type)
        {
            return new DataTemplate(type);
        }
    }
}