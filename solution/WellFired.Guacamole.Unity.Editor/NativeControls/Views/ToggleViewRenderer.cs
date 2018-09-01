﻿using System;
using System.ComponentModel;
using System.Threading.Tasks;
 using UnityEditor;
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
		private enum ToggleState
		{
			On,
			Off
		}
		
		private readonly ImageLoader _imageLoader = new ImageLoader();
		private Texture2D _onTexture;
		private Texture2D _offTexture;
		private Texture2D _currentTexture;
		public override UISize? NativeSize => UISize.Of(18);
		
		private bool _instantiateNineSliceData;
		private UIPadding _nineSliceRect;
		
		private UIPadding? _onNineSliceRect;
		private UIPadding? _offNineSliceRect;

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
			
			EditorGUIUtility.AddCursorRect(UnityRect, MouseCursor.Link);

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

			if (_instantiateNineSliceData)
			{
				_instantiateNineSliceData = false;
				Style.border = new RectOffset(
					_nineSliceRect.Left,
					_nineSliceRect.Top,
					_nineSliceRect.Right,
					_nineSliceRect.Bottom);
			}
			
			if (!GUI.Button(UnityRect, _currentTexture, Style))
				return;
			
			toggleView.Click();
		}

		public override void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnViewPropertyChanged(sender, e);
			
			var toggleView = (ToggleView)Control;

			if (e.PropertyName == ToggleView.OnProperty.PropertyName)
				UpdateCurrentTexture(toggleView);

			if (e.PropertyName == ToggleView.OnImageSourceProperty.PropertyName)
				TaskEx.Run(() => UpdateTexture(ToggleState.On));
			
			if(e.PropertyName == ToggleView.OffImageSourceProperty.PropertyName)
				TaskEx.Run(() => UpdateTexture(ToggleState.Off));
		}

		private async void UpdateTexture(ToggleState toggleState)
		{
			var onState = toggleState == ToggleState.On;
			var toggleView = (ToggleView) Control;

			var imageSource = onState ? toggleView.OnImageSource : toggleView.OffImageSource;
			var isStillAwaited = onState ? (Func<bool>) (() => toggleView.OnImageSource == imageSource) : () => toggleView.OffImageSource == imageSource;

			var texture = await _imageLoader.LoadImage(imageSource, isStillAwaited);
			
			if (texture == default(Texture2D)) 
				return;
			 
			if(onState)
				_onTexture = texture;
			else
				_offTexture = texture;

			if (onState)
				_onNineSliceRect = imageSource.NineSliceDefinition;
			else
				_offNineSliceRect = imageSource.NineSliceDefinition;

			UpdateCurrentTexture(toggleView);
			_currentTexture = toggleView.On ? _onTexture : _offTexture;
		}

		private void UpdateCurrentTexture(ToggleView toggleView)
		{
			var previousTexture = _currentTexture;
			_currentTexture = toggleView.On ? _onTexture : _offTexture;

			if (previousTexture == _currentTexture)
				return;

			if (toggleView.On && _onNineSliceRect.HasValue)
			{
				_instantiateNineSliceData = true;
				_nineSliceRect = _onNineSliceRect.Value;	
			}
			else if (!toggleView.On && _offNineSliceRect.HasValue)
			{
				_instantiateNineSliceData = true;
				_nineSliceRect = _offNineSliceRect.Value;	
			}
		}
	}
}