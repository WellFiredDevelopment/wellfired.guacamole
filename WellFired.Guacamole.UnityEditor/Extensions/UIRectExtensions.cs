using UnityEngine;

namespace WellFired.Guacamole.Unity.Editor
{
    // ReSharper disable once InconsistentNaming
	public static class UIExtensions 
	{
		public static Rect ToUnityRect(this UIRect source)
		{
			return new Rect (source.X, source.Y, source.Width, source.Height);
		}
	}
}