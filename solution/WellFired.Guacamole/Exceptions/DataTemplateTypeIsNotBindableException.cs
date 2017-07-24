using System;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Exceptions
{
    public class DataTemplateTypeIsNotBindableException : GuacamoleUserFacingException
    {
        private readonly Type _type;
        private readonly object _caller;

        public DataTemplateTypeIsNotBindableException(Type type, object caller)
        {
            _type = type;
            _caller = caller;
        }

        public override string UserFacingError()
        {
            return $"The type created by your DataTemplate from a {_caller.GetType()} does not implement {nameof(IBindableObject)}, please ensure all your DataTemplates create something " +
                   $"which implements {nameof(IBindableObject)}. The DataTemplate tried to create something of ({_type}).";
        }
    }
}