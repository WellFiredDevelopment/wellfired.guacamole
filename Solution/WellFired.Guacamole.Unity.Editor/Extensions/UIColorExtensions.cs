using UnityEngine;

namespace WellFired.Guacamole.Unity.Editor.Extensions
{
    // ReSharper disable once InconsistentNaming
	public static class UIColorExtensions 
	{
		public static Color ToUnityColor(this Types.UIColor source)
		{
			return new Color(source.R, source.G, source.B, source.A);
		}
	}
}