using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Integration.View.Label.Bindable
{
	[TestFixture]
	public class LabelTextColorTests
	{
		[SetUp]
		public void Setup()
		{
			_labelView = new Views.Label();
			_labelContext = new LabelContextObject();
			_labelView.BindingContext = _labelContext;
		}

		private Views.Label _labelView;
		private LabelContextObject _labelContext;

		[Test]
		public void IsBindable()
		{
			_labelView.TextColor = UIColor.Aquamarine;
			_labelContext.TextColor = UIColor.Beige;
			Assert.That(_labelContext.TextColor != _labelView.TextColor);
			_labelView.Bind(Views.Label.TextColorProperty, nameof(_labelContext.TextColor));
			Assert.That(_labelContext.TextColor == _labelView.TextColor);
			_labelContext.TextColor = UIColor.Blue;
			Assert.That(_labelContext.TextColor == _labelView.TextColor);
		}
	}
}