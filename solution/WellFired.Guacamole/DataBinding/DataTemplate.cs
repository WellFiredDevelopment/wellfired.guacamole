using System;
using WellFired.Guacamole.Exceptions;

namespace WellFired.Guacamole.DataBinding
{
    public class DataTemplate
    {
        private readonly Func<object, IBindableObject> _builder;

        private DataTemplate(Type type)
        {
            Type = type;
        }
        
        private DataTemplate(Func<object, IBindableObject> builder)
        {
            _builder = builder;
        }

        public Type Type { get; }

        public static DataTemplate Of(Type type)
        {
            return new DataTemplate(type);
        }

        public static DataTemplate Of(Func<object, IBindableObject> builder)
        {
            return new DataTemplate(builder);
        }

        public IBindableObject Create(object caller)
        {
            var instance = Activator.CreateInstance(Type) as IBindableObject;
            if (instance == null)
                throw new DataTemplateTypeIsNotBindableException(Type, caller);
            
            return instance;
        }

        public IBindableObject Create(object caller, object objectRetrieval)
        {
            return _builder == null ? Create(caller) : _builder(objectRetrieval);
        }
    }
}