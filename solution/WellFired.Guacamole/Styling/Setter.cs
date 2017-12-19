using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Styling
{
    [PublicAPI]
    public class Setter : ISetter
    {
        public BindableProperty Property { get; set; }
        public object Value { get; set; }
    }
}