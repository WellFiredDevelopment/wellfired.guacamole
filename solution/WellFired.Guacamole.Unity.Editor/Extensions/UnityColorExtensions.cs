using UnityEngine;
using WellFired.Guacamole.Data;
using JetBrains.Annotations;

namespace WellFired.Guacamole.Unity.Editor.Extensions
{
	[PublicAPI]
	public static class UnityColorExtensions
	{
		// ReSharper disable once InconsistentNaming	
		[PublicAPI]
		public static UIColor ToUIColor(this Color source)
		{
			return UIColor.FromRGB((int) (source.r*255.0f), (int) (source.g*255.0f), (int) (source.b*255.0f));
		}
	}
}