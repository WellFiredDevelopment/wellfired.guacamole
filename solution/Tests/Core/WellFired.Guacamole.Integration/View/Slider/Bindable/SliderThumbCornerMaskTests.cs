using NUnit.Framework;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Integration.View.Slider.Bindable
{
	[TestFixture]
	public class SliderThumbCornerMaskTests
	{
		[SetUp]
		public void Setup()
		{
			_sliderView = new Views.Slider();
			_sliderContext = new SliderContextObject();
			_sliderView.BindingContext = _sliderContext;
		}

		private Views.Slider _sliderView;
		private SliderContextObject _sliderContext;

		[Test]
		public void IsBindable()
		{
			_sliderView.ThumbCornerMask = CornerMask.BottomLeft;
			_sliderContext.ThumbCornerMask = CornerMask.Bottom;
			Assert.That(_sliderContext.ThumbCornerMask != _sliderView.ThumbCornerMask);
			_sliderView.Bind(Views.Slider.ThumbCornerMaskProperty, nameof(_sliderContext.ThumbCornerMask));
			Assert.That(_sliderContext.ThumbCornerMask == _sliderView.ThumbCornerMask);
			_sliderContext.ThumbCornerMask = CornerMask.Left;
			Assert.That(_sliderContext.ThumbCornerMask == _sliderView.ThumbCornerMask);
		}
	}
}