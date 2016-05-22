using System.Collections.Generic;
using System.ComponentModel;

namespace WellFired.Guacamole.Databinding
{
	public class BindableObject
	{
		public static readonly BindableProperty BindingContextProperty = BindableProperty.Create<BindableObject, INotifyPropertyChanged>(null, BindingMode.OneWay, bindableObject => bindableObject.BindingContext
		);
		
		private INotifyPropertyChanged bindingContext;
		private Dictionary<string, BindableProperty> bindings = new Dictionary<string, BindableProperty>();
		private Dictionary<BindableProperty, BindableContext> contexts = new Dictionary<BindableProperty, BindableContext>();

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
				throw new BindingExistsException(bindableProperty.PropertyName);

			bindableProperty.BindingMode = bindingMode;

			bindings[bindableProperty.PropertyName] = bindableProperty;
			var context = GetOrCreateBindableContext(bindableProperty);
			context.Object = BindingContext;
			context.TargetProperty = targetProperty;
			var initialValue = context.GetInitialValue();
			SetValue(bindableProperty, initialValue);
		}

		public object GetValue(BindableProperty bindableProperty)
		{
			return GetOrCreateBindableContext(bindableProperty).Value;
		}

		public void SetValue(BindableProperty bindableProperty, object value)
		{
			GetOrCreateBindableContext(bindableProperty).Value = value;
		}

		private BindableContext GetOrCreateBindableContext(BindableProperty bindableProperty)
		{
			var bindablePropertyContext = GetContext(bindableProperty);
			if(bindablePropertyContext == null)
				bindablePropertyContext = CreateAndAddContext(bindableProperty);
			return bindablePropertyContext;
		}

		private BindableContext GetContext(BindableProperty bindableProperty)
		{
			try
			{ return contexts[bindableProperty]; }
			catch
			{
				// ignored
			}
			return null;
		}

		private BindableContext CreateAndAddContext(BindableProperty bindableProperty)
		{
			var bindablePropertyContext = new BindableContext {
				Property = bindableProperty,
				Value = bindableProperty.DefaultValue,
				Object = bindingContext
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