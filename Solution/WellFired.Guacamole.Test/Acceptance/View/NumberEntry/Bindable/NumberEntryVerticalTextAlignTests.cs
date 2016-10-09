using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Acceptance.View.NumberEntry.Bindable
{
	[TestFixture]
	public class NumberEntryVerticalTextAlignTests
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
			_numberEntryView.VerticalTextAlign = UITextAlign.End;
			_numberEntryContext.VerticalTextAlign = UITextAlign.Middle;
			Assert.That(_numberEntryContext.VerticalTextAlign != _numberEntryView.VerticalTextAlign);
			_numberEntryView.Bind(Guacamole.View.NumberEntry.VerticalTextAlignProperty,
				nameof(_numberEntryContext.VerticalTextAlign));
			Assert.That(_numberEntryContext.VerticalTextAlign == _numberEntryView.VerticalTextAlign);
			_numberEntryContext.VerticalTextAlign = UITextAlign.Start;
			Assert.That(_numberEntryContext.VerticalTextAlign == _numberEntryView.VerticalTextAlign);
		}
	}
}