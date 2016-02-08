using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace WellFired.Guacamole.Databinding
{
    public class ModelBinder : IObservableModel, IDisposable
    {
		Type type;
		ObservableMessage bindingMessage = new ObservableMessage();
		object instance;
		IObservableModel bindableInstance;
		INotifyPropertyChanged notifyInstance;

        public ModelBinder(object newInstance)
        {
			instance = newInstance;
            type = instance.GetType();

            bindableInstance = instance as IObservableModel;
            notifyInstance = instance as INotifyPropertyChanged;

            if (bindableInstance != null)
                bindableInstance.OnBindingUpdate += BindableInstance_OnBindingUpdate;
            else if (notifyInstance != null)
                notifyInstance.PropertyChanged += NotifyInstance_PropertyChanged;

        }

        void NotifyInstance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (onBindingEvent != null)
            {
                bindingMessage.name = e.PropertyName;
                bindingMessage.value = GetValue(e.PropertyName);
                onBindingEvent(bindingMessage);
            }
        }

        void BindableInstance_OnBindingUpdate(ObservableMessage obj)
        {
            if (onBindingEvent != null)
            {
                onBindingEvent(obj);
            }
        }

        public virtual void NotifyProperty(string propertyName, object propValue)
        {
            RaiseBindingUpdate(propertyName, propValue);
        }

		private Action<ObservableMessage> onBindingEvent = delegate { };
        public event Action<ObservableMessage> OnBindingUpdate
        {
            add
            {
                onBindingEvent = (Action<ObservableMessage>)Delegate.Combine(onBindingEvent, value);
            }
            remove
            {
                onBindingEvent = (Action<ObservableMessage>)Delegate.Remove(onBindingEvent, value);
            }
        }

        public void RaiseBindingUpdate(string memberName, object paramater)
        {
            if (onBindingEvent != null)
            {
                bindingMessage.name = memberName;
                bindingMessage.sender = this;
                bindingMessage.value = paramater;

                onBindingEvent(bindingMessage);
            }
        }

        public object GetValue(string memberName)
        {
            var member = type.GetRuntimeMember(memberName);

            if (member == null)
            {
                return null;
            }

            return member.GetMemberValue(instance);
        }

        public object GetValue(string memberName, object paramater)
        {
            var member = type.GetRuntimeMember(memberName);

            if (member == null)
            {
                return null;
            }

            if (member is MethodInfo)
            {
                var meth = (member as MethodInfo);
                if (paramater != null)
                {
                    var p = meth.GetParameters().FirstOrDefault();
                    if (p == null)
                    {
                        return GetValue(memberName);
                    }

                    var converted = ConverterHelper.ConvertTo(p.GetType(), paramater);
                    return meth.Invoke(instance, new[] { converted });
                }

                return meth.Invoke(instance, null);
            }
            if (member is PropertyInfo)
            {
                return (member as PropertyInfo).GetValue(instance, null);
            }

            return (member as FieldInfo).GetValue(instance);
        }

        public void Command(string memberName)
        {
            Command(memberName, null);
        }

        public void Command(string memberName, object paramater)
        {
            var member = type.GetRuntimeMember(memberName);

            if (member == null)
            {
                return;
            }

            // convert to fit signature
            var converted = ConverterHelper.ConvertTo(member.GetParamaterType(), paramater);

            if (member is MethodInfo)
            {
                var method = member as MethodInfo;
                if (method.ReturnType == typeof(IEnumerator))
                {
					throw new NotImplementedException ();
                }
            }

            member.SetMemberValue(instance, converted);
        }

        public void Dispose()
        {
            bindingMessage.Dispose();

            if (bindableInstance != null)
            {
                bindableInstance.OnBindingUpdate -= BindableInstance_OnBindingUpdate;
            }
            if (notifyInstance != null)
            {
                notifyInstance.PropertyChanged -= NotifyInstance_PropertyChanged;
            }
            type = null;
            instance = null;
            bindableInstance = null;
            bindingMessage = null;
            notifyInstance = null;
        }
    }
}