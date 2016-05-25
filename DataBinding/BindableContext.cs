using System;
using System.ComponentModel;
using System.Reflection;

namespace WellFired.Guacamole.Databinding
{
	public class BindableContext
	{
		public BindableProperty Property;
		private PropertyInfo PropertyInfo;
		private MethodInfo PropertySetMethod;
		private MethodInfo PropertyGetMethod;
		private INotifyPropertyChanged bindableObject;
		private object value;
		private string targetProperty;

		public string TargetProperty 
		{ 
			get { return targetProperty; }
			set {
				targetProperty = value;
				ConfigureSet();
			}
		}

		public object Value
		{
			set
			{
				if (this.value == value)
					return;

				this.value = value;

				switch (Property.BindingMode)
				{
					case BindingMode.OneWay:
						return;
					case BindingMode.TwoWay:
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				if (PropertySetMethod != null)
					PropertySetMethod.Invoke(bindableObject, new[] {value});
			}
			get { return this.value; }
		}

		public INotifyPropertyChanged Object
		{
			get { return bindableObject; }
			set
			{
				bindableObject = value;
				ConfigureSet();
			}
		}

		private void ConfigureSet()
		{
			if (bindableObject == null || Property == null || TargetProperty == null)
			{
				PropertyInfo = null;
				PropertySetMethod = null;
			}
			else
			{
				var type = bindableObject.GetType();
				PropertyInfo = type.GetProperty(TargetProperty, BindingFlags.Public | BindingFlags.Instance);
				PropertySetMethod = PropertyInfo.GetSetMethod();
				PropertyGetMethod = PropertyInfo.GetGetMethod();
			}
		}

		public object GetValue()
		{
			if (PropertyGetMethod != null)
				return PropertyGetMethod.Invoke(bindableObject, null);

			return null;
		}
	}
}