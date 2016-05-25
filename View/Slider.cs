using WellFired.Guacamole.Databinding;

namespace WellFired.Guacamole
{
	public class Slider : ViewBase
	{
		public static readonly BindableProperty MinValueProperty = BindableProperty.Create<Slider, double>(
			defaultValue: 0.0,
			bindingMode: BindingMode.TwoWay,
			getter: slider => slider.MinValue
		);

		public static readonly BindableProperty MaxValueProperty = BindableProperty.Create<Slider, double>(
			defaultValue: 1.0,
			bindingMode: BindingMode.TwoWay,
			getter: slider => slider.MaxValue
		);

		public static readonly BindableProperty ValueProperty = BindableProperty.Create<Slider, double>(
			defaultValue: 0.0,
			bindingMode: BindingMode.TwoWay,
			getter: slider => slider.Value
		);

		public double MinValue
		{
			get { return (double)GetValue(MinValueProperty); }
			set { SetValue(MinValueProperty, value); }
		}

		public double MaxValue
		{
			get { return (double)GetValue(MaxValueProperty); }
			set { SetValue(MaxValueProperty, value); }
		}

		public double Value
		{
			get { return (double)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		protected override UIRect CalculateValidRectRequest()
		{
			return new UIRect(0, 0, 250, 20);
		}
	}
}