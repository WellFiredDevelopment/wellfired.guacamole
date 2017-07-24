using System.ComponentModel;
using UnityEngine;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Renderer;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
	public abstract class BaseRenderer : INativeRenderer
	{
		private UIRect _prevRect;
		private const string EmptyStyleName = "EMPTYSTYLE";
		private GUIStyle _style = new GUIStyle { name = EmptyStyleName };
		
		private Texture2D BackgroundTexture { get; set; }
		public virtual UISize? NativeSize => null;
		public View Control { protected get; set; }
		protected Rect UnityRect { get; private set; }

		protected GUIStyle Style
		{
			get
			{
				if (_style.name != EmptyStyleName) 
					return _style;

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
			if (renderRect != _prevRect)
				UnityRect = renderRect.ToUnityRect();
			_prevRect = renderRect;
			
			if (Control.ControlState != ControlState.Disabled)
			{
				if (Control.ControlState != ControlState.Active)
					if ((UnityEngine.Event.current.type == EventType.MouseDown) && UnityRect.Contains(UnityEngine.Event.current.mousePosition))
						Control.ControlState = ControlState.Active;
					else if (UnityRect.Contains(UnityEngine.Event.current.mousePosition))
						Control.ControlState = ControlState.Hover;
					else
						Control.ControlState = ControlState.Normal;

				if (UnityEngine.Event.current.rawType == EventType.MouseUp)
					Control.ControlState = ControlState.Normal;
			}

			var offset = (float) Control.CornerRadius;
			var smallest = (int) (Mathf.Min(offset, Mathf.Min(UnityRect.width * 0.5f, UnityRect.height * 0.5f)) + 0.5f);
			smallest = Mathf.Max(smallest, 2);
			Style.border = new RectOffset(smallest, smallest, smallest, smallest);

			if (BackgroundTexture == null)
				CreateBackgroundTexture();

			GUI.SetNextControlName(Control.Id);
		}

		public virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == View.CornerRadiusProperty.PropertyName ||
			    e.PropertyName == View.OutlineColorProperty.PropertyName ||
			    e.PropertyName == View.BackgroundColorProperty.PropertyName ||
			    e.PropertyName == View.ControlStateProperty.PropertyName)
			{
				CreateBackgroundTexture();
				ResetStyle();
			}

			if (e.PropertyName == View.EnabledProperty.PropertyName)
			{
				Control.ControlState = Control.Enabled ? ControlState.Normal : ControlState.Disabled;
				ResetStyle();
			}
			
			if(e.PropertyName == View.PaddingProperty.PropertyName)
				Style.padding = Control.Padding.ToRectOffset();
		}

		public void ResetStyle()
		{
			_style.name = EmptyStyleName;
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

		private void CreateBackgroundTexture()
		{
			BackgroundTexture = Texture2DExtensions.CreateRoundedTexture(64, 64, Control.BackgroundColor, Control.OutlineColor, Control.CornerRadius, Control.CornerMask);
		}
	}
}