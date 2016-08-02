using UnityEngine;

namespace WellFired.Guacamole.Unity.Editor
{
    // ReSharper disable once InconsistentNaming
	public static class UISizeExtensions 
	{
		public static Vector2 ToUnityVector2(this UISize source)
		{			
			return new Vector2 (source.Width, source.Height);
		}
	}
}