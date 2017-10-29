using System;
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
		
		[Test]
		public void With_ABackingValueOfTypeInt_When_BoundToTypeNull_Then_BindIsNotSuccessful()
		{
			var view = new Views.View { OutlineThickness = 1 };
			var backingStore = new BackingStore();
			Assert.That(view.OutlineThickness, Is.EqualTo(1));

			view.BindingContext = backingStore;
			Assert.That(() => view.Bind(Views.View.OutlineThicknessProperty, "SomeNullObject"), Throws.TypeOf<SystemException>());
		}
		
		[Test]
		public void With_ABackingValueOfTypeObject_When_BoundToTypeNull_Then_BindIsSuccessful()
		{
			var bindable = new BindableObjectWithNullableProperty { CanBeNull = 1 };
			var backingStore = new BackingStore();
			Assert.That(bindable.CanBeNull, Is.EqualTo(1));

			bindable.BindingContext = backingStore;
			Assert.That(() => bindable.Bind(BindableObjectWithNullableProperty.CanBeNullProperty, "SomeNullObject"), Throws.Nothing);
		}
	}
}