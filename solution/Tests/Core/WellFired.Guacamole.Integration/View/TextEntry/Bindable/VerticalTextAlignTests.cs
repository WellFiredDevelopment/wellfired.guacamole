using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Integration.View.TextEntry.Bindable
{
	[TestFixture]
	public class VerticalTextAlignTests
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
			_textEntryView.VerticalTextAlign = UITextAlign.End;
			_context.VerticalTextAlign = UITextAlign.Middle;
			Assert.That(_context.VerticalTextAlign != _textEntryView.VerticalTextAlign);
			_textEntryView.Bind(Views.TextEntry.VerticalTextAlignProperty, nameof(_context.VerticalTextAlign));
			Assert.That(_context.VerticalTextAlign == _textEntryView.VerticalTextAlign);
			_context.VerticalTextAlign = UITextAlign.Start;
			Assert.That(_context.VerticalTextAlign == _textEntryView.VerticalTextAlign);
		}
	}
}