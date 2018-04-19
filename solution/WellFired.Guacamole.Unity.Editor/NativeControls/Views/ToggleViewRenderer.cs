using System;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Image;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(ToggleView), typeof(ToggleViewRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
	public class ToggleViewRenderer : BaseRenderer
	{
		private enum ToggleState
		{
			On,
			Off
		}
		
		private readonly ImageCreatorHandler _handler = new ImageCreatorHandler();
		private Texture2D _onTexture;
		private Texture2D _offTexture;
		private Texture2D _currentTexture;
		private readonly object _textureLoadingLock = new object();
		public override UISize? NativeSize => UISize.Of(18);

		public override void Create()
		{
			base.Create();
			
			LoadTextures();
		}

		private void LoadTextures()
		{
			TaskEx.Run(() => UpdateTexture(ToggleState.On));
			TaskEx.Run(() => UpdateTexture(ToggleState.Off));
		}

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
			
			if (!GUI.Button(UnityRect, _currentTexture, Style))
				return;
			
			toggleView.Click();
		}

		public override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(sender, e);
			
			var toggleView = (ToggleView)Control;
			
			if(e.PropertyName == ToggleView.OnProperty.PropertyName)
				_currentTexture = toggleView.On ? _onTexture : _offTexture;

			if (e.PropertyName == ToggleView.OnImageSourceProperty.PropertyName)
				TaskEx.Run(() => UpdateTexture(ToggleState.On));
			
			if(e.PropertyName == ToggleView.OffImageSourceProperty.PropertyName)
				TaskEx.Run(() => UpdateTexture(ToggleState.Off));
		}

		

		private async void UpdateTexture(ToggleState toggleState)
		{
			var onState = toggleState == ToggleState.On;
			var toggleView = (ToggleView) Control;

			var getCurrentSource = onState ? (Func<IImageSource>) (() => toggleView.OnImageSource) : (() => toggleView.OffImageSource);

			var texture = await LoadTexture(getCurrentSource);
			
			if (texture == default(Texture2D)) 
				return;
			
			if(onState)
				_onTexture = texture;
			else
			{
				_offTexture = texture;
			}
				
			_currentTexture = toggleView.On ? _onTexture : _offTexture;
		}
		
		private async Task<Texture2D> LoadTexture(Func<IImageSource> getCurrentSource)
		{
			var source = getCurrentSource();
			
			//if the texture is already loading, then we return a null texture.
			if (source.InProgress)
				return default(Texture2D);

			var texture = await _handler.UpdatedImageSource(source);
			
			lock (_textureLoadingLock)
			{
				//If the source of the toggle changed while we were loading it, then we return a null texture.
				return source != getCurrentSource() ? 
					default(Texture2D) : 
					texture;
			}
		}
	}
}