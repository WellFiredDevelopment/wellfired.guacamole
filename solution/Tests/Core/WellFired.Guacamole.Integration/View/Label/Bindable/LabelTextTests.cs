using NUnit.Framework;

namespace WellFired.Guacamole.Integration.View.Label.Bindable
{
	[TestFixture]
	public class LabelTextTests
	{
		[SetUp]
		public void Setup()
		{
			_labelViewView = new Views.LabelView();
			_labelContext = new LabelContextObject();
			_labelViewView.BindingContext = _labelContext;
		}

		private Views.LabelView _labelViewView;
		private LabelContextObject _labelContext;

		[Test]
		public void IsBindable()
		{
			_labelViewView.Text = "a";
			_labelContext.Text = "b";
			Assert.That(_labelContext.Text != _labelViewView.Text);
			_labelViewView.Bind(Views.LabelView.TextProperty, nameof(_labelContext.Text));
			Assert.That(_labelContext.Text == _labelViewView.Text);
			_labelContext.Text = "c";
			Assert.That(_labelContext.Text == _labelViewView.Text);
		}
	}
}