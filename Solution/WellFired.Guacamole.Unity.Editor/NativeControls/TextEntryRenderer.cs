using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls;
using WellFired.Guacamole.View;
using Debug = System.Diagnostics.Debug;

[assembly: CustomRenderer(typeof(TextEntry), typeof(TextEntryRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls
{
	public class TextEntryRenderer : BaseRenderer
	{
		private GUIStyle Style { get; set; }

		public override UISize? NativeSize
		{
			get
			{
				var entry = Control as TextEntry;
				Debug.Assert(entry != null, $"{nameof(entry)} != null");

				CreateStyleWith(entry);
				return Style.CalcSize(new GUIContent(entry.Text)).ToUISize();
			}
		}

		private void CreateStyleWith([NotNull] TextEntry entry)
		{
			if (Style == null)
				Style = new GUIStyle();

			Style.focused.background = BackgroundTexture;
			Style.active.background = BackgroundTexture;
			Style.hover.background = BackgroundTexture;
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

			var entry = Control as TextEntry;

			Debug.Assert(entry != null, "entry != null");

			CreateStyleWith(entry);

			var offset = (float) Control.CornerRadius;
			var smallest = (int) (Mathf.Min(offset, Mathf.Min(renderRect.Width*0.5f, renderRect.Height*0.5f)) + 0.5f);
			smallest = Mathf.Max(smallest, 2);
			Style.border = new RectOffset(smallest, smallest, smallest, smallest);

			entry.Text = EditorGUI.TextField(renderRect.ToUnityRect(), entry.Text, Style);
		}
	}
}