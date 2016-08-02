using UnityEngine;
using WellFired.Guacamole;

[assembly : CustomRenderer(typeof(Button), typeof(WellFired.Guacamole.Unity.Editor.ButtonRenderer))]
namespace WellFired.Guacamole.Unity.Editor
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