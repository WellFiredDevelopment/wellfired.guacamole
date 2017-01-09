using NUnit.Framework;

namespace WellFired.Guacamole.Tests.Integration.View.TextEntry.Bindable
{
	[TestFixture]
	public class TextEntryTextTests
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
			_textEntryView.Text = "a";
			_textEntryContext.Text = "b";
			Assert.That(_textEntryContext.Text != _textEntryView.Text);
			_textEntryView.Bind(Views.TextEntry.TextProperty, nameof(_textEntryContext.Text));
			Assert.That(_textEntryContext.Text == _textEntryView.Text);
			_textEntryContext.Text = "c";
			Assert.That(_textEntryContext.Text == _textEntryView.Text);
		}
	}
}