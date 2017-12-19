using NUnit.Framework;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Unit.Bindable
{
	[TestFixture]
	public class GeneralBindableObjectTests
	{
		private class BindableTestObject : BindableObject
		{
			public static readonly BindableProperty ObjectProperty = BindableProperty.Create<BindableTestObject, object>(
				null,
				BindingMode.TwoWay,
				testObject => testObject.Value
			);

			public object Value
			{
				get => GetValue(ObjectProperty);
				set => SetValue(ObjectProperty, value);
			}
		}

		private class FirstContext : NotifyBase
		{
			private object _currentValue;

			public object Value
			{
				get => _currentValue;
				set => SetProperty(ref _currentValue, value);
			}
		}
		
		private class SecondContext : NotifyBase
		{
			private object _currentValue;

			public object Value
			{
				get => _currentValue;
				set => SetProperty(ref _currentValue, value);
			}
		}
		
		[Test]
		public void When_OverwritingBindingModeFromABind_Then_BindingModeShouldNotBeShared()
		{
			var bindableTestObject1 = new BindableTestObject();
			var bindableTestObject2 = new BindableTestObject();

			bindableTestObject1.Bind(BindableTestObject.ObjectProperty, nameof(FirstContext.Value), BindingMode.ReadOnly);
			var firstContext = new FirstContext();
			bindableTestObject1.BindingContext = firstContext;
			
			bindableTestObject2.Bind(BindableTestObject.ObjectProperty, nameof(SecondContext.Value), BindingMode.TwoWay);
			var secondContext = new SecondContext();
			bindableTestObject2.BindingContext = secondContext;

			firstContext.Value = 10;
			Assert.That(bindableTestObject1.Value, Is.EqualTo(firstContext.Value));
			
			bindableTestObject1.Value = 11;
			Assert.That(bindableTestObject1.Value, Is.Not.EqualTo(11));
			Assert.That(bindableTestObject1.Value, Is.EqualTo(firstContext.Value));
		}
	}
}