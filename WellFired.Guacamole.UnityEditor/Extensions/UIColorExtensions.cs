using UnityEngine;

namespace WellFired.Guacamole.Unity.Editor
{
    // ReSharper disable once InconsistentNaming
	public static class UIColorExtensions 
	{
		public static Color ToUnityColor(this UIColor source)
		{
			return new Color(source.R, source.G, source.B, source.A);
		}
	}
}