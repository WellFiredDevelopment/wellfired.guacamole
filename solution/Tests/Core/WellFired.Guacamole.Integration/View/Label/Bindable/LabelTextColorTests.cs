using NUnit.Framework;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Integration.View.Label.Bindable
{
	[TestFixture]
	public class LabelTextColorTests
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
			_labelViewView.TextColor = UIColor.Aquamarine;
			_labelContext.TextColor = UIColor.Beige;
			Assert.That(_labelContext.TextColor != _labelViewView.TextColor);
			_labelViewView.Bind(Views.LabelView.TextColorProperty, nameof(_labelContext.TextColor));
			Assert.That(_labelContext.TextColor == _labelViewView.TextColor);
			_labelContext.TextColor = UIColor.Blue;
			Assert.That(_labelContext.TextColor == _labelViewView.TextColor);
		}
	}
}