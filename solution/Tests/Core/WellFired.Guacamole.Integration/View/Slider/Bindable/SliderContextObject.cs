using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Integration.View.Slider.Bindable
{
	public class SliderContextObject : ObservableBase
	{
		private double _maxValue;
		private double _minValue;
		private UIColor _thumbBackgroundColor;
		private CornerMask _thumbCornerMask;
		private double _thumbCornerRadius;
		private UIColor _thumbOutlineColor;
		private double _value;

		public double MinValue
		{
			get => _minValue;
			set => SetProperty(ref _minValue, value);
		}

		public double MaxValue
		{
			get => _maxValue;
			set => SetProperty(ref _maxValue, value);
		}

		public double Value
		{
			get => _value;
			set => SetProperty(ref _value, value);
		}

		public UIColor ThumbBackgroundColor
		{
			get => _thumbBackgroundColor;
			set => SetProperty(ref _thumbBackgroundColor, value);
		}

		public UIColor ThumbOutlineColor
		{
			get => _thumbOutlineColor;
			set => SetProperty(ref _thumbOutlineColor, value);
		}

		public double ThumbCornerRadius
		{
			get => _thumbCornerRadius;
			set => SetProperty(ref _thumbCornerRadius, value);
		}

		public CornerMask ThumbCornerMask
		{
			get => _thumbCornerMask;
			set => SetProperty(ref _thumbCornerMask, value);
		}
	}
}