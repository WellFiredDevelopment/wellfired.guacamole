using NUnit.Framework;

namespace WellFired.Guacamole.Integration.View.Button.Bindable
{
	[TestFixture]
	public class ButtonButtonPressedTests
	{
		[SetUp]
		public void Setup()
		{
			_buttonView = new Views.Button();
			_labelContext = new ButtonContextObject();
			_buttonView.BindingContext = _labelContext;
		}

		private Views.Button _buttonView;
		private ButtonContextObject _labelContext;

		[Test]
		public void IsBindable()
		{
			var buttonPressed1 = new Command();
			var buttonPressed2 = new Command();
			var buttonPressed3 = new Command();
			_buttonView.ButtonPressedCommand = buttonPressed1;
			_labelContext.ButtonPressedCommand = buttonPressed2;
			Assert.That(_labelContext.ButtonPressedCommand != _buttonView.ButtonPressedCommand);
			_buttonView.Bind(Views.Button.ButtonPressedCommandProperty, nameof(_labelContext.ButtonPressedCommand));
			Assert.That(_labelContext.ButtonPressedCommand == _buttonView.ButtonPressedCommand);
			_labelContext.ButtonPressedCommand = buttonPressed3;
			Assert.That(_labelContext.ButtonPressedCommand == _buttonView.ButtonPressedCommand);
		}
	}
}