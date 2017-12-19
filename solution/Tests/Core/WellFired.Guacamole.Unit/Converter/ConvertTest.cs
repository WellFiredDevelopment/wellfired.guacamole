using NUnit.Framework;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unit.Converter
{
	public class ConvertTest
	{
		[Test]
		public void Test()
		{
			var bindableObject = new YesNoBindingContext();
			var label = new Label { Text = "Invalid" };
			Assert.That(label.Text, Is.EqualTo("Invalid"));

			label.BindingContext = bindableObject;
			label.Bind(Label.TextProperty, "YesNo", new YesNoConverter());
			Assert.That(label.Text, Is.EqualTo("no"));

			bindableObject.YesNo = true;
			Assert.That(label.Text, Is.EqualTo("yes"));

			label.Text = "no";
			Assert.That(bindableObject.YesNo, Is.EqualTo(false));
		}
	}
}