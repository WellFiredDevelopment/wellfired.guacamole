using System.Collections.Generic;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Styling
{
    public interface ITrigger
    {
        BindableProperty Property { get; }
        object Value { get; }
        IList<ISetter> Setters { get; }
        IList<IConditional> Conditionals { get; }
        void Fire(IBindableObject bindableObject);
    }
}