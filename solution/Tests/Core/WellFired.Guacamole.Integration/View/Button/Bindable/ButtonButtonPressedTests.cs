﻿using NUnit.Framework;

namespace WellFired.Guacamole.Integration.View.Button.Bindable
{
	[TestFixture]
	public class ButtonButtonPressedTests
	{
		[SetUp]
		public void Setup()
		{
			_buttonViewView = new Views.ButtonView();
			_labelContext = new ButtonContextObject();
			_buttonViewView.BindingContext = _labelContext;
		}

		private Views.ButtonView _buttonViewView;
		private ButtonContextObject _labelContext;

		[Test]
		public void IsBindable()
		{
			var buttonPressed1 = new Command();
			var buttonPressed2 = new Command();
			var buttonPressed3 = new Command();
			_buttonViewView.ButtonPressedCommand = buttonPressed1;
			_labelContext.ButtonPressedCommand = buttonPressed2;
			Assert.That(_labelContext.ButtonPressedCommand != _buttonViewView.ButtonPressedCommand);
			_buttonViewView.Bind(Views.ButtonView.ButtonPressedCommandProperty, nameof(_labelContext.ButtonPressedCommand));
			Assert.That(_labelContext.ButtonPressedCommand == _buttonViewView.ButtonPressedCommand);
			_labelContext.ButtonPressedCommand = buttonPressed3;
			Assert.That(_labelContext.ButtonPressedCommand == _buttonViewView.ButtonPressedCommand);
		}
	}
}