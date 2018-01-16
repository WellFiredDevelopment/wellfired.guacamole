using System;
using System.Linq.Expressions;
using System.Reflection;

namespace WellFired.Guacamole.DataBinding
{
	public static class GetterInfo
	{
		public static void GetInfo<TA, TB>(Expression<Func<TA, TB>> getter, out string propertyName, out Type propertyType)
		{
			var expression = getter.Body;

			var unaryExpression = expression as UnaryExpression;
			if (unaryExpression != null)
				expression = unaryExpression.Operand;

			var memberExpression = expression as MemberExpression;
			if (memberExpression == null)
				throw new ArgumentException("getter must be a MemberExpression", nameof(getter));

			var propertyInfo = (PropertyInfo) memberExpression.Member;

			propertyName = propertyInfo.Name;
			propertyType = propertyInfo.PropertyType;
		}
	}
}