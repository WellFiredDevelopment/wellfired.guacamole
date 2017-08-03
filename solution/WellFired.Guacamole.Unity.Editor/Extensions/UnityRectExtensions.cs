using UnityEngine;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Unity.Editor.Extensions
{
	public static class UnityRectExtensions
	{
		// ReSharper disable once InconsistentNaming
		public static UIRect ToUIRect(this Rect source)
		{
			return UIRect.With((int) source.x, (int) source.y, (int) source.width, (int) source.height);
		}
	}
}