using System;
using UnityEngine;

namespace WellFired.Guacamole.Databinding
{ 
    public class ObservableMessage : IDisposable
    {
        public object sender;
        public string name;
        public object value;

        public T CastValue<T>()
        {
            return (T)value;
        }

        public void Dispose()
        {
            name = null;
            value = sender = null;
        }

        public override string ToString()
        {
			return string.Format("Observable {0} {1}", name, value);
        }
    }

    public interface IObservableModel
    {
        event Action<ObservableMessage> OnBindingUpdate;
        void RaiseBindingUpdate(string memberName, object paramater);
        object GetValue(string memberName);
        object GetValue(string memberName, object paramater);
        void Command(string memberName);
        void Command(string memberName, object paramater);
    }
}