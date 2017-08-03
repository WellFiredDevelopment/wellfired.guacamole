using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;
using Debug = System.Diagnostics.Debug;

[assembly: CustomRenderer(typeof(TextEntry), typeof(TextEntryRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
	public class TextEntryRenderer : BaseRenderer
	{
		public override UISize? NativeSize
		{
			get
			{
				var entry = Control as TextEntry;
				Debug.Assert(entry != null, $"{nameof(entry)} != null");
				return Style.CalcSize(new GUIContent(entry.Text)).ToUISize();
			}
		}

		protected override void SetupWithNewStyle()
		{
			base.SetupWithNewStyle();

			var entry = (TextEntry)Control;
			Style.alignment = UITextAlignExtensions.Combine(entry.HorizontalTextAlign, entry.VerticalTextAlign);
			Style.focused.textColor = entry.TextColor.ToUnityColor();
			Style.active.textColor = entry.TextColor.ToUnityColor();
			Style.hover.textColor = entry.TextColor.ToUnityColor();
			Style.normal.textColor = entry.TextColor.ToUnityColor();
		}

		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);

			var entry = (TextEntry)Control;
			entry.Text = EditorGUI.TextField(UnityRect, entry.Text, Style);
		}

		public override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(sender, e);

			var entry = (TextEntry)Control;
			if (e.PropertyName == TextEntry.HorizontalTextAlignProperty.PropertyName || e.PropertyName == TextEntry.VerticalTextAlignProperty.PropertyName)
				Style.alignment = UITextAlignExtensions.Combine(entry.HorizontalTextAlign, entry.VerticalTextAlign);

			if (e.PropertyName == TextEntry.TextColorProperty.PropertyName)
			{
				Style.focused.textColor = entry.TextColor.ToUnityColor();
				Style.active.textColor = entry.TextColor.ToUnityColor();
				Style.hover.textColor = entry.TextColor.ToUnityColor();
				Style.normal.textColor = entry.TextColor.ToUnityColor();
			}
		}
	}
}