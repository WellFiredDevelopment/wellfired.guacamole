using UnityEngine;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Unity.Editor.Extensions
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