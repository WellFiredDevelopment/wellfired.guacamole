using UnityEngine;
using WellFired.Guacamole.Data;
using JetBrains.Annotations;

namespace WellFired.Guacamole.Unity.Editor.Extensions
{
	[PublicAPI]
	public static class UITextClippingExtensions
	{
		// ReSharper disable once InconsistentNaming	
		[PublicAPI]
		public static TextClipping ToUnityClipping(this UITextClipping clipping)
		{
			return clipping == UITextClipping.Clip ? TextClipping.Clip : TextClipping.Overflow;
		}
	}
}