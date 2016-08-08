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

    public class Style
    {
        [PublicAPI]
        public IList<Setter> Setters { get; set; } = new List<Setter>();
    }
}