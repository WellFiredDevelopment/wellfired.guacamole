using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
	public class Slider : View
	{
		[PublicAPI] public static readonly BindableProperty MinValueProperty = BindableProperty.Create<Slider, double>(
			0.0,
			BindingMode.TwoWay,
			slider => slider.MinValue
		);

		[PublicAPI] public static readonly BindableProperty MaxValueProperty = BindableProperty.Create<Slider, double>(
			1.0,
			BindingMode.TwoWay,
			slider => slider.MaxValue
		);

		[PublicAPI] public static readonly BindableProperty ValueProperty = BindableProperty.Create<Slider, double>(
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
			.Create<TextEntry, double>(
				0.0,
				BindingMode.TwoWay,
				viewBase => viewBase.CornerRadius
			);

		[PublicAPI] public static readonly BindableProperty ThumbCornerMaskProperty = BindableProperty
			.Create<TextEntry, CornerMask>(
				CornerMask.All,
				BindingMode.TwoWay,
				viewBase => viewBase.CornerMask
			);

		public Slider()
		{
			Style = Styling.Styles.Slider.Style;
		}

		[PublicAPI]
		public double MinValue
		{
			get { return (double) GetValue(MinValueProperty); }
			set { SetValue(MinValueProperty, value); }
		}

		[PublicAPI]
		public double MaxValue
		{
			get { return (double) GetValue(MaxValueProperty); }
			set { SetValue(MaxValueProperty, value); }
		}

		[PublicAPI]
		public double Value
		{
			get { return (double) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		public UIColor ThumbBackgroundColor
		{
			get { return (UIColor) GetValue(ThumbBackgroundColorProperty); }
			set { SetValue(ThumbBackgroundColorProperty, value); }
		}

		public UIColor ThumbOutlineColor
		{
			get { return (UIColor) GetValue(ThumbOutlineColorProperty); }
			set { SetValue(ThumbOutlineColorProperty, value); }
		}

		public double ThumbCornerRadius
		{
			get { return (double) GetValue(ThumbCornerRadiusProperty); }
			set { SetValue(ThumbCornerRadiusProperty, value); }
		}

		public CornerMask ThumbCornerMask
		{
			get { return (CornerMask) GetValue(ThumbCornerMaskProperty); }
			set { SetValue(ThumbCornerMaskProperty, value); }
		}
	}
}