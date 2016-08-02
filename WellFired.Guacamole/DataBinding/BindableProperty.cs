using System;
using System.Linq.Expressions;
using System.Reflection;

namespace WellFired.Guacamole.Databinding
{
	public class BindableProperty
	{
		public string PropertyName
		{
			get;
			set;
		}
	
		public System.Func<object, object> Getter 
		{
			set;
			get;
		}
	
		public object DefaultValue 
		{
			get;
			set;
		}

		public Type PropertyType
		{
			get;
			set;
		}
	
		public BindingMode BindingMode 
		{
			get;
			set;
		}

		/// <summary>
		/// Creates a BindableProperty on an object of Type A, binding to a property of Type B
		/// For example..... BindableProperty<NumberEntry, float> Will create a bindable property on 
		/// a NumberEntry, binding to float Property.
		/// </summary>
		public static BindableProperty Create<TA, TB>(TB defaultValue, BindingMode bindingMode, Expression<Func<TA,TB>> getter)
		{
			var expression = getter.Body;

			var unaryExpression = expression as UnaryExpression;
			if(unaryExpression != null)
				expression = unaryExpression.Operand;

			var memberExpression = expression as MemberExpression;
			if(memberExpression == null)
				throw new ArgumentException("getter must be a MemberExpression", "getter");

			var propertyInfo = (PropertyInfo)memberExpression.Member;
			
			var property = new BindableProperty {
				PropertyName = propertyInfo.Name,
				PropertyType = propertyInfo.PropertyType,
				DefaultValue = defaultValue,
				BindingMode = bindingMode,
			};

			return property;
		}
	}
}