using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Acceptance.View.Slider.Bindable
{
	[TestFixture]
	public class SliderThumbOutlineColorTests
	{
		private Guacamole.View.Slider _sliderView;
		private SliderContextObject _sliderContext;

		[SetUp]
		public void OneTimeSetup()
		{
			_sliderView = new Guacamole.View.Slider();
			_sliderContext = new SliderContextObject();
			_sliderView.BindingContext = _sliderContext;
		}

		[Test]
		public void IsBindable()
		{
			_sliderView.ThumbOutlineColor = UIColor.Aquamarine;
			_sliderContext.ThumbOutlineColor = UIColor.Beige;
			Assert.That(_sliderContext.ThumbOutlineColor != _sliderView.ThumbOutlineColor);
			_sliderView.Bind(Guacamole.View.Slider.ThumbOutlineColorProperty, nameof(_sliderContext.ThumbOutlineColor));
			Assert.That(_sliderContext.ThumbOutlineColor == _sliderView.ThumbOutlineColor);
			_sliderContext.ThumbOutlineColor = UIColor.Blue;
			Assert.That(_sliderContext.ThumbOutlineColor == _sliderView.ThumbOutlineColor);
		}
	}
}