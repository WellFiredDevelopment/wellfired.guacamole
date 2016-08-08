using System.Collections.Generic;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole
{
    [PublicAPI]
    public class Setter
    {
        [PublicAPI]
        public BindableProperty Property { get; set; }
        [PublicAPI]
        public object Value { get; set; }
    }

    public class Trigger
    {
        [PublicAPI]
        public BindableProperty Property { get; set; }
        [PublicAPI]
        public object Value { get; set; }
        [PublicAPI]
        public IList<Setter> Setters { get; set; } = new List<Setter>();
    }

    public class Style
    {
        [PublicAPI]
        public IList<Setter> Setters { get; set; } = new List<Setter>();
        [PublicAPI]
        public IList<Trigger> Triggers { get; set; } = new List<Trigger>();
    }
}