using UnityEngine;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Annotations;

namespace WellFired.Guacamole.Unity.Editor.Extensions
{
	public static class UnityVector2Extensions
	{	
		// ReSharper disable once InconsistentNaming
		[PublicAPI]
		public static UISize ToUISize(this Vector2 source)
		{
			return UISize.Of(source.x, source.y);
		}

		// ReSharper disable once InconsistentNaming
		[PublicAPI]
		public static UILocation ToUILocation(this Vector2 source)
		{
			return UILocation.Of(source.x,  source.y);
		}
	}
}