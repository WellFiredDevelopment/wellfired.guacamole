using UnityEngine;
using WellFired.Guacamole;

[assembly : CustomRenderer(typeof(AdjacentLayout), typeof(WellFired.Guacamole.Unity.Editor.AdjacentLayoutRenderer))]
namespace WellFired.Guacamole.Unity.Editor
{
	public class AdjacentLayoutRenderer : BaseRenderer
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

			var layout = Control as AdjacentLayout;

			var offset = (float)layout.CornerRadius;
			var smallest = (int)(Mathf.Min(offset, Mathf.Min(renderRect.Width * 0.5f, renderRect.Height * 0.5f)) + 0.5f);
			smallest = Mathf.Max(smallest, 2);
			Style.border = new RectOffset(smallest, smallest, smallest, smallest);
			Style.padding = new RectOffset(smallest, smallest, 0, 0);

			GUI.Box(renderRect.ToUnityRect(), "", Style);
		}
	}
}