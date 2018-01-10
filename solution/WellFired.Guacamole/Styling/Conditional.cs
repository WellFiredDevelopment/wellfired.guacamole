using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Styling
{
    [PublicAPI]
    public class Conditional : IConditional
    {
        public BindableProperty Property { get; set; }
        public object Value { get; set; }
    }
}