using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Attributes;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Extensions;
using WellFired.Guacamole.Unity.Editor.NativeControls;
using WellFired.Guacamole.View;
using Debug = System.Diagnostics.Debug;

[assembly : CustomRenderer(typeof(Slider), typeof(SliderRenderer))]
namespace WellFired.Guacamole.Unity.Editor.NativeControls
{
	public class SliderRenderer : BaseRenderer
	{
		private GUIStyle Style { get; set; }
		private GUIStyle ThumbStyle { get; set; }
		private Texture2D ThumbBackgroundTexture { get; set; }

		private void CreateStyleWith([NotNull] ViewBase slider)
		{
			if (Style == null)
				Style = new GUIStyle();

			Style.focused.background = BackgroundTexture;
			Style.active.background = BackgroundTexture;
			Style.hover.background = BackgroundTexture;
			Style.normal.background = BackgroundTexture;

			Style.padding = slider.Padding.ToRectOffset();
		}

		private void CreateThumbStyleWith([NotNull] ViewBase slider)
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

			var slider = Control as Slider;

			Debug.Assert(slider != null, "slider != null");

			CreateStyleWith(slider);
			CreateThumbStyleWith(slider);

			var offset = (float)Control.CornerRadius;
			var smallest = (int)(Mathf.Min(offset, Mathf.Min(renderRect.Width * 0.5f, renderRect.Height * 0.5f)) + 0.5f);
			smallest = Mathf.Max(smallest, 2);
			Style.border = new RectOffset(smallest, smallest, smallest, smallest);

			GUI.Box(renderRect.ToUnityRect(), "", Style);

			slider.Value = GUI.HorizontalSlider(renderRect.ToUnityRect(), (float)slider.Value, (float)slider.MinValue, (float)slider.MaxValue, Style, ThumbStyle);
		}

		public override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(sender, e);

			if (e.PropertyName == Slider.ThumbCornerRadiusProperty.PropertyName ||
				e.PropertyName == Slider.ThumbOutlineColorProperty.PropertyName ||
				e.PropertyName == Slider.ThumbBackgroundColorProperty.PropertyName)
				CreateThumbBackgroundTexture();
		}

		private void UpdateThumbIfNeeded()
		{
			if (ThumbBackgroundTexture == null)
				CreateThumbBackgroundTexture();
		}

		private void CreateThumbBackgroundTexture()
		{
			var slider = Control as Slider;
			Debug.Assert(slider != null, "slider != null");
			ThumbBackgroundTexture = Texture2DExtensions.CreateRoundedTexture(32, 32, slider.ThumbBackgroundColor, slider.ThumbOutlineColor, slider.ThumbCornerRadius, slider.ThumbCornerMask);
		}
	}
}