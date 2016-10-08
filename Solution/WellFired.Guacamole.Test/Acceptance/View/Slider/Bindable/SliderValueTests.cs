﻿using System;
using NUnit.Framework;

namespace WellFired.Guacamole.Test.Acceptance.View.Slider.Bindable
{
	[TestFixture]
	public class SliderValueTests
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
			_sliderView.Value = 0.0;
			_sliderContext.Value = 1.0;
			Assert.That(Math.Abs(_sliderContext.Value - _sliderView.Value) > 0.001);
			_sliderView.Bind(Guacamole.View.Slider.ValueProperty, nameof(_sliderContext.Value));
			Assert.That(Math.Abs(_sliderContext.Value - _sliderView.Value) < 0.001);
			_sliderContext.Value = 2.0;
			Assert.That(Math.Abs(_sliderContext.Value - _sliderView.Value) < 0.001);
		}
	}
}