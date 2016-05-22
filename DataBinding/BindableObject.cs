using System.ComponentModel;
using System.Collections.Generic;
using System;

namespace WellFired.Guacamole.Databinding
{
	public class BindableObject
	{
		public static readonly BindableProperty BindingContextProperty = BindableProperty.Create<BindableObject, INotifyPropertyChanged>(
			defaultValue: null,
			bindingMode: BindingMode.OneWay,
			getter: bindableObject => bindableObject.BindingContext
		);
		
		private INotifyPropertyChanged bindingContext;
		private Dictionary<string, WellFired.Guacamole.Databinding.BindableProperty> bindings = new Dictionary<string, WellFired.Guacamole.Databinding.BindableProperty>();
		private Dictionary<WellFired.Guacamole.Databinding.BindableProperty, WellFired.Guacamole.Databinding.BindableContext> contexts = new Dictionary<WellFired.Guacamole.Databinding.BindableProperty, WellFired.Guacamole.Databinding.BindableContext>();

		public INotifyPropertyChanged BindingContext
		{
			get 
			{
				return bindingContext;
			}
			set 
			{
				// Here we check for equality so we can avoid recursion.
				if(bindingContext == value)
					return;

				if(bindingContext != null)
					bindingContext.PropertyChanged -= PropertyChanged;
				
				bindingContext = value;
				foreach(var bindingKvp in bindings) {
					var bindableProperty = bindingKvp.Value;
					contexts[bindableProperty].Object = bindingContext;
					SetValue(bindableProperty, GetValue(bindableProperty));
				}

				PropertyChanged(this, new PropertyChangedEventArgs(BindingContextProperty.PropertyName));

				bindingContext.PropertyChanged += PropertyChanged;
			}
		}

		public void Bind(BindableProperty bindableProperty, string targetProperty, BindingMode bindingMode = BindingMode.OneWay)
		{
			if(bindings.ContainsKey(bindableProperty.PropertyName))
				throw new BindingExistsException(forBinding: bindableProperty.PropertyName);
			
			bindings[bindableProperty.PropertyName] = bindableProperty;
			var context = GetOrCreateBindableContext(bindableProperty);
			context.Object = BindingContext;
			context.TargetProperty = targetProperty;
			var initialValue = context.GetInitialValue();
			SetValue(bindableProperty, initialValue);
		}

		public object GetValue(WellFired.Guacamole.Databinding.BindableProperty bindableProperty)
		{
			return GetOrCreateBindableContext(bindableProperty).Value;
		}

		public void SetValue(WellFired.Guacamole.Databinding.BindableProperty bindableProperty, object value)
		{
			GetOrCreateBindableContext(bindableProperty).Value = value;
		}

		private WellFired.Guacamole.Databinding.BindableContext GetOrCreateBindableContext(WellFired.Guacamole.Databinding.BindableProperty bindableProperty)
		{
			var bindablePropertyContext = GetContext(bindableProperty);
			if(bindablePropertyContext == null) {
				bindablePropertyContext = CreateAndAddContext(bindableProperty);
			}
			return bindablePropertyContext;
		}

		private WellFired.Guacamole.Databinding.BindableContext GetContext(WellFired.Guacamole.Databinding.BindableProperty bindableProperty)
		{
			try
			{ return contexts[bindableProperty]; }
			catch 
			{}
			return null;
		}

		private WellFired.Guacamole.Databinding.BindableContext CreateAndAddContext(WellFired.Guacamole.Databinding.BindableProperty bindableProperty)
		{
			var bindablePropertyContext = new WellFired.Guacamole.Databinding.BindableContext {
				Property = bindableProperty,
				Value = bindableProperty.DefaultValue,
				Object = bindingContext,
			};

			contexts[bindableProperty] = bindablePropertyContext;
			return bindablePropertyContext;
		}

		public virtual void PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if(!bindings.ContainsKey(e.PropertyName))
				return;

			var binding = bindings[e.PropertyName];
			var newValue = GetValue(binding);
			SetValue(binding, newValue);
		}
	}
}