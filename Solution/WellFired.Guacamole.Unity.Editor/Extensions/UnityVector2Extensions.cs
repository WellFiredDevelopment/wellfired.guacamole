using UnityEngine;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Unity.Editor.Extensions
{
	public static class UnityVector2Extensions
	{
		// ReSharper disable once InconsistentNaming
		[PublicAPI]
		public static UISize ToUISize(this Vector2 source)
		{
			return new UISize((int) source.x, (int) source.y);
		}

		// ReSharper disable once InconsistentNaming
		[PublicAPI]
		public static UILocation ToUILocation(this Vector2 source)
		{
			return new UILocation((int) source.x, (int) source.y);
		}
	}
}