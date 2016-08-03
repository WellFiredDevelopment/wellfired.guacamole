using System;
using UnityEngine;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls;
using WellFired.Guacamole.View;
using Debug = System.Diagnostics.Debug;

[assembly : CustomRenderer(typeof(Slider), typeof(SliderRenderer))]
namespace WellFired.Guacamole.Unity.Editor.NativeControls
{
	public class SliderRenderer : BaseRenderer
	{
		private GUIStyle Style { get; set; }

		private void CreateStyleWith([NotNull] Slider entry)
		{
			if (Style == null)
				Style = new GUIStyle();

			Style.focused.background = BackgroundTexture;
			Style.active.background = ActiveBackgroundTexture;
			Style.hover.background = HoverBackgroundTexture;
			Style.normal.background = BackgroundTexture;

			Style.padding = entry.Padding.ToRectOffset();
		}

		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);

			var slider = Control as Slider;

			Debug.Assert(slider != null, "slider != null");

			CreateStyleWith(slider);

			var offset = (float)Control.CornerRadius;
			var smallest = (int)(Mathf.Min(offset, Mathf.Min(renderRect.Width * 0.5f, renderRect.Height * 0.5f)) + 0.5f);
			smallest = Mathf.Max(smallest, 2);
			Style.border = new RectOffset(smallest, smallest, smallest, smallest);

			GUI.Box(renderRect.ToUnityRect(), "", Style);
			slider.Value = UnityEditor.EditorGUI.Slider(renderRect.ToUnityRect(), (float)slider.Value, (float)slider.MinValue, (float)slider.MaxValue);
		}
	}
}