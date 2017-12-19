using UnityEngine;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Unity.Editor.Extensions
{
	// ReSharper disable once InconsistentNaming
	public static class UIExtensions
	{
		public static Rect ToUnityRect(this UIRect source)
		{
			return new Rect(source.X, source.Y, source.Width, source.Height);
		}
	}
}