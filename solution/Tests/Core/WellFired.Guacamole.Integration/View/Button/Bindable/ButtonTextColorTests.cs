using NUnit.Framework;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Integration.View.Button.Bindable
{
	[TestFixture]
	public class ButtonTextColorTests
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
			_buttonViewView.TextColor = UIColor.Black;
			_buttonContext.TextColor = UIColor.Blue;
			Assert.That(_buttonContext.TextColor != _buttonViewView.TextColor);
			_buttonViewView.Bind(Views.ButtonView.TextColorProperty, nameof(_buttonContext.TextColor));
			Assert.That(_buttonContext.TextColor == _buttonViewView.TextColor);
			_buttonContext.TextColor = UIColor.Brown;
			Assert.That(_buttonContext.TextColor == _buttonViewView.TextColor);
		}
	}
}