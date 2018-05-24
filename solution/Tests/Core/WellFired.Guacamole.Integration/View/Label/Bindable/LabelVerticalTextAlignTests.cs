using NUnit.Framework;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Integration.View.Label.Bindable
{
	[TestFixture]
	public class LabelVerticalTextAlignTests
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
			_labelViewView.VerticalTextAlign = UITextAlign.End;
			_labelContext.VerticalTextAlign = UITextAlign.Middle;
			Assert.That(_labelContext.VerticalTextAlign != _labelViewView.VerticalTextAlign);
			_labelViewView.Bind(Views.LabelView.VerticalTextAlignProperty, nameof(_labelContext.VerticalTextAlign));
			Assert.That(_labelContext.VerticalTextAlign == _labelViewView.VerticalTextAlign);
			_labelContext.VerticalTextAlign = UITextAlign.Start;
			Assert.That(_labelContext.VerticalTextAlign == _labelViewView.VerticalTextAlign);
		}
	}
}