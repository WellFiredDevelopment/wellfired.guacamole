using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(ToggleView), typeof(ToggleViewRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
	public class ToggleViewRenderer : BaseRenderer
	{
		private readonly ImageCreatorHandler _handler = new ImageCreatorHandler();
		private Texture2D _texture = default(Texture2D);
		public override UISize? NativeSize => UISize.Of(30);

		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);

			var toggleView = (ToggleView)Control;

			var controlState = toggleView.ControlState;
			if (!toggleView.ButtonPressedCommand.CanExecute)
			{
				if (controlState != ControlState.Disabled)
					toggleView.ControlState = ControlState.Disabled;
			}
			else
			{
				if(controlState == ControlState.Disabled)
					toggleView.ControlState = ControlState.Normal;
			}
			
			if (!GUI.Button(UnityRect, _texture, Style))
				return;
			
			toggleView.Click();
		}

		protected override void SetupWithNewStyle()
		{
			base.SetupWithNewStyle();
			
			var toggleView = (ToggleView)Control;
			CreateTextureFor(toggleView);
		}

		public override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(sender, e);
			
			var toggleView = (ToggleView)Control;
			CreateTextureFor(toggleView);
		}

		private async void CreateTextureFor(ToggleView toggleView)
		{
			_texture = default(Texture2D);
			_texture = await _handler.UpdatedImageSource(toggleView.On ? toggleView.OnImageSource : toggleView.OffImageSource);
		}
	}
}