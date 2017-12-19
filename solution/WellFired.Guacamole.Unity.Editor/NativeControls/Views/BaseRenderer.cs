using System.ComponentModel;
using UnityEngine;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Renderer;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
	public abstract class BaseRenderer : INativeRenderer
	{
		private bool _firstRender;
		private UIRect _prevRect;
		private bool _resetStyle = true;
		private GUIStyle _style = GUIStyle.none;
		
		private Texture2D BackgroundTexture { get; set; }
		public virtual UISize? NativeSize => null;
		public View Control { protected get; set; }
		protected Rect UnityRect { get; private set; }

		protected GUIStyle Style
		{
			get
			{
				if (!_resetStyle) 
					return _style;
				
				_resetStyle = false;
				SetupWithNewStyle();
				return _style;
			}
		}

		public virtual void Create()
		{
			Control.ControlState = Control.Enabled ? ControlState.Normal : ControlState.Disabled;
		}

		protected virtual void SetupWithNewStyle()
		{
			_style = new GUIStyle
			{
				focused = {background = BackgroundTexture},
				active = {background = BackgroundTexture},
				hover = {background = BackgroundTexture},
				normal = {background = BackgroundTexture},
				padding = Control.Padding.ToRectOffset()
			};
		}

		public virtual void Render(UIRect renderRect)
		{
			if (!_firstRender)
			{
				_resetStyle = true;
				_firstRender = true;
				CreateBackgroundTexture();
			}
			
			if (renderRect != _prevRect)
				UnityRect = renderRect.ToUnityRect();
			_prevRect = renderRect;

			var controlState = Control.ControlState;
			var canUpdateState = controlState != ControlState.Disabled;
						
			if (canUpdateState)
			{
				var newControlState = controlState;
				if (controlState != ControlState.Active)
				{
					if (UnityEngine.Event.current.type == EventType.MouseDown && UnityRect.Contains(UnityEngine.Event.current.mousePosition))
						newControlState = ControlState.Active;
					else if (UnityRect.Contains(UnityEngine.Event.current.mousePosition))
						newControlState = ControlState.Hover;
					else
						newControlState = ControlState.Normal;
				}

				if (UnityEngine.Event.current.rawType == EventType.MouseUp && controlState != ControlState.Normal)
					newControlState = ControlState.Normal;

				if (controlState != newControlState)
					Control.ControlState = newControlState;
			}

			var thickness = (int) Control.OutlineThickness;
			var offset = (float) Control.CornerRadius + thickness;
			var smallest = (int) (Mathf.Min(offset, Mathf.Min(UnityRect.width * 0.5f, UnityRect.height * 0.5f)) + 0.5f);
			smallest = Mathf.Max(smallest, 2);
			Style.border = new RectOffset(smallest, smallest, smallest, smallest);

			GUI.SetNextControlName(Control.Id);
		}

		public virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == View.CornerRadiusProperty.PropertyName ||
			    e.PropertyName == View.OutlineColorProperty.PropertyName ||
			    e.PropertyName == View.BackgroundColorProperty.PropertyName ||
			    e.PropertyName == View.OutlineThicknessProperty.PropertyName ||
			    e.PropertyName == View.OutlineMaskProperty.PropertyName ||
			    e.PropertyName == View.CornerMaskProperty.PropertyName)
			{
				CreateBackgroundTexture();
				ResetStyle();
			}

			if (e.PropertyName == View.EnabledProperty.PropertyName)
			{
				Control.ControlState = Control.Enabled ? ControlState.Normal : ControlState.Disabled;
				CreateBackgroundTexture();
				ResetStyle();
			}
			
			if(e.PropertyName == View.PaddingProperty.PropertyName)
				Style.padding = Control.Padding.ToRectOffset();
		}

		public void ResetStyle()
		{
			_resetStyle = true;
		}

		public void FocusControl()
		{
			GUI.FocusControl(Control.Id);
		}

		public virtual bool PushMaskStack(UIRect maskRect)
		{
			return false;
		}

		public virtual void PopMaskStack()
		{
		}

		public virtual void RecycleWithNewBindingContext()
		{
			
		}

		private void CreateBackgroundTexture()
		{
			if (!_firstRender)
				return;
			
			BackgroundTexture = Texture2DExtensions.CreateRoundedTexture(64, 64, Control.BackgroundColor, Control.OutlineColor, Control.CornerRadius, Control.OutlineThickness, Control.CornerMask, Control.OutlineMask);
		}
	}
}