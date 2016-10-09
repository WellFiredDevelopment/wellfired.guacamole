using NUnit.Framework;

namespace WellFired.Guacamole.Test.Acceptance.View.TextEntry.Bindable
{
	[TestFixture]
	public class TextEntryTextTests
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
			_textEntryView.Text = "a";
			_textEntryContext.Text = "b";
			Assert.That(_textEntryContext.Text != _textEntryView.Text);
			_textEntryView.Bind(Guacamole.View.TextEntry.TextProperty, nameof(_textEntryContext.Text));
			Assert.That(_textEntryContext.Text == _textEntryView.Text);
			_textEntryContext.Text = "c";
			Assert.That(_textEntryContext.Text == _textEntryView.Text);
		}
	}
}