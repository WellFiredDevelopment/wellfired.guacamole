using WellFired.Guacamole.Data;
using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Views
{
	public class SliderView : View
	{
		[PublicAPI] public static readonly BindableProperty MinValueProperty = BindableProperty.Create<SliderView, double>(
			0.0,
			BindingMode.TwoWay,
			slider => slider.MinValue
		);

		[PublicAPI] public static readonly BindableProperty MaxValueProperty = BindableProperty.Create<SliderView, double>(
			1.0,
			BindingMode.TwoWay,
			slider => slider.MaxValue
		);

		[PublicAPI] public static readonly BindableProperty ValueProperty = BindableProperty.Create<SliderView, double>(
			0.0,
			BindingMode.TwoWay,
			slider => slider.Value
		);

		[PublicAPI] public static readonly BindableProperty ThumbBackgroundColorProperty = BindableProperty
			.Create<View, UIColor>(
				UIColor.White,
				BindingMode.TwoWay,
				viewBase => viewBase.BackgroundColor
			);

		[PublicAPI] public static readonly BindableProperty ThumbOutlineColorProperty = BindableProperty
			.Create<View, UIColor>(
				default(UIColor),
				BindingMode.TwoWay,
				viewBase => viewBase.OutlineColor
			);

		[PublicAPI] public static readonly BindableProperty ThumbCornerRadiusProperty = BindableProperty
			.Create<TextEntryView, double>(
				0.0,
				BindingMode.TwoWay,
				viewBase => viewBase.CornerRadius
			);

		[PublicAPI] public static readonly BindableProperty ThumbCornerMaskProperty = BindableProperty
			.Create<TextEntryView, CornerMask>(
				CornerMask.All,
				BindingMode.TwoWay,
				viewBase => viewBase.CornerMask
			);

		public SliderView()
		{
			Style = Styling.Styles.Slider.Style;
		}

		[PublicAPI]
		public double MinValue
		{
			get => (double) GetValue(MinValueProperty);
			set => SetValue(MinValueProperty, value);
		}

		[PublicAPI]
		public double MaxValue
		{
			get => (double) GetValue(MaxValueProperty);
			set => SetValue(MaxValueProperty, value);
		}

		[PublicAPI]
		public double Value
		{
			get => (double) GetValue(ValueProperty);
			set => SetValue(ValueProperty, value);
		}

		public UIColor ThumbBackgroundColor
		{
			get => (UIColor) GetValue(ThumbBackgroundColorProperty);
			set => SetValue(ThumbBackgroundColorProperty, value);
		}

		public UIColor ThumbOutlineColor
		{
			get => (UIColor) GetValue(ThumbOutlineColorProperty);
			set => SetValue(ThumbOutlineColorProperty, value);
		}

		public double ThumbCornerRadius
		{
			get => (double) GetValue(ThumbCornerRadiusProperty);
			set => SetValue(ThumbCornerRadiusProperty, value);
		}

		public CornerMask ThumbCornerMask
		{
			get => (CornerMask) GetValue(ThumbCornerMaskProperty);
			set => SetValue(ThumbCornerMaskProperty, value);
		}
	}
}