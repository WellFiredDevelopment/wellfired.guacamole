using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Tests.Integration.View.Button.Bindable
{
	[TestFixture]
	public class ButtonTextColorTests
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
			_buttonView.TextColor = UIColor.Black;
			_buttonContext.TextColor = UIColor.Blue;
			Assert.That(_buttonContext.TextColor != _buttonView.TextColor);
			_buttonView.Bind(Views.Button.TextColorProperty, nameof(_buttonContext.TextColor));
			Assert.That(_buttonContext.TextColor == _buttonView.TextColor);
			_buttonContext.TextColor = UIColor.Brown;
			Assert.That(_buttonContext.TextColor == _buttonView.TextColor);
		}
	}
}