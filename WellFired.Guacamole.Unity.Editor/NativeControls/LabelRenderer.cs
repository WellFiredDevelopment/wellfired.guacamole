using UnityEngine;
using WellFired.Guacamole.Annotations;
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

		public override UISize? NativeSize
		{
			get
			{
				var label = Control as Label;
				Debug.Assert(label != null, "numberEntry != null");

				CreateStyleWith(label);
				return Constrain(Style.CalcSize(new GUIContent(label.Text)).ToUISize());
			}
		}

		private void CreateStyleWith([NotNull] Label label)
		{
			if (Style == null)
				Style = new GUIStyle();

			Style.focused.background = BackgroundTexture;
			Style.active.background = ActiveBackgroundTexture;
			Style.hover.background = HoverBackgroundTexture;
			Style.normal.background = BackgroundTexture;

			Style.alignment = UITextAlignExtensions.Combine(label.HorizontalTextAlign, label.VerticalTextAlign);

			Style.focused.textColor = label.TextColor.ToUnityColor();
			Style.active.textColor = label.TextColor.ToUnityColor();
			Style.hover.textColor = label.TextColor.ToUnityColor();
			Style.normal.textColor = label.TextColor.ToUnityColor();

			Style.padding = label.Padding.ToRectOffset();
		}

		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);
			
			var label = Control as Label;

		    Debug.Assert(label != null, "label != null");

			CreateStyleWith(label);

			UnityEditor.EditorGUI.LabelField(renderRect.ToUnityRect(), label.Text, Style);
		}
	}
}