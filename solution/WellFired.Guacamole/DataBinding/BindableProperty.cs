using System;
using System.Linq.Expressions;
using System.Reflection;

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
			var expression = getter.Body;

			var unaryExpression = expression as UnaryExpression;
			if (unaryExpression != null)
				expression = unaryExpression.Operand;

			var memberExpression = expression as MemberExpression;
			if (memberExpression == null)
				throw new ArgumentException("getter must be a MemberExpression", nameof(getter));

			var propertyInfo = (PropertyInfo) memberExpression.Member;

			var property = new BindableProperty
			{
				PropertyName = propertyInfo.Name,
				PropertyType = propertyInfo.PropertyType,
				DefaultValue = defaultValue,
				BindingMode = bindingMode
			};

			return property;
		}
	}
}