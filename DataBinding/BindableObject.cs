using System.Collections.Generic;
using System.ComponentModel;

namespace WellFired.Guacamole.Databinding
{
	public class BindableObject : INotifyPropertyChanged
	{
		public static readonly BindableProperty BindingContextProperty = BindableProperty.Create<BindableObject, INotifyPropertyChanged>(null, BindingMode.OneWay, bindableObject => bindableObject.BindingContext
		);
		
		private INotifyPropertyChanged bindingContext;
		private Dictionary<string, BindableProperty> bindings = new Dictionary<string, BindableProperty>();
		private Dictionary<BindableProperty, BindableContext> contexts = new Dictionary<BindableProperty, BindableContext>();
		private Dictionary<string, BindableContext> targetToContexts = new Dictionary<string, BindableContext>();

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
					bindingContext.PropertyChanged -= OnPropertyChanged;
				
				bindingContext = value;
				foreach(var bindingKvp in bindings) {
					var bindableProperty = bindingKvp.Value;
					contexts[bindableProperty].Object = bindingContext;
					SetValue(bindableProperty, GetValue(bindableProperty));
				}

				OnPropertyChanged(this, new PropertyChangedEventArgs(BindingContextProperty.PropertyName));

				bindingContext.PropertyChanged += OnPropertyChanged;
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
			targetToContexts[targetProperty] = context;
			var initialValue = context.GetValue();
			SetValue(bindableProperty, initialValue);
		}

		public object GetValue(BindableProperty bindableProperty)
		{
			return GetOrCreateBindableContext(bindableProperty).Value;
		}

		public void SetValue(BindableProperty bindableProperty, object value)
		{
			var previous = GetOrCreateBindableContext(bindableProperty).Value;

			if(Equals(previous, value))
				return;

			GetOrCreateBindableContext(bindableProperty).Value = value;

			if(PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(bindableProperty.PropertyName));
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

		public virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if(!targetToContexts.ContainsKey(e.PropertyName))
				return;

			var context = targetToContexts[e.PropertyName];
			var newValue = context.GetValue();
			SetValue(context.Property, newValue);
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}