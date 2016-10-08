using NUnit.Framework;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Test.Acceptance.View.Button.Bindable
{
	[TestFixture]
	public class ButtonTextTests
	{
		private Guacamole.View.Button _buttonView;
		private ButtonContextObject _buttonContext;

		[SetUp]
		public void OneTimeSetup()
		{
			_buttonView = new Guacamole.View.Button();
			_buttonContext = new ButtonContextObject();
			_buttonView.BindingContext = _buttonContext;
		}

		[Test]
		public void OnBindTextIsAutomaticallyUpdatedToTheValueOfBindingContextText()
		{
			_buttonView.Text = "a";
			_buttonContext.Text = "b";
			Assert.That(_buttonContext.Text != _buttonView.Text);
			_buttonView.Bind(Guacamole.View.Button.TextProperty, nameof(_buttonContext.Text));
			Assert.That(_buttonContext.Text == _buttonView.Text);
		}

		[Test]
		public void TextTextBindingWorksInOneWay()
		{
			_buttonView.Bind(Guacamole.View.Button.TextProperty, nameof(_buttonContext.Text));
			Assert.That(_buttonContext.Text == _buttonView.Text);
			_buttonContext.Text = "a";
			Assert.That(_buttonContext.Text == _buttonView.Text);
		}

		[Test]
		public void TextTextBindingWorksInTwoWay()
		{
			_buttonView.Bind(Guacamole.View.Button.TextProperty, nameof(_buttonContext.Text), BindingMode.TwoWay);
			Assert.That(_buttonContext.Text == _buttonView.Text);
			_buttonContext.Text = "a";
			Assert.That(_buttonContext.Text == _buttonView.Text);
			_buttonView.Text = "b";
			Assert.That(_buttonContext.Text == _buttonView.Text);
		}

		[Test]
		public void TextTextBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_buttonView.Bind(Guacamole.View.Button.TextProperty, nameof(_buttonContext.Text));
			Assert.That(_buttonContext.Text == _buttonView.Text);
			_buttonContext.Text = "a";
			Assert.That(_buttonContext.Text == _buttonView.Text);
			_buttonView.Text = "b";
			Assert.That(_buttonContext.Text != _buttonView.Text);
		}
	}
}