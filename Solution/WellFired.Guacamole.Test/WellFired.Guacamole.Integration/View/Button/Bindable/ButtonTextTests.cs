using NUnit.Framework;

namespace WellFired.Guacamole.Tests.Integration.View.Button.Bindable
{
	[TestFixture]
	public class ButtonTextTests
	{
		[SetUp]
		public void Setup()
		{
			_buttonView = new Views.Button();
			_buttonContext = new ButtonContextObject();
			_buttonView.BindingContext = _buttonContext;
		}

		private Views.Button _buttonView;
		private ButtonContextObject _buttonContext;

		[Test]
		public void IsBindable()
		{
			_buttonView.Text = "a";
			_buttonContext.Text = "b";
			Assert.That(_buttonContext.Text != _buttonView.Text);
			_buttonView.Bind(Views.Button.TextProperty, nameof(_buttonContext.Text));
			Assert.That(_buttonContext.Text == _buttonView.Text);
			_buttonContext.Text = "c";
			Assert.That(_buttonContext.Text == _buttonView.Text);
		}
	}
}