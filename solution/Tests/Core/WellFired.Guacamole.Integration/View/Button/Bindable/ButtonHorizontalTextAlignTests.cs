using NUnit.Framework;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Integration.View.Button.Bindable
{
	[TestFixture]
	public class ButtonHorizontalTextAlignTests
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
			_buttonViewView.HorizontalTextAlign = UITextAlign.End;
			_buttonContext.HorizontalTextAlign = UITextAlign.Middle;
			Assert.That(_buttonContext.HorizontalTextAlign != _buttonViewView.HorizontalTextAlign);
			_buttonViewView.Bind(Views.ButtonView.HorizontalTextAlignProperty, nameof(_buttonContext.HorizontalTextAlign));
			Assert.That(_buttonContext.HorizontalTextAlign == _buttonViewView.HorizontalTextAlign);
			_buttonContext.HorizontalTextAlign = UITextAlign.Start;
			Assert.That(_buttonContext.HorizontalTextAlign == _buttonViewView.HorizontalTextAlign);
		}
	}
}