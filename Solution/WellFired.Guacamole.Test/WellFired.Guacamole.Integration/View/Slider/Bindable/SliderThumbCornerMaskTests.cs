using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Tests.Integration.View.Slider.Bindable
{
	[TestFixture]
	public class SliderThumbCornerMaskTests
	{
		[SetUp]
		public void Setup()
		{
			_sliderView = new Guacamole.View.Slider();
			_sliderContext = new SliderContextObject();
			_sliderView.BindingContext = _sliderContext;
		}

		private Guacamole.View.Slider _sliderView;
		private SliderContextObject _sliderContext;

		[Test]
		public void IsBindable()
		{
			_sliderView.ThumbCornerMask = CornerMask.BottomLeft;
			_sliderContext.ThumbCornerMask = CornerMask.Bottom;
			Assert.That(_sliderContext.ThumbCornerMask != _sliderView.ThumbCornerMask);
			_sliderView.Bind(Guacamole.View.Slider.ThumbCornerMaskProperty, nameof(_sliderContext.ThumbCornerMask));
			Assert.That(_sliderContext.ThumbCornerMask == _sliderView.ThumbCornerMask);
			_sliderContext.ThumbCornerMask = CornerMask.Left;
			Assert.That(_sliderContext.ThumbCornerMask == _sliderView.ThumbCornerMask);
		}
	}
}