﻿using NUnit.Framework;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Integration.View.TextEntry.Bindable
{
	[TestFixture]
	public class TextColorTests
	{
		[SetUp]
		public void Setup()
		{
			_textEntryView = new Views.TextEntryView();
			_context = new ContextObject();
			_textEntryView.BindingContext = _context;
		}

		private Views.TextEntryView _textEntryView;
		private ContextObject _context;

		[Test]
		public void IsBindable()
		{
			_textEntryView.TextColor = UIColor.Aquamarine;
			_context.TextColor = UIColor.Beige;
			Assert.That(_context.TextColor != _textEntryView.TextColor);
			_textEntryView.Bind(Views.TextEntryView.TextColorProperty, nameof(_context.TextColor));
			Assert.That(_context.TextColor == _textEntryView.TextColor);
			_context.TextColor = UIColor.Blue;
			Assert.That(_context.TextColor == _textEntryView.TextColor);
		}
	}
}