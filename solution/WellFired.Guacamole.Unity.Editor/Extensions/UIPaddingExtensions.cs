using UnityEngine;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Unity.Editor.Extensions
{
	// ReSharper disable once InconsistentNaming
	public static class UIPaddingExtensions
	{
		public static RectOffset ToRectOffset(this UIPadding padding)
		{
			return new RectOffset(padding.Left, padding.Right, padding.Top, padding.Bottom);
		}
	}
}