using NUnit.Framework;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Integration.View.Button.Bindable
{
	[TestFixture]
	public class ButtonVerticalTextAlignTests
	{
		[SetUp]
		public void Setup()
		{
			_buttonViewView = new Views.ButtonView();
			_buttonContext = new ButtonContextObject();
			_buttonViewView.BindingContext = _buttonContext;
		}

		private Views.ButtonView _buttonViewView;
		private ButtonContextObject _buttonContext;

		[Test]
		public void IsBindable()
		{
			_buttonViewView.VerticalTextAlign = UITextAlign.End;
			_buttonContext.VerticalTextAlign = UITextAlign.Middle;
			Assert.That(_buttonContext.VerticalTextAlign != _buttonViewView.VerticalTextAlign);
			_buttonViewView.Bind(Views.ButtonView.VerticalTextAlignProperty, nameof(_buttonContext.VerticalTextAlign));
			Assert.That(_buttonContext.VerticalTextAlign == _buttonViewView.VerticalTextAlign);
			_buttonContext.VerticalTextAlign = UITextAlign.Start;
			Assert.That(_buttonContext.VerticalTextAlign == _buttonViewView.VerticalTextAlign);
		}
	}
}