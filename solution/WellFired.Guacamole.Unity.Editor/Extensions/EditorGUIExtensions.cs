using System.Reflection;
using UnityEditor;

namespace WellFired.Guacamole.Unity.Editor.Extensions
{
	public static class EditorGUIExtensions
	{
		public static FieldInfo ActiveEditorFieldInfo => typeof(EditorGUI).GetField("activeEditor", BindingFlags.Static | BindingFlags.NonPublic);

		// This is disabled so I can get the exception when testing in case this method is removed by unity
		// ReSharper disable once PossibleNullReferenceException
		public static object ActiveEditor => ActiveEditorFieldInfo.GetValue(null);
	}
}