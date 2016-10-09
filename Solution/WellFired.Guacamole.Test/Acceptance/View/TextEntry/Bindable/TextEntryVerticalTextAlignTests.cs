using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Acceptance.View.TextEntry.Bindable
{
	[TestFixture]
	public class TextEntryVerticalTextAlignTests
	{
		[SetUp]
		public void OneTimeSetup()
		{
			_textEntryView = new Guacamole.View.TextEntry();
			_textEntryContext = new TextEntryContextObject();
			_textEntryView.BindingContext = _textEntryContext;
		}

		private Guacamole.View.TextEntry _textEntryView;
		private TextEntryContextObject _textEntryContext;

		[Test]
		public void IsBindable()
		{
			_textEntryView.VerticalTextAlign = UITextAlign.End;
			_textEntryContext.VerticalTextAlign = UITextAlign.Middle;
			Assert.That(_textEntryContext.VerticalTextAlign != _textEntryView.VerticalTextAlign);
			_textEntryView.Bind(Guacamole.View.TextEntry.VerticalTextAlignProperty, nameof(_textEntryContext.VerticalTextAlign));
			Assert.That(_textEntryContext.VerticalTextAlign == _textEntryView.VerticalTextAlign);
			_textEntryContext.VerticalTextAlign = UITextAlign.Start;
			Assert.That(_textEntryContext.VerticalTextAlign == _textEntryView.VerticalTextAlign);
		}
	}
}