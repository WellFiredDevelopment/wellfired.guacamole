using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Acceptance.View.Slider.Bindable
{
	public class SliderContextObject : NotifyBase
	{
		private double _minValue;
		private double _maxValue;
		private double _value;
		private UIColor _thumbBackgroundColor;
		private UIColor _thumbOutlineColor;
		private double _thumbCornerRadius;
		private CornerMask _thumbCornerMask;

		public double MinValue
		{
			get { return _minValue; }
			set { SetProperty(ref _minValue, value, nameof(MinValue)); }
		}

		public double MaxValue
		{
			get { return _maxValue; }
			set { SetProperty(ref _maxValue, value, nameof(MaxValue)); }
		}

		public double Value
		{
			get { return _value; }
			set { SetProperty(ref _value, value, nameof(Value)); }
		}

		public UIColor ThumbBackgroundColor
		{
			get { return _thumbBackgroundColor; }
			set { SetProperty(ref _thumbBackgroundColor, value, nameof(ThumbBackgroundColor)); }
		}

		public UIColor ThumbOutlineColor
		{
			get { return _thumbOutlineColor; }
			set { SetProperty(ref _thumbOutlineColor, value, nameof(ThumbOutlineColor)); }
		}

		public double ThumbCornerRadius
		{
			get { return _thumbCornerRadius; }
			set { SetProperty(ref _thumbCornerRadius, value, nameof(ThumbCornerRadius)); }
		}

		public CornerMask ThumbCornerMask
		{
			get { return _thumbCornerMask; }
			set { SetProperty(ref _thumbCornerMask, value, nameof(ThumbCornerMask)); }
		}
	}
}