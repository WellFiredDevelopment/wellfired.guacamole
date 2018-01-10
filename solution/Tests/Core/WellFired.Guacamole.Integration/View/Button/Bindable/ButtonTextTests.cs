using NUnit.Framework;

namespace WellFired.Guacamole.Integration.View.Button.Bindable
{
	[TestFixture]
	public class ButtonTextTests
	{
		[SetUp]
		public void Setup()
		{
			_buttonViewView = new Views.ButtonView();
			_buttonContext = new ButtonContextObject();
			_buttonViewView.BindingContext = _buttonContext;
		}

		private Views.ButtonView _buttonViewView;
		private ButtonContextObject _buttonContext;

		[Test]
		public void IsBindable()
		{
			_buttonViewView.Text = "a";
			_buttonContext.Text = "b";
			Assert.That(_buttonContext.Text != _buttonViewView.Text);
			_buttonViewView.Bind(Views.ButtonView.TextProperty, nameof(_buttonContext.Text));
			Assert.That(_buttonContext.Text == _buttonViewView.Text);
			_buttonContext.Text = "c";
			Assert.That(_buttonContext.Text == _buttonViewView.Text);
		}
	}
}