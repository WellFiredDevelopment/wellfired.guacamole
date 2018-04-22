using System.ComponentModel;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(ToggleView), typeof(ToggleViewRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
	public class ToggleViewRenderer : BaseRenderer
	{
		private readonly ImageCreatorHandler _handler = new ImageCreatorHandler();
		private Texture2D _onTexture;
		private Texture2D _offTexture;
		private Texture2D _currentTexture;
		public override UISize? NativeSize => UISize.Of(18);

		public override async void Create()
		{
			base.Create();
			
			var toggleView = (ToggleView)Control;
			_onTexture = await _handler.UpdatedImageSource(toggleView.OnImageSource);
			_offTexture = await _handler.UpdatedImageSource(toggleView.OffImageSource);
		}

		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);

			var toggleView = (ToggleView)Control;
			
			_currentTexture = toggleView.On ? _onTexture : _offTexture;

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
			
			if (!GUI.Button(UnityRect, _currentTexture, Style))
				return;
			
			toggleView.Click();
		}

		public override async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(sender, e);
			
			var toggleView = (ToggleView)Control;

			if (e.PropertyName == ToggleView.OnImageSourceProperty.PropertyName)
				_onTexture = await _handler.UpdatedImageSource(toggleView.OnImageSource);
			
			if(e.PropertyName == ToggleView.OffImageSourceProperty.PropertyName)
				_offTexture = await _handler.UpdatedImageSource(toggleView.OffImageSource);
		}
	}
}