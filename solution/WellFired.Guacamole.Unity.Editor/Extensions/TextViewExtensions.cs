using System;
using UnityEngine;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Unity.Editor.Extensions
{
	public static class TextViewExtensions
	{
		public static UISize CalculateNativeSizeWithWordWrap(IView view, Vector2 nativeSize, GUIContent content, GUIStyle style)
		{
			var wordWrapBreakpoint = view.MaxSize.Width;

			if (view.RectRequest.Width < wordWrapBreakpoint)
				wordWrapBreakpoint = view.RectRequest.Width; 

			// if Unity's calculated size is less than the wrap breakpoint, we can return the basic size.
			if (nativeSize.x < wordWrapBreakpoint)
				return nativeSize.ToUISize();

			// We set our width to the breakpoint and calculate our height based on that breakpoint data.
			nativeSize.x = wordWrapBreakpoint;
			nativeSize.y = style.CalcHeight(content, wordWrapBreakpoint);

			return nativeSize.ToUISize();
		}
		
		public static bool HasHeightChanged(UIRect renderRect, GUIContent content, GUIStyle style)
		{
			var currentWidth = renderRect.Width;
			var height = style.CalcHeight(content, currentWidth);
			return Math.Abs(height - renderRect.Height) > 0.01f;
		}
	}
}