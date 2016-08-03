﻿using System;
using UnityEngine;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls;
using WellFired.Guacamole.View;
using Debug = System.Diagnostics.Debug;

[assembly : CustomRenderer(typeof(Button), typeof(ButtonRenderer))]
namespace WellFired.Guacamole.Unity.Editor.NativeControls
{
	public class ButtonRenderer : BaseRenderer
	{
		private GUIStyle Style { get; set; }

		public override UISize? NativeSize
		{
			get
			{
				var button = Control as Button;
				Debug.Assert(button != null, "button != null");

				CreateStyleWith(button);
				return Constrain(Style.CalcSize(new GUIContent(button.Text)).ToUISize());
			}
		}

		private void CreateStyleWith([NotNull] Button entry)
		{
			if (Style == null)
				Style = new GUIStyle();

			Style.focused.background = BackgroundTexture;
			Style.active.background = ActiveBackgroundTexture;
			Style.hover.background = HoverBackgroundTexture;
			Style.normal.background = BackgroundTexture;

			Style.alignment = UITextAlignExtensions.Combine(entry.HorizontalTextAlign, entry.VerticalTextAlign);

			Style.focused.textColor = entry.TextColor.ToUnityColor();
			Style.active.textColor = entry.TextColor.ToUnityColor();
			Style.hover.textColor = entry.TextColor.ToUnityColor();
			Style.normal.textColor = entry.TextColor.ToUnityColor();

			Style.padding = entry.Padding.ToRectOffset();
		}

		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);
			
			if (Style == null)
				Style = new GUIStyle();
			
			var button = Control as Button;

		    Debug.Assert(button != null, "button != null");

			CreateStyleWith(button);

			GUI.Button(renderRect.ToUnityRect(), button.Text, Style);
		}
	}
}