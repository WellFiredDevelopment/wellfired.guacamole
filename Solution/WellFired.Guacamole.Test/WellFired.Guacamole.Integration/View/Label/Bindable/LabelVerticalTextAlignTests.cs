using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Tests.Integration.View.Label.Bindable
{
	[TestFixture]
	public class LabelVerticalTextAlignTests
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
			_labelView.VerticalTextAlign = UITextAlign.End;
			_labelContext.VerticalTextAlign = UITextAlign.Middle;
			Assert.That(_labelContext.VerticalTextAlign != _labelView.VerticalTextAlign);
			_labelView.Bind(Views.Label.VerticalTextAlignProperty, nameof(_labelContext.VerticalTextAlign));
			Assert.That(_labelContext.VerticalTextAlign == _labelView.VerticalTextAlign);
			_labelContext.VerticalTextAlign = UITextAlign.Start;
			Assert.That(_labelContext.VerticalTextAlign == _labelView.VerticalTextAlign);
		}
	}
}