using NUnit.Framework;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Integration.View.TextEntry.Bindable
{
	[TestFixture]
	public class HorizontalTextAlignTests
	{
		[SetUp]
		public void Setup()
		{
			_textEntryView = new Views.TextEntry();
			_context = new ContextObject();
			_textEntryView.BindingContext = _context;
		}

		private Views.TextEntry _textEntryView;
		private ContextObject _context;

		[Test]
		public void IsBindable()
		{
			_textEntryView.HorizontalTextAlign = UITextAlign.End;
			_context.HorizontalTextAlign = UITextAlign.Middle;
			Assert.That(_context.HorizontalTextAlign != _textEntryView.HorizontalTextAlign);
			_textEntryView.Bind(Views.TextEntry.HorizontalTextAlignProperty,
				nameof(_context.HorizontalTextAlign));
			Assert.That(_context.HorizontalTextAlign == _textEntryView.HorizontalTextAlign);
			_context.HorizontalTextAlign = UITextAlign.Start;
			Assert.That(_context.HorizontalTextAlign == _textEntryView.HorizontalTextAlign);
		}
	}
}