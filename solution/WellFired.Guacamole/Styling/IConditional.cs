using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Styling
{
    public interface IConditional
    {
        BindableProperty Property { get; }
        object Value { get; }
    }
}