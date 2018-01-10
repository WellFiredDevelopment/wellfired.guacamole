using NUnit.Framework;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Integration.View.Label.Bindable
{
	[TestFixture]
	public class LabelHorizontalTextAlignTests
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
			_labelViewView.HorizontalTextAlign = UITextAlign.End;
			_labelContext.HorizontalTextAlign = UITextAlign.Middle;
			Assert.That(_labelContext.HorizontalTextAlign != _labelViewView.HorizontalTextAlign);
			_labelViewView.Bind(Views.LabelView.HorizontalTextAlignProperty, nameof(_labelContext.HorizontalTextAlign));
			Assert.That(_labelContext.HorizontalTextAlign == _labelViewView.HorizontalTextAlign);
			_labelContext.HorizontalTextAlign = UITextAlign.Start;
			Assert.That(_labelContext.HorizontalTextAlign == _labelViewView.HorizontalTextAlign);
		}
	}
}