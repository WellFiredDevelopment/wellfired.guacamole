using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.View
{
	public class Slider : ViewBase
	{
		[PublicAPI] public static readonly BindableProperty MinValueProperty = BindableProperty.Create<Slider, double>(
			defaultValue: 0.0,
			bindingMode: BindingMode.TwoWay,
			getter: slider => slider.MinValue
			);

		[PublicAPI] public static readonly BindableProperty MaxValueProperty = BindableProperty.Create<Slider, double>(
			defaultValue: 1.0,
			bindingMode: BindingMode.TwoWay,
			getter: slider => slider.MaxValue
			);

		[PublicAPI] public static readonly BindableProperty ValueProperty = BindableProperty.Create<Slider, double>(
			defaultValue: 0.0,
			bindingMode: BindingMode.TwoWay,
			getter: slider => slider.Value
			);

		[PublicAPI]
		public static readonly BindableProperty ThumbBackgroundColorProperty = BindableProperty
			.Create<ViewBase, UIColor>(
				defaultValue: UIColor.White,
				bindingMode: BindingMode.TwoWay,
				getter: viewBase => viewBase.BackgroundColor
			);

		[PublicAPI]
		public static readonly BindableProperty ThumbOutlineColorProperty = BindableProperty
			.Create<ViewBase, UIColor>(
				default(UIColor),
				bindingMode: BindingMode.TwoWay,
				getter: viewBase => viewBase.OutlineColor
			);

		[PublicAPI]
		public static readonly BindableProperty ThumbCornerRadiusProperty = BindableProperty
			.Create<TextEntry, double>(
				defaultValue: 0.0,
				bindingMode: BindingMode.TwoWay,
				getter: viewBase => viewBase.CornerRadius
			);

		[PublicAPI]
		public static readonly BindableProperty ThumbCornerMaskProperty = BindableProperty
			.Create<TextEntry, CornerMask>(
				defaultValue: CornerMask.All,
				bindingMode: BindingMode.TwoWay,
				getter: viewBase => viewBase.CornerMask
			);

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
			get { return (UIColor)GetValue(ThumbBackgroundColorProperty); }
			set { SetValue(ThumbBackgroundColorProperty, value); }
		}

		public UIColor ThumbOutlineColor
		{
			get { return (UIColor)GetValue(ThumbOutlineColorProperty); }
			set { SetValue(ThumbOutlineColorProperty, value); }
		}

		public double ThumbCornerRadius
		{
			get { return (double)GetValue(ThumbCornerRadiusProperty); }
			set { SetValue(ThumbCornerRadiusProperty, value); }
		}

		public CornerMask ThumbCornerMask
		{
			get { return (CornerMask)GetValue(ThumbCornerMaskProperty); }
			set { SetValue(ThumbCornerMaskProperty, value); }
		}

		public Slider()
		{
			Style = Styling.Styles.Slider.Style;
		}
	}
}