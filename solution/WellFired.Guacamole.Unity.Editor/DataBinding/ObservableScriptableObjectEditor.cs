using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using JetBrains.Annotations;
using Object = UnityEngine.Object;

namespace WellFired.Guacamole.Unity.Editor.DataBinding
{
	[CustomEditor(typeof(ObservableScriptableObject), true)]
	[UsedImplicitly]
	public class ObservableScriptableObjectEditor : UnityEditor.Editor
	{
		private PropertyField[] _fields;
		private ObservableScriptableObject _instance;

		private static PropertyField[] GetProperties(object obj)
		{
			var fields = new List<PropertyField>();

			var infos = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

			foreach (var info in infos)
			{
				if (!(info.CanRead && info.CanWrite))
					continue;

				var attributes = info.GetCustomAttributes(true);

				var isExposed = attributes.Any(o => o.GetType() == typeof(ExposePropertyAttribute));
				if (!isExposed)
					continue;

				SerializedPropertyType type;
				if (!PropertyField.GetPropertyType(info, out type))
					continue;

				var field = new PropertyField(obj, info, type);
				fields.Add(field);
			}

			return fields.ToArray();
		}

		public override void OnInspectorGUI()
		{
			_instance = target as ObservableScriptableObject;
			_fields = GetProperties(_instance);

			if (_instance == null)
				return;
			if (_fields == null)
				return;

			DrawDefaultInspector();
			Expose(_fields);
		}

		private static void Expose(IEnumerable<PropertyField> properties)
		{
			var emptyOptions = new GUILayoutOption[0];
			EditorGUILayout.BeginVertical(emptyOptions);
			foreach (var field in properties)
			{
				EditorGUILayout.BeginHorizontal(emptyOptions);
				switch (field.Type)
				{
					case SerializedPropertyType.Integer:
					{
						var oldValue = (int) field.GetValue();
						var newValue = EditorGUILayout.IntField(field.Name, oldValue, emptyOptions);
						if (oldValue != newValue)
							field.SetValue(newValue);
					}
						break;
					case SerializedPropertyType.Float:
					{
						var oldValue = (float) field.GetValue();
						var newValue = EditorGUILayout.FloatField(field.Name, oldValue, emptyOptions);
						if (Math.Abs(oldValue - newValue) > 0.001f)
							field.SetValue(newValue);
					}
						break;
					case SerializedPropertyType.Boolean:
					{
						var oldValue = (bool) field.GetValue();
						var newValue = EditorGUILayout.Toggle(field.Name, oldValue, emptyOptions);
						if (oldValue != newValue)
							field.SetValue(newValue);
					}
						break;
					case SerializedPropertyType.String:
					{
						var oldValue = (string) field.GetValue();
						var newValue = EditorGUILayout.TextField(field.Name, oldValue, emptyOptions);
						if (oldValue != newValue)
							field.SetValue(newValue);
					}
						break;
					case SerializedPropertyType.Vector2:
					{
						var oldValue = (Vector2) field.GetValue();
						var newValue = EditorGUILayout.Vector2Field(field.Name, oldValue, emptyOptions);
						if (oldValue != newValue)
							field.SetValue(newValue);
					}
						break;
					case SerializedPropertyType.Vector3:
					{
						var oldValue = (Vector3) field.GetValue();
						var newValue = EditorGUILayout.Vector3Field(field.Name, oldValue, emptyOptions);
						if (oldValue != newValue)
							field.SetValue(newValue);
					}
						break;
					case SerializedPropertyType.Enum:
					{
						var oldValue = (Enum) field.GetValue();
						var newValue = EditorGUILayout.EnumPopup(field.Name, oldValue, emptyOptions);
						if (!Equals(oldValue, newValue))
							field.SetValue(newValue);
					}
						break;
					case SerializedPropertyType.ObjectReference:
					{
						var oldValue = (Object) field.GetValue();
						var newValue = EditorGUILayout.ObjectField(
							field.Name,
							oldValue,
							field.Info.PropertyType,
							true,
							emptyOptions);
						if (oldValue != newValue)
							field.SetValue(newValue);
					}
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
				EditorGUILayout.EndHorizontal();
			}
			EditorGUILayout.EndVertical();
		}

		private class PropertyField
		{
			private readonly MethodInfo _getter;
			private readonly object _obj;
			private readonly MethodInfo _setter;

			public PropertyField(object obj, PropertyInfo info, SerializedPropertyType type)
			{
				_obj = obj;
				Info = info;
				Type = type;

				_getter = Info.GetGetMethod();
				_setter = Info.GetSetMethod();
			}

			public PropertyInfo Info { get; }

			public SerializedPropertyType Type { get; }

			public string Name => ObjectNames.NicifyVariableName(Info.Name);

			public object GetValue()
			{
				return _getter.Invoke(_obj, null);
			}

			public void SetValue(object value)
			{
				_setter.Invoke(_obj, new[] {value});
			}

			public static bool GetPropertyType(PropertyInfo info, out SerializedPropertyType propertyType)
			{
				var type = info.PropertyType;
				propertyType = SerializedPropertyType.Generic;
				if (type == typeof(int))
					propertyType = SerializedPropertyType.Integer;
				else if (type == typeof(float))
					propertyType = SerializedPropertyType.Float;
				else if (type == typeof(bool))
					propertyType = SerializedPropertyType.Boolean;
				else if (type == typeof(string))
					propertyType = SerializedPropertyType.String;
				else if (type == typeof(Vector2))
					propertyType = SerializedPropertyType.Vector2;
				else if (type == typeof(Vector3))
					propertyType = SerializedPropertyType.Vector3;
				else if (type.IsEnum)
					propertyType = SerializedPropertyType.Enum;
				else if (typeof(MonoBehaviour).IsAssignableFrom(type))
					propertyType = SerializedPropertyType.ObjectReference;
				return propertyType != SerializedPropertyType.Generic;
			}
		}
	}
}