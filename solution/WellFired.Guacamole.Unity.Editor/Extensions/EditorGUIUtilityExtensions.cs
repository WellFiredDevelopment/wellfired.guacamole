using System.Reflection;
using UnityEditor;

namespace WellFired.Guacamole.Unity.Editor.Extensions
{
	public static class EditorGUIUtilityExtensions
	{
		public static FieldInfo LastControlIDFieldInfo => typeof(EditorGUIUtility).GetField("s_LastControlID", BindingFlags.Static | BindingFlags.NonPublic);
		public static int LastControlId => (int) LastControlIDFieldInfo.GetValue(null);
	}
}