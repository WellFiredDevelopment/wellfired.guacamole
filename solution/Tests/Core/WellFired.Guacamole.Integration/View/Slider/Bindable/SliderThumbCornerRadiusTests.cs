using System;
using NUnit.Framework;

namespace WellFired.Guacamole.Integration.View.Slider.Bindable
{
	[TestFixture]
	public class SliderThumbCornerRadiusTests
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
			_sliderView.ThumbCornerRadius = 0.0;
			_sliderContext.ThumbCornerRadius = 1.0;
			Assert.That(Math.Abs(_sliderContext.ThumbCornerRadius - _sliderView.ThumbCornerRadius) > 0.001);
			_sliderView.Bind(Views.SliderView.ThumbCornerRadiusProperty, nameof(_sliderContext.ThumbCornerRadius));
			Assert.That(Math.Abs(_sliderContext.ThumbCornerRadius - _sliderView.ThumbCornerRadius) < 0.001);
			_sliderContext.ThumbCornerRadius = 2.0;
			Assert.That(Math.Abs(_sliderContext.ThumbCornerRadius - _sliderView.ThumbCornerRadius) < 0.001);
		}
	}
}