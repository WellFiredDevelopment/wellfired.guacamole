using NUnit.Framework;

namespace WellFired.Guacamole.Test.Acceptance.View.Button.Bindable
{
	[TestFixture]
	public class ButtonTextTests
	{
		[SetUp]
		public void OneTimeSetup()
		{
			_buttonView = new Guacamole.View.Button();
			_buttonContext = new ButtonContextObject();
			_buttonView.BindingContext = _buttonContext;
		}

		private Guacamole.View.Button _buttonView;
		private ButtonContextObject _buttonContext;

		[Test]
		public void IsBindable()
		{
			_buttonView.Text = "a";
			_buttonContext.Text = "b";
			Assert.That(_buttonContext.Text != _buttonView.Text);
			_buttonView.Bind(Guacamole.View.Button.TextProperty, nameof(_buttonContext.Text));
			Assert.That(_buttonContext.Text == _buttonView.Text);
			_buttonContext.Text = "c";
			Assert.That(_buttonContext.Text == _buttonView.Text);
		}
	}
}