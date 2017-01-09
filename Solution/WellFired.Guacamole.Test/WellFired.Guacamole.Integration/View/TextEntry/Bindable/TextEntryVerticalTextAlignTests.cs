using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Tests.Integration.View.TextEntry.Bindable
{
	[TestFixture]
	public class TextEntryVerticalTextAlignTests
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
			_textEntryView.VerticalTextAlign = UITextAlign.End;
			_textEntryContext.VerticalTextAlign = UITextAlign.Middle;
			Assert.That(_textEntryContext.VerticalTextAlign != _textEntryView.VerticalTextAlign);
			_textEntryView.Bind(Views.TextEntry.VerticalTextAlignProperty, nameof(_textEntryContext.VerticalTextAlign));
			Assert.That(_textEntryContext.VerticalTextAlign == _textEntryView.VerticalTextAlign);
			_textEntryContext.VerticalTextAlign = UITextAlign.Start;
			Assert.That(_textEntryContext.VerticalTextAlign == _textEntryView.VerticalTextAlign);
		}
	}
}