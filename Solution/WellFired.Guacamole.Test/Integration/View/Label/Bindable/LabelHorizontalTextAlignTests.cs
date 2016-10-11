using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Integration.View.Label.Bindable
{
	[TestFixture]
	public class LabelHorizontalTextAlignTests
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
			_labelView.HorizontalTextAlign = UITextAlign.End;
			_labelContext.HorizontalTextAlign = UITextAlign.Middle;
			Assert.That(_labelContext.HorizontalTextAlign != _labelView.HorizontalTextAlign);
			_labelView.Bind(Guacamole.View.Label.HorizontalTextAlignProperty, nameof(_labelContext.HorizontalTextAlign));
			Assert.That(_labelContext.HorizontalTextAlign == _labelView.HorizontalTextAlign);
			_labelContext.HorizontalTextAlign = UITextAlign.Start;
			Assert.That(_labelContext.HorizontalTextAlign == _labelView.HorizontalTextAlign);
		}
	}
}