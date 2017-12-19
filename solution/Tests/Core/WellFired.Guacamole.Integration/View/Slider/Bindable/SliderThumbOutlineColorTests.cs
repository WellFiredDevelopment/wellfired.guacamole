using NUnit.Framework;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Integration.View.Slider.Bindable
{
	[TestFixture]
	public class SliderThumbOutlineColorTests
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
			_sliderView.ThumbOutlineColor = UIColor.Aquamarine;
			_sliderContext.ThumbOutlineColor = UIColor.Beige;
			Assert.That(_sliderContext.ThumbOutlineColor != _sliderView.ThumbOutlineColor);
			_sliderView.Bind(Views.Slider.ThumbOutlineColorProperty, nameof(_sliderContext.ThumbOutlineColor));
			Assert.That(_sliderContext.ThumbOutlineColor == _sliderView.ThumbOutlineColor);
			_sliderContext.ThumbOutlineColor = UIColor.Blue;
			Assert.That(_sliderContext.ThumbOutlineColor == _sliderView.ThumbOutlineColor);
		}
	}
}