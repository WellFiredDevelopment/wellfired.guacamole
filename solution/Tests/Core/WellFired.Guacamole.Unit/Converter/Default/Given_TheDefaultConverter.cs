using NUnit.Framework;

namespace WellFired.Guacamole.Unit.Converter.Default
{
	public class GivenTheDefaultConverter
	{
		[Test]
		public void With_ABackingValueOfTypeIntAsZero_When_BoundToTypeDouble_Then_BindIsSuccessful()
		{
			var view = new Views.View { OutlineThickness = 1 };
			var backingStore = new BackingStore();
			Assert.That(view.OutlineThickness, Is.EqualTo(1));

			view.BindingContext = backingStore;
			view.Bind(Views.View.OutlineThicknessProperty, "DefaultThickness");
			
			Assert.That(view.OutlineThickness, Is.EqualTo(0));
		}
	}
}