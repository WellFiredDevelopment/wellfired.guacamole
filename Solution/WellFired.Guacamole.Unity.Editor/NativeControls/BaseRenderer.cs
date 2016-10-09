using System.ComponentModel;
using UnityEngine;
using WellFired.Guacamole.Renderer;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole.Unity.Editor.NativeControls
{
	public abstract class BaseRenderer : INativeRenderer
	{
		protected Texture2D BackgroundTexture { get; private set; }
		public virtual UISize? NativeSize => null;

		public ViewBase Control { protected get; set; }

		public virtual void Create()
		{
			Control.ControlState = Control.Enabled ? ControlState.Normal : ControlState.Disabled;
		}

		public virtual void Render(UIRect renderRect)
		{
			if (Control.ControlState != ControlState.Disabled)
			{
				if (Control.ControlState != ControlState.Active)
					if ((Event.current.type == EventType.MouseDown) && renderRect.ToUnityRect().Contains(Event.current.mousePosition))
						Control.ControlState = ControlState.Active;
					else if (renderRect.ToUnityRect().Contains(Event.current.mousePosition))
						Control.ControlState = ControlState.Hover;
					else
						Control.ControlState = ControlState.Normal;

				if (Event.current.rawType == EventType.MouseUp)
					Control.ControlState = ControlState.Normal;
			}

			if (BackgroundTexture == null)
				CreateBackgroundTexture();
		}

		public virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if ((e.PropertyName == ViewBase.CornerRadiusProperty.PropertyName) ||
			    (e.PropertyName == ViewBase.OutlineColorProperty.PropertyName) ||
			    (e.PropertyName == ViewBase.BackgroundColorProperty.PropertyName) ||
			    (e.PropertyName == ViewBase.ControlStateProperty.PropertyName))
				CreateBackgroundTexture();

			if (e.PropertyName == ViewBase.EnabledProperty.PropertyName)
				Control.ControlState = Control.Enabled ? ControlState.Normal : ControlState.Disabled;
		}

		private void CreateBackgroundTexture()
		{
			BackgroundTexture = Texture2DExtensions.CreateRoundedTexture(64, 64, Control.BackgroundColor, Control.OutlineColor,
				Control.CornerRadius, Control.CornerMask);
		}

		protected UISize Constrain(UISize requestedSize)
		{
			if (requestedSize.Width < Control.MinSize.Width)
				requestedSize.Width = Control.MinSize.Width;
			if (requestedSize.Height < Control.MinSize.Height)
				requestedSize.Height = Control.MinSize.Height;
			if (requestedSize.Width > Control.MaxSize.Width)
				requestedSize.Width = Control.MaxSize.Width;
			if (requestedSize.Height > Control.MaxSize.Height)
				requestedSize.Height = Control.MaxSize.Height;

			return requestedSize;
		}
	}
}