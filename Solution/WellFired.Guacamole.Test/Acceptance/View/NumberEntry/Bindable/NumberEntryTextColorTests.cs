using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Acceptance.View.NumberEntry.Bindable
{
	[TestFixture]
	public class NumberEntryTextColorTests
	{
		[SetUp]
		public void OneTimeSetup()
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
			_numberEntryView.TextColor = UIColor.Aquamarine;
			_numberEntryContext.TextColor = UIColor.Beige;
			Assert.That(_numberEntryContext.TextColor != _numberEntryView.TextColor);
			_numberEntryView.Bind(Guacamole.View.NumberEntry.TextColorProperty, nameof(_numberEntryContext.TextColor));
			Assert.That(_numberEntryContext.TextColor == _numberEntryView.TextColor);
			_numberEntryContext.TextColor = UIColor.Blue;
			Assert.That(_numberEntryContext.TextColor == _numberEntryView.TextColor);
		}
	}
}