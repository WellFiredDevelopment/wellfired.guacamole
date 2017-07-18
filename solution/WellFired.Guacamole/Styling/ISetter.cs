using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Styling
{
    public interface ISetter
    {
        BindableProperty Property { get; }
        object Value { get; }
    }
}