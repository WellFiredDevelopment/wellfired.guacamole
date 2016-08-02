using UnityEditor;
using UnityEngine;
using WellFired.Guacamole;

[assembly : CustomRenderer(typeof(NumberEntry), typeof(WellFired.Guacamole.Unity.Editor.NumberEntryRenderer))]
namespace WellFired.Guacamole.Unity.Editor
{
	public class NumberEntryRenderer : BaseRenderer
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

			var entry = Control as NumberEntry;

			Style.alignment = UITextAlignExtensions.Combine(entry.HorizontalTextAlign, entry.VerticalTextAlign);

			Style.focused.textColor = entry.TextColor.ToUnityColor();
			Style.active.textColor = entry.TextColor.ToUnityColor();
			Style.hover.textColor = entry.TextColor.ToUnityColor();
			Style.normal.textColor = entry.TextColor.ToUnityColor();

			var offset = (float)entry.CornerRadius;
			var smallest = (int)(Mathf.Min(offset, Mathf.Min(renderRect.Width * 0.5f, renderRect.Height * 0.5f)) + 0.5f);
			smallest = Mathf.Max(smallest, 2);
			Style.border = new RectOffset(smallest, smallest, smallest, smallest);
			Style.padding = new RectOffset(smallest, smallest, 0, 0);

			var newNumber = EditorGUI.FloatField(renderRect.ToUnityRect(), entry.Number, Style);
			if(Equals(newNumber, entry.Number))
				entry.Number = newNumber;
		}
	}
}