using NUnit.Framework;

namespace WellFired.Guacamole.Test.Acceptance.View.Label.Bindable
{
	[TestFixture]
	public class LabelTextTests
	{
		[SetUp]
		public void Setup()
		{
			_labelView = new Guacamole.View.Label();
			_labelContext = new LabelContextObject();
			_labelView.BindingContext = _labelContext;
		}

		private Guacamole.View.Label _labelView;
		private LabelContextObject _labelContext;

		[Test]
		public void IsBindable()
		{
			_labelView.Text = "a";
			_labelContext.Text = "b";
			Assert.That(_labelContext.Text != _labelView.Text);
			_labelView.Bind(Guacamole.View.Label.TextProperty, nameof(_labelContext.Text));
			Assert.That(_labelContext.Text == _labelView.Text);
			_labelContext.Text = "c";
			Assert.That(_labelContext.Text == _labelView.Text);
		}
	}
}