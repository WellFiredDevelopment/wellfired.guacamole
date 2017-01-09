using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Tests.Integration.View.TextEntry.Bindable
{
	[TestFixture]
	public class TestEntryTextColorTests
	{
		[SetUp]
		public void Setup()
		{
			_textEntryView = new Views.TextEntry();
			_textEntryContext = new TextEntryContextObject();
			_textEntryView.BindingContext = _textEntryContext;
		}

		private Views.TextEntry _textEntryView;
		private TextEntryContextObject _textEntryContext;

		[Test]
		public void IsBindable()
		{
			_textEntryView.TextColor = UIColor.Aquamarine;
			_textEntryContext.TextColor = UIColor.Beige;
			Assert.That(_textEntryContext.TextColor != _textEntryView.TextColor);
			_textEntryView.Bind(Views.TextEntry.TextColorProperty, nameof(_textEntryContext.TextColor));
			Assert.That(_textEntryContext.TextColor == _textEntryView.TextColor);
			_textEntryContext.TextColor = UIColor.Blue;
			Assert.That(_textEntryContext.TextColor == _textEntryView.TextColor);
		}
	}
}