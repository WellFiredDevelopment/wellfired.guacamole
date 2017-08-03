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
			_buttonView = new Views.Button();
			_buttonContext = new ButtonContextObject();
			_buttonView.BindingContext = _buttonContext;
		}

		private Views.Button _buttonView;
		private ButtonContextObject _buttonContext;

		[Test]
		public void IsBindable()
		{
			_buttonView.VerticalTextAlign = UITextAlign.End;
			_buttonContext.VerticalTextAlign = UITextAlign.Middle;
			Assert.That(_buttonContext.VerticalTextAlign != _buttonView.VerticalTextAlign);
			_buttonView.Bind(Views.Button.VerticalTextAlignProperty, nameof(_buttonContext.VerticalTextAlign));
			Assert.That(_buttonContext.VerticalTextAlign == _buttonView.VerticalTextAlign);
			_buttonContext.VerticalTextAlign = UITextAlign.Start;
			Assert.That(_buttonContext.VerticalTextAlign == _buttonView.VerticalTextAlign);
		}
	}
}