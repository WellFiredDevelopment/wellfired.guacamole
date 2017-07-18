using System.Collections.Generic;
using System.Linq;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Styling;

namespace WellFired.Guacamole.Views
{
    public static class StyleHelper
    {
        public static void ProcessTriggers(IEnumerable<ITrigger> triggers, IBindableObject bindableObject, string propertyName)
        {
            foreach (var trigger in triggers)
            {
                if(ShouldFiredTrigger(trigger, bindableObject, propertyName))
                    trigger.Fire(bindableObject);
            }
        }

        public static bool ShouldFiredTrigger(ITrigger trigger, IBindableObject bindableObject, string propertyName)
        {
            if (trigger.Property.PropertyName != propertyName)
                return false;

            var value = bindableObject.GetValue(trigger.Property);
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (!value.Equals(trigger.Value))
                return false;

            return DoesPass(bindableObject, trigger.Conditionals);
        }

        private static bool DoesPass(IBindableObject view, IEnumerable<IConditional> conditionals)
        {
            return (from conditional in conditionals let sourceValue = view.GetValue(conditional.Property) select !sourceValue.Equals(conditional.Value)).All(rejected => !rejected);
        }
    }
}