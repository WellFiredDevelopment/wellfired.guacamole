using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Linq;

namespace WellFired.Guacamole.Databinding
{
    public abstract class ObservableObject : IObservableModel
    {
		private Action<ObservableMessage> onBindingEvent = delegate { };
		private ModelBinder binder;
		private ObservableMessage bindingMessage;

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

        protected ObservableObject()
        {
            bindingMessage = new ObservableMessage { sender = this };
            binder = new ModelBinder(this);
        }

        public void RaiseBindingUpdate(string memberName, object paramater)
        {
            if (onBindingEvent != null)
            {
                bindingMessage.name = memberName;
                bindingMessage.value = paramater;
                onBindingEvent(bindingMessage);
            }

            binder.RaiseBindingUpdate(memberName, paramater);
        }

        public void SetValue(string memberName, object paramater)
        {
            binder.RaiseBindingUpdate(memberName, paramater);
        }

        public void Command(string memberName)
        {
            binder.Command(memberName);
        }

        public void Command(string memberName, object paramater)
        {
            binder.Command(memberName, paramater);
        }

        public object GetValue(string memberName)
        {
            return binder.GetValue(memberName);
        }

        public object GetValue(string memberName, object paramater)
        {
            return binder.GetValue(memberName, paramater);
        }

        public virtual void Dispose()
        {
            if (binder != null)
            {
                binder.Dispose();
            }

            if (bindingMessage != null)
            {
                bindingMessage.Dispose();
            }

            bindingMessage = null;
            binder = null;
        }

        public virtual void NotifyProperty(string memberName, object paramater)
        {
            RaiseBindingUpdate(memberName, paramater);
        }

        protected bool Set<T>(ref T valueHolder, T value, string propName = null)
        {
            var same = EqualityComparer<T>.Default.Equals(valueHolder, value);


            if (!same)
            {
                if (string.IsNullOrEmpty(propName))
                {
                    // get call stack
                    var stackTrace = new StackTrace();
                    // get method calls (frames)
                    var stackFrames = stackTrace.GetFrames().ToList();

                    if (propName == null && stackFrames.Count > 1)
                    {
                        propName = stackFrames[1].GetMethod().Name.Replace("set_", "");
                    }
                }
                valueHolder = value;

                NotifyProperty(propName, value);

                return true;
            }

            return false;
        }
    }
}