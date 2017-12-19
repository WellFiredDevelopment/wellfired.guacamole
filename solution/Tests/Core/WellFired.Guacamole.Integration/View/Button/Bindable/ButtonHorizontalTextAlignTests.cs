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
			_buttonView = new Views.Button();
			_buttonContext = new ButtonContextObject();
			_buttonView.BindingContext = _buttonContext;
		}

		private Views.Button _buttonView;
		private ButtonContextObject _buttonContext;

		[Test]
		public void IsBindable()
		{
			_buttonView.HorizontalTextAlign = UITextAlign.End;
			_buttonContext.HorizontalTextAlign = UITextAlign.Middle;
			Assert.That(_buttonContext.HorizontalTextAlign != _buttonView.HorizontalTextAlign);
			_buttonView.Bind(Views.Button.HorizontalTextAlignProperty, nameof(_buttonContext.HorizontalTextAlign));
			Assert.That(_buttonContext.HorizontalTextAlign == _buttonView.HorizontalTextAlign);
			_buttonContext.HorizontalTextAlign = UITextAlign.Start;
			Assert.That(_buttonContext.HorizontalTextAlign == _buttonView.HorizontalTextAlign);
		}
	}
}