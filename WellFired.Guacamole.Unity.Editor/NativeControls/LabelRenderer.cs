﻿using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls;
using WellFired.Guacamole.View;
using Debug = System.Diagnostics.Debug;

[assembly : CustomRenderer(typeof(Label), typeof(LabelRenderer))]
namespace WellFired.Guacamole.Unity.Editor.NativeControls
{
	public class LabelRenderer : BaseRenderer
	{
		private GUIStyle Style { get; set; }

		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);

			if (Style == null)
				Style = new GUIStyle();

			Style.focused.background = BackgroundTexture;
			Style.active.background = ActiveBackgroundTexture;
			Style.hover.background = HoverBackgroundTexture;
			Style.normal.background = BackgroundTexture;
			
			var label = Control as Label;

		    Debug.Assert(label != null, "label != null");

		    Style.alignment = UITextAlignExtensions.Combine(label.HorizontalTextAlign, label.VerticalTextAlign);

			Style.focused.textColor = label.TextColor.ToUnityColor();
			Style.active.textColor = label.TextColor.ToUnityColor();
			Style.hover.textColor = label.TextColor.ToUnityColor();
			Style.normal.textColor = label.TextColor.ToUnityColor();

			var offset = (float)label.CornerRadius;
			var smallest = (int)(Mathf.Min(offset, Mathf.Min(renderRect.Width * 0.5f, renderRect.Height * 0.5f)) + 0.5f);
			smallest = Mathf.Max(smallest, 2);
			Style.border = new RectOffset(smallest, smallest, smallest, smallest);
			Style.padding = new RectOffset(smallest, smallest, 0, 0);

			UnityEditor.EditorGUI.LabelField(renderRect.ToUnityRect(), label.Text, Style);
		}
	}
}