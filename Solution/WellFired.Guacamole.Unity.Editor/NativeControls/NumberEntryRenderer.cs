using System.Globalization;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls;
using WellFired.Guacamole.Views;
using Debug = System.Diagnostics.Debug;

[assembly: CustomRenderer(typeof(NumberEntry), typeof(NumberEntryRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls
{
	public class NumberEntryRenderer : BaseRenderer
	{
		private GUIStyle Style { get; set; }

		public override UISize? NativeSize
		{
			get
			{
				var numberEntry = Control as NumberEntry;
				Debug.Assert(numberEntry != null, $"{nameof(numberEntry)} != null");

				CreateStyleWith(numberEntry);
				return Style.CalcSize(new GUIContent(numberEntry.Number.ToString(CultureInfo.InvariantCulture))).ToUISize();
			}
		}

		private void CreateStyleWith([NotNull] NumberEntry entry)
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

			var entry = Control as NumberEntry;

			Debug.Assert(entry != null, "entry != null");

			CreateStyleWith(entry);

			var offset = (float) Control.CornerRadius;
			var smallest = (int) (Mathf.Min(offset, Mathf.Min(renderRect.Width*0.5f, renderRect.Height*0.5f)) + 0.5f);
			smallest = Mathf.Max(smallest, 2);
			Style.border = new RectOffset(smallest, smallest, smallest, smallest);

			var newNumber = EditorGUI.FloatField(renderRect.ToUnityRect(), entry.Number, Style);
			if (Equals(newNumber, entry.Number))
				entry.Number = newNumber;
		}
	}
}