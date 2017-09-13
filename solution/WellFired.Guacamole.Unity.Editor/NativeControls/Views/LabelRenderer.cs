using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(Label), typeof(LabelRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
	public class LabelRenderer : BaseRenderer
	{
		public override UISize? NativeSize
		{
			get
			{
				var label = (Label)Control;
				return Style.CalcSize(new GUIContent(label.Text)).ToUISize();
			}
		}

		protected override void SetupWithNewStyle()
		{
			base.SetupWithNewStyle();
			
			var label = (Label)Control;
			Style.alignment = UITextAlignExtensions.Combine(label.HorizontalTextAlign, label.VerticalTextAlign);
			Style.clipping = label.Clipping.ToUnityClipping();
			Style.richText = label.RichText;
			Style.focused.textColor = label.TextColor.ToUnityColor();
			Style.active.textColor = label.TextColor.ToUnityColor();
			Style.hover.textColor = label.TextColor.ToUnityColor();
			Style.normal.textColor = label.TextColor.ToUnityColor();
			Style.fontSize = label.FontSize;
			Style.wordWrap = label.WordWrap;
		}

		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);

			var label = (Label)Control;
			EditorGUI.LabelField(UnityRect, label.Text, Style);
		}

		public override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(sender, e);

			var label = (Label)Control;
			
			if (e.PropertyName == Label.TextColorProperty.PropertyName)
			{
				Style.focused.textColor = label.TextColor.ToUnityColor();
				Style.active.textColor = label.TextColor.ToUnityColor();
				Style.hover.textColor = label.TextColor.ToUnityColor();
				Style.normal.textColor = label.TextColor.ToUnityColor();
			}
			if(e.PropertyName == Label.HorizontalTextAlignProperty.PropertyName || e.PropertyName == Label.VerticalTextAlignProperty.PropertyName)
				Style.alignment = UITextAlignExtensions.Combine(label.HorizontalTextAlign, label.VerticalTextAlign);
			if (e.PropertyName == Label.WordWrapProperty.PropertyName)
				Style.wordWrap = label.WordWrap;
			if (e.PropertyName == Label.ClippingProperty.PropertyName)
				Style.clipping = label.Clipping.ToUnityClipping();
			if (e.PropertyName == Label.RichTextProperty.PropertyName)
				Style.richText = label.RichText;
			if (e.PropertyName == Label.FontSizeProperty.PropertyName)
				Style.fontSize = label.FontSize;
		}
	}
}