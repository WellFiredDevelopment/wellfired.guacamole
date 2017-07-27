using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Types;
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
			Style.focused.textColor = label.TextColor.ToUnityColor();
			Style.active.textColor = label.TextColor.ToUnityColor();
			Style.hover.textColor = label.TextColor.ToUnityColor();
			Style.normal.textColor = label.TextColor.ToUnityColor();
			Style.wordWrap = true;
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
		}
	}
}