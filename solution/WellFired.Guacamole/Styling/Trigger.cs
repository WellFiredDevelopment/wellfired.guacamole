using System.Collections.Generic;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Styling
{
    public class Trigger : ITrigger
    {
        public BindableProperty Property { get; set; }
        public object Value { get; set; }
        public IList<ISetter> Setters { get; } = new List<ISetter>();
        public IList<IConditional> Conditionals { get; } = new List<IConditional>();

        public void Fire(IBindableObject bindableObject)
        {
            foreach (var setter in Setters)
                bindableObject.SetValue(setter.Property, setter.Value);
        }
    }
}