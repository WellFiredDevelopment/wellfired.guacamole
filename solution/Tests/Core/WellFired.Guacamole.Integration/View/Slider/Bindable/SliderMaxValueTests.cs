using System;
using NUnit.Framework;

namespace WellFired.Guacamole.Integration.View.Slider.Bindable
{
	[TestFixture]
	public class SliderMaxValueTests
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
			_sliderView.MaxValue = 0.0;
			_sliderContext.MaxValue = 1.0;
			Assert.That(Math.Abs(_sliderContext.MaxValue - _sliderView.MaxValue) > 0.001);
			_sliderView.Bind(Views.Slider.MaxValueProperty, nameof(_sliderContext.MaxValue));
			Assert.That(Math.Abs(_sliderContext.MaxValue - _sliderView.MaxValue) < 0.001);
			_sliderContext.MaxValue = 2.0;
			Assert.That(Math.Abs(_sliderContext.MaxValue - _sliderView.MaxValue) < 0.001);
		}
	}
}