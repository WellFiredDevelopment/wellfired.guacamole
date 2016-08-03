using UnityEngine;
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

		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);
			
			if (Style == null)
				Style = new GUIStyle();

			Style.focused.background = BackgroundTexture;
			Style.active.background = ActiveBackgroundTexture;
			Style.hover.background = HoverBackgroundTexture;
			Style.normal.background = BackgroundTexture;

			var button = Control as Button;

		    Debug.Assert(button != null, "button != null");

		    Style.alignment = UITextAlignExtensions.Combine(button.HorizontalTextAlign, button.VerticalTextAlign);

			Style.focused.textColor = button.TextColor.ToUnityColor();
			Style.active.textColor = button.TextColor.ToUnityColor();
			Style.hover.textColor = button.TextColor.ToUnityColor();
			Style.normal.textColor = button.TextColor.ToUnityColor();

			var offset = (float)button.CornerRadius;
			var smallest = (int)(Mathf.Min(offset, Mathf.Min(renderRect.Width * 0.5f, renderRect.Height * 0.5f)) + 0.5f);
			smallest = Mathf.Max(smallest, 2);
			Style.border = new RectOffset(smallest, smallest, smallest, smallest);
			Style.padding = new RectOffset(smallest, smallest, 0, 0);

			GUI.Button(renderRect.ToUnityRect(), button.Text, Style);
		}
	}
}