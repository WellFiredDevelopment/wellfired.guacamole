using UnityEngine;

namespace WellFired.Guacamole.Unity.Editor.Extensions
{
	public static class UnityEventExtension
	{
		public static bool LeftMouseDown(this UnityEngine.Event unityEvent)
		{
			return unityEvent.rawType == EventType.MouseDown && unityEvent.button == 0;
		}
		
		public static bool LeftMouseUp(this UnityEngine.Event unityEvent)
		{
			return unityEvent.rawType == EventType.MouseUp && unityEvent.button == 0;
		}
	}
}