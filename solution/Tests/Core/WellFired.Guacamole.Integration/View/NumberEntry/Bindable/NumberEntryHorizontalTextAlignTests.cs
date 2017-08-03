using NUnit.Framework;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Integration.View.NumberEntry.Bindable
{
	[TestFixture]
	public class NumberEntryHorizontalTextAlignTests
	{
		[SetUp]
		public void Setup()
		{
			_numberEntryView = new Views.NumberEntry();
			_numberEntryContext = new NumberEntryContextObject();
			_numberEntryView.BindingContext = _numberEntryContext;
		}

		private Views.NumberEntry _numberEntryView;
		private NumberEntryContextObject _numberEntryContext;

		[Test]
		public void IsBindable()
		{
			_numberEntryView.HorizontalTextAlign = UITextAlign.End;
			_numberEntryContext.HorizontalTextAlign = UITextAlign.Middle;
			Assert.That(_numberEntryContext.HorizontalTextAlign != _numberEntryView.HorizontalTextAlign);
			_numberEntryView.Bind(Views.NumberEntry.HorizontalTextAlignProperty,
				nameof(_numberEntryContext.HorizontalTextAlign));
			Assert.That(_numberEntryContext.HorizontalTextAlign == _numberEntryView.HorizontalTextAlign);
			_numberEntryContext.HorizontalTextAlign = UITextAlign.Start;
			Assert.That(_numberEntryContext.HorizontalTextAlign == _numberEntryView.HorizontalTextAlign);
		}
	}
}