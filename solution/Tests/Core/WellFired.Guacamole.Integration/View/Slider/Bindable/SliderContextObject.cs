using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Integration.View.Slider.Bindable
{
	public class SliderContextObject : NotifyBase
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
			get { return _minValue; }
			set { SetProperty(ref _minValue, value); }
		}

		public double MaxValue
		{
			get { return _maxValue; }
			set { SetProperty(ref _maxValue, value); }
		}

		public double Value
		{
			get { return _value; }
			set { SetProperty(ref _value, value); }
		}

		public UIColor ThumbBackgroundColor
		{
			get { return _thumbBackgroundColor; }
			set { SetProperty(ref _thumbBackgroundColor, value); }
		}

		public UIColor ThumbOutlineColor
		{
			get { return _thumbOutlineColor; }
			set { SetProperty(ref _thumbOutlineColor, value); }
		}

		public double ThumbCornerRadius
		{
			get { return _thumbCornerRadius; }
			set { SetProperty(ref _thumbCornerRadius, value); }
		}

		public CornerMask ThumbCornerMask
		{
			get { return _thumbCornerMask; }
			set { SetProperty(ref _thumbCornerMask, value); }
		}
	}
}