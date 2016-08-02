using System.ComponentModel;
using UnityEngine;

namespace WellFired.Guacamole.Unity.Editor
{
	public abstract class BaseRenderer : INativeRenderer
	{
		public ViewBase Control 
		{
			get;
			set;
		}

		protected Texture2D BackgroundTexture { get; private set; }
		protected Texture2D HoverBackgroundTexture { get; private set; }
		protected Texture2D ActiveBackgroundTexture { get; private set; }

		public virtual void Render(UIRect renderRect)
		{
			if(BackgroundTexture == null)
				CreateBackgroundTexture();
			if(HoverBackgroundTexture == null)
				CreateHoverBackgroundTexture();
			if (ActiveBackgroundTexture == null)
				CreateActiveBackgroundTexture();
		}

		public virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if(e.PropertyName == ViewBase.CornerRadiusProperty.PropertyName ||
			   e.PropertyName == ViewBase.OutlineColorProperty.PropertyName)
			{
				CreateBackgroundTexture();
				CreateHoverBackgroundTexture();
				CreateActiveBackgroundTexture();
			}

			if (e.PropertyName == ViewBase.BackgroundColorProperty.PropertyName)
				CreateBackgroundTexture();

			if (e.PropertyName == ViewBase.HoverBackgroundColorProperty.PropertyName)
				CreateHoverBackgroundTexture();

			if (e.PropertyName == ViewBase.ActiveBackgroundColorProperty.PropertyName)
				CreateHoverBackgroundTexture();
		}

		private void CreateBackgroundTexture()
		{
			BackgroundTexture = Texture2DExtensions.CreateRoundedTexture(
				64,
				64,
				Control.BackgroundColor,
				Control.OutlineColor,
				Control.CornerRadius,
				Control.CornerMask);
		}

		private void CreateHoverBackgroundTexture()
		{
			var backgroundColor = Control.HoverBackgroundColor == default(UIColor)
				                      ? Control.BackgroundColor
				                      : Control.HoverBackgroundColor;
			HoverBackgroundTexture = Texture2DExtensions.CreateRoundedTexture(
					  64,
					  64,
					  backgroundColor,
					  Control.OutlineColor == default(UIColor) ? backgroundColor : Control.OutlineColor,
					  Control.CornerRadius,
					  Control.CornerMask);
		}

		private void CreateActiveBackgroundTexture()
		{
			var backgroundColor = Control.ActiveBackgroundColor == default(UIColor)
				                      ? Control.BackgroundColor
				                      : Control.ActiveBackgroundColor;
			ActiveBackgroundTexture = Texture2DExtensions.CreateRoundedTexture(
					  64,
					  64,
					  backgroundColor,
					  Control.OutlineColor == default(UIColor) ? backgroundColor : Control.OutlineColor,
					  Control.CornerRadius,
					  Control.CornerMask);
		}
	}
}