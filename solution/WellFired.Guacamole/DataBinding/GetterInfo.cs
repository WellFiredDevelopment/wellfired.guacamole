using System;
using System.Linq.Expressions;
using System.Reflection;

namespace WellFired.Guacamole.DataBinding
{
	public static class GetterInfo
	{
		/// <summary>
		/// Extract the property name and property type from an Expression. This is a convenient way to get something
		/// similar to reflexion without the hassle of using non-refactorable string values. If the expression is v => v.Text
		/// and that Text is a property belonging to v of type string, then the returned name will be "Text" and the return
		/// type will be string.
		/// </summary>
		/// <param name="getter">the expression returning the property we want to get name and type.</param>
		/// <param name="propertyName"></param>
		/// <param name="propertyType"></param>
		/// <typeparam name="TA">The type of the object owning the property</typeparam>
		/// <typeparam name="TB">The type of the property</typeparam>
		/// <exception cref="ArgumentException">Thrown if the expression is not a MemberExpression.</exception>
		public static void GetInfo<TA, TB>(Expression<Func<TA, TB>> getter, out string propertyName, out Type propertyType)
		{
			var expression = getter.Body;

			if (expression is UnaryExpression unaryExpression)
				expression = unaryExpression.Operand;

			if (!(expression is MemberExpression memberExpression))
				throw new ArgumentException("getter must be a MemberExpression", nameof(getter));

			var propertyInfo = (PropertyInfo) memberExpression.Member;

			propertyName = propertyInfo.Name;
			propertyType = propertyInfo.PropertyType;
		}
	}
}