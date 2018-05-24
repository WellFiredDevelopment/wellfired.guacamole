using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(LabelView), typeof(LabelViewRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
	public class LabelViewRenderer : BaseRenderer
	{
		public override UISize? NativeSize
		{
			get
			{
				var label = (LabelView)Control;
				var content = new GUIContent(label.Text);
				var size = Style.CalcSize(content);
				
				// If we don't have word wrap on, we return the calculated size, this should always work when it's only considering the X Axis.
				return !label.WordWrap ? size.ToUISize() : TextViewExtensions.CalculateNativeSizeWithWordWrap(Control, size, content, Style);
			}
		}

		protected override void SetupWithNewStyle()
		{
			base.SetupWithNewStyle();
			
			var label = (LabelView)Control;
			Style.alignment = UITextAlignExtensions.Combine(label.HorizontalTextAlign, label.VerticalTextAlign);
			Style.clipping = label.Clipping.ToUnityClipping();
			Style.focused.textColor = label.TextColor.ToUnityColor();
			Style.active.textColor = label.TextColor.ToUnityColor();
			Style.hover.textColor = label.TextColor.ToUnityColor();
			Style.normal.textColor = label.TextColor.ToUnityColor();
			Style.fontSize = label.FontSize;
			Style.wordWrap = label.WordWrap;
			Style.richText = true;
		}

		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);

			var label = (LabelView)Control;
			
			EditorGUI.LabelField(UnityRect, label.Text, Style);

			if (!label.WordWrap)
				return;
			
			var content = new GUIContent(label.Text);
			if(TextViewExtensions.HasHeightChanged(renderRect, content, Style))
				Control.InvalidateRectRequest();
		}

		public override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(sender, e);

			var label = (LabelView)Control;
			
			if (e.PropertyName == LabelView.TextColorProperty.PropertyName)
			{
				Style.focused.textColor = label.TextColor.ToUnityColor();
				Style.active.textColor = label.TextColor.ToUnityColor();
				Style.hover.textColor = label.TextColor.ToUnityColor();
				Style.normal.textColor = label.TextColor.ToUnityColor();
			}
			if(e.PropertyName == LabelView.HorizontalTextAlignProperty.PropertyName || e.PropertyName == LabelView.VerticalTextAlignProperty.PropertyName)
				Style.alignment = UITextAlignExtensions.Combine(label.HorizontalTextAlign, label.VerticalTextAlign);
			if (e.PropertyName == LabelView.WordWrapProperty.PropertyName)
				Style.wordWrap = label.WordWrap;
			if (e.PropertyName == LabelView.ClippingProperty.PropertyName)
				Style.clipping = label.Clipping.ToUnityClipping();
			if (e.PropertyName == LabelView.FontSizeProperty.PropertyName)
				Style.fontSize = label.FontSize;
		}
	}
}