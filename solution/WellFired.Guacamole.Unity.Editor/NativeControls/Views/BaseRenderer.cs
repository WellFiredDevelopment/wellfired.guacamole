using System.ComponentModel;
using UnityEngine;
using WellFired.Guacamole.Renderer;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
	public abstract class BaseRenderer : INativeRenderer
	{
		protected Texture2D BackgroundTexture { get; private set; }
		public virtual UISize? NativeSize => null;

		public View Control { protected get; set; }

		public virtual void Create()
		{
			Control.ControlState = Control.Enabled ? ControlState.Normal : ControlState.Disabled;
		}

		public virtual void Render(UIRect renderRect)
		{
			if (Control.ControlState != ControlState.Disabled)
			{
				if (Control.ControlState != ControlState.Active)
					if ((UnityEngine.Event.current.type == EventType.MouseDown) && renderRect.ToUnityRect().Contains(UnityEngine.Event.current.mousePosition))
						Control.ControlState = ControlState.Active;
					else if (renderRect.ToUnityRect().Contains(UnityEngine.Event.current.mousePosition))
						Control.ControlState = ControlState.Hover;
					else
						Control.ControlState = ControlState.Normal;

				if (UnityEngine.Event.current.rawType == EventType.MouseUp)
					Control.ControlState = ControlState.Normal;
			}

			if (BackgroundTexture == null)
				CreateBackgroundTexture();

			GUI.SetNextControlName(Control.Id);
		}

		public virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if ((e.PropertyName == View.CornerRadiusProperty.PropertyName) ||
			    (e.PropertyName == View.OutlineColorProperty.PropertyName) ||
			    (e.PropertyName == View.BackgroundColorProperty.PropertyName) ||
			    (e.PropertyName == View.ControlStateProperty.PropertyName))
			{
				CreateBackgroundTexture();
				ResetStyle();
			}

			if (e.PropertyName == View.EnabledProperty.PropertyName)
			{
				Control.ControlState = Control.Enabled ? ControlState.Normal : ControlState.Disabled;
				ResetStyle();
			}
		}

		public abstract void ResetStyle();

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