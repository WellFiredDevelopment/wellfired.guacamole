using System;
using System.Linq;
using System.Reflection;

namespace WellFired.Guacamole.StoredData
{
	public class FieldReflector<T>
	{
		private readonly T _data;
		private readonly object _proxy;
		private readonly FieldInfo[] _fields;
		private readonly PropertyInfo[] _properties;
		
		public FieldReflector(T data, object proxy)
		{
			_data = data;
			_proxy = proxy;
			_fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			_properties = _proxy.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		public void ReflectPropertyToFields(string fieldName, object value)
		{
			_fields.First(fieldInfo => fieldInfo.Name == fieldName).SetValue(_data, value);
		}

		public void ReflectFieldsToProperties()
		{
			foreach (var field in _fields)
			{
				var propertyInfo = _properties.FirstOrDefault(property => property.Name == field.Name);
				propertyInfo?.SetValue(_proxy, field.GetValue(_data), null);
			}
		}
	}
}