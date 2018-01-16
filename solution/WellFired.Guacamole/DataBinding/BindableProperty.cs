using System;
using System.Linq.Expressions;

namespace WellFired.Guacamole.DataBinding
{
	public class BindableProperty
	{
		public string PropertyName { get; private set; }

		public object DefaultValue { get; private set; }

		public Type PropertyType { get; private set; }

		public BindingMode BindingMode { get; set; }

		public static BindableProperty Create<TA, TB>(TB defaultValue, BindingMode bindingMode, Expression<Func<TA, TB>> getter)
		{
			GetterInfo.GetInfo(getter, out var propertyName, out var propertyType);
			
			var property = new BindableProperty
			{
				PropertyName = propertyName,
				PropertyType = propertyType,
				DefaultValue = defaultValue,
				BindingMode = bindingMode
			};

			return property;
		}
	}
}