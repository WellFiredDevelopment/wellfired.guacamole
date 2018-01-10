using NUnit.Framework;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Integration.View.NumberEntry.Bindable
{
	[TestFixture]
	public class NumberEntryVerticalTextAlignTests
	{
		[SetUp]
		public void Setup()
		{
			_numberEntryView = new Views.NumberEntryView();
			_numberEntryContext = new NumberEntryContextObject();
			_numberEntryView.BindingContext = _numberEntryContext;
		}

		private Views.NumberEntryView _numberEntryView;
		private NumberEntryContextObject _numberEntryContext;

		[Test]
		public void IsBindable()
		{
			_numberEntryView.VerticalTextAlign = UITextAlign.End;
			_numberEntryContext.VerticalTextAlign = UITextAlign.Middle;
			Assert.That(_numberEntryContext.VerticalTextAlign != _numberEntryView.VerticalTextAlign);
			_numberEntryView.Bind(Views.NumberEntryView.VerticalTextAlignProperty, nameof(_numberEntryContext.VerticalTextAlign));
			Assert.That(_numberEntryContext.VerticalTextAlign == _numberEntryView.VerticalTextAlign);
			_numberEntryContext.VerticalTextAlign = UITextAlign.Start;
			Assert.That(_numberEntryContext.VerticalTextAlign == _numberEntryView.VerticalTextAlign);
		}
	}
}