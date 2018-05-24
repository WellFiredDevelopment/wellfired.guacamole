using System;
using NUnit.Framework;

namespace WellFired.Guacamole.Integration.View.Slider.Bindable
{
	[TestFixture]
	public class SliderMinValueTests
	{
		[SetUp]
		public void Setup()
		{
			_sliderView = new Views.SliderView();
			_sliderContext = new SliderContextObject();
			_sliderView.BindingContext = _sliderContext;
		}

		private Views.SliderView _sliderView;
		private SliderContextObject _sliderContext;

		[Test]
		public void IsBindable()
		{
			_sliderView.MinValue = 0.0;
			_sliderContext.MinValue = 1.0;
			Assert.That(Math.Abs(_sliderContext.MinValue - _sliderView.MinValue) > 0.001);
			_sliderView.Bind(Views.SliderView.MinValueProperty, nameof(_sliderContext.MinValue));
			Assert.That(Math.Abs(_sliderContext.MinValue - _sliderView.MinValue) < 0.001);
			_sliderContext.MinValue = 2.0;
			Assert.That(Math.Abs(_sliderContext.MinValue - _sliderView.MinValue) < 0.001);
		}
	}
}