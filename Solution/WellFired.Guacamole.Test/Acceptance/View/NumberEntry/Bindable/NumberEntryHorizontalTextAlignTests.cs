using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Acceptance.View.NumberEntry.Bindable
{
	[TestFixture]
	public class NumberEntryHorizontalTextAlignTests
	{
		[SetUp]
		public void Setup()
		{
			_numberEntryView = new Guacamole.View.NumberEntry();
			_numberEntryContext = new NumberEntryContextObject();
			_numberEntryView.BindingContext = _numberEntryContext;
		}

		private Guacamole.View.NumberEntry _numberEntryView;
		private NumberEntryContextObject _numberEntryContext;

		[Test]
		public void IsBindable()
		{
			_numberEntryView.HorizontalTextAlign = UITextAlign.End;
			_numberEntryContext.HorizontalTextAlign = UITextAlign.Middle;
			Assert.That(_numberEntryContext.HorizontalTextAlign != _numberEntryView.HorizontalTextAlign);
			_numberEntryView.Bind(Guacamole.View.NumberEntry.HorizontalTextAlignProperty,
				nameof(_numberEntryContext.HorizontalTextAlign));
			Assert.That(_numberEntryContext.HorizontalTextAlign == _numberEntryView.HorizontalTextAlign);
			_numberEntryContext.HorizontalTextAlign = UITextAlign.Start;
			Assert.That(_numberEntryContext.HorizontalTextAlign == _numberEntryView.HorizontalTextAlign);
		}
	}
}