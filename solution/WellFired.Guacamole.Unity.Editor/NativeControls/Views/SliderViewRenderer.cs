using System.ComponentModel;
using UnityEngine;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls.Views;
using WellFired.Guacamole.Views;

[assembly: CustomRenderer(typeof(SliderView), typeof(SliderViewRenderer))]

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
	public class SliderViewRenderer : BaseRenderer
	{
		private GUIStyle ThumbStyle { get; set; }
		private Texture2D ThumbBackgroundTexture { get; set; }

	    public override UISize? NativeSize => Style.CalcSize(new GUIContent()).ToUISize();

		private void CreateThumbStyleWith()
		{
			if (ThumbStyle == null)
				ThumbStyle = new GUIStyle();

			ThumbStyle.focused.background = ThumbBackgroundTexture;
			ThumbStyle.active.background = ThumbBackgroundTexture;
			ThumbStyle.hover.background = ThumbBackgroundTexture;
			ThumbStyle.normal.background = ThumbBackgroundTexture;

			const int padding = 4;
			ThumbStyle.padding = new RectOffset(padding, padding, padding, padding);
		}

		public override void Render(UIRect renderRect)
		{
			base.Render(renderRect);

			UpdateThumbIfNeeded();

			var slider = (SliderView)Control;
			
			CreateThumbStyleWith();

			var newValue = GUI.HorizontalSlider(UnityRect, (float) slider.Value, (float) slider.MinValue,
				(float) slider.MaxValue, Style, ThumbStyle);

			if (Control.Enabled)
				slider.Value = newValue;
		}

		public override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(sender, e);

			if (e.PropertyName == SliderView.ThumbCornerRadiusProperty.PropertyName ||
			    e.PropertyName == SliderView.ThumbOutlineColorProperty.PropertyName ||
			    e.PropertyName == SliderView.ThumbBackgroundColorProperty.PropertyName ||
			    e.PropertyName == View.ControlStateProperty.PropertyName)
				CreateThumbBackgroundTexture();
		}

		private void UpdateThumbIfNeeded()
		{
			if (ThumbBackgroundTexture == null)
				CreateThumbBackgroundTexture();
		}

		private void CreateThumbBackgroundTexture()
		{
			var slider = Control as SliderView;
			// ReSharper disable once PossibleNullReferenceException
			ThumbBackgroundTexture = Texture2DExtensions.CreateRoundedTexture(32, 32, slider.ThumbBackgroundColor, slider.ThumbOutlineColor, slider.ThumbCornerRadius, 1, slider.ThumbCornerMask, OutlineMask.All);
		}
	}
}