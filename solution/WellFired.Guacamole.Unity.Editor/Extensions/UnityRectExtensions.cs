using UnityEngine;
using WellFired.Guacamole.Types;

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