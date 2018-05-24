using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Platforms;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(ButtonView), typeof(ButtonViewRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
	public class ButtonViewRenderer : BaseRenderer
	{
		public override UISize? NativeSize
		{
			get
			{
				var button = Control as ButtonView;
				// ReSharper disable once PossibleNullReferenceException
				return Style.CalcSize(new GUIContent(button.Text)).ToUISize();
			}
		}

		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);

			var button = (ButtonView)Control;

			EditorGUIUtility.AddCursorRect(UnityRect, MouseCursor.Link);

			var controlState = button.ControlState;
			if (!button.ButtonPressedCommand.CanExecute)
			{
				if (controlState != ControlState.Disabled)
					button.ControlState = ControlState.Disabled;
			}
			else
			{
				if(controlState == ControlState.Disabled)
					button.ControlState = ControlState.Normal;
			}

			if (!GUI.Button(UnityRect, button.Text, Style))
				return;
			
			MainThreadRunner.ExecuteBeforeLayout(() => button.Click(0));
		}

		protected override void SetupWithNewStyle()
		{
			base.SetupWithNewStyle();
			
			var button = (ButtonView)Control;
			Style.alignment = UITextAlignExtensions.Combine(button.HorizontalTextAlign, button.VerticalTextAlign);
			Style.focused.textColor = button.TextColor.ToUnityColor();
			Style.active.textColor = button.TextColor.ToUnityColor();
			Style.hover.textColor = button.TextColor.ToUnityColor();
			Style.normal.textColor = button.TextColor.ToUnityColor();
		}

		public override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(sender, e);
			
			var button = (ButtonView)Control;
			if (e.PropertyName == ButtonView.HorizontalTextAlignProperty.PropertyName || e.PropertyName == ButtonView.VerticalTextAlignProperty.PropertyName)
				Style.alignment = UITextAlignExtensions.Combine(button.HorizontalTextAlign, button.VerticalTextAlign);

			if (e.PropertyName == ButtonView.TextColorProperty.PropertyName)
			{
				Style.focused.textColor = button.TextColor.ToUnityColor();
				Style.active.textColor = button.TextColor.ToUnityColor();
				Style.hover.textColor = button.TextColor.ToUnityColor();
				Style.normal.textColor = button.TextColor.ToUnityColor();
			}
		}
	}
}