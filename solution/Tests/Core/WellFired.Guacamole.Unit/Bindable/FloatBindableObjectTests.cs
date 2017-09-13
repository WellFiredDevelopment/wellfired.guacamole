using NUnit.Framework;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Unit.Bindable
{
	[TestFixture]
	public class FloatBindableObjectTests
	{
		private class BindableTestObject : BindableObject
		{
			public static readonly BindableProperty FloatProperty = BindableProperty.Create<BindableTestObject, float>(
				0.0f,
				BindingMode.TwoWay,
				testObject => testObject.Value
			);

			public float Value
			{
				get { return (float) GetValue(FloatProperty); }
				set { SetValue(FloatProperty, value); }
			}
		}

		private class ContextObject : NotifyBase
		{
			private float _currentValue;

			public float Value
			{
				get { return _currentValue; }
				set { SetProperty(ref _currentValue, value); }
			}
		}

		[Test]
		public void OneWayBindingInverseTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.FloatProperty, nameof(ContextObject.Value), BindingMode.OneWay);
			bindingContext.Value = 10.0f;

			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));

			source.Value = 15.0f;
			Assert.That(bindingContext.Value, Is.Not.EqualTo(source.Value));
		}

		[Test]
		public void OneWayBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.FloatProperty, nameof(ContextObject.Value));
			bindingContext.Value = 10.0f;

			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));
		}
		
		[Test]
		public void ReadOnlyBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.FloatProperty, nameof(ContextObject.Value), BindingMode.ReadOnly);
			
			bindingContext.Value = 10.0f;

			Assert.That(bindingContext.Value, Is.EqualTo(10.0f));
			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));

			source.Value = 15.0f;
			Assert.That(source.Value, Is.EqualTo(10.0f));
			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));
		}

		[Test]
		public void TwoWayBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.FloatProperty, nameof(ContextObject.Value), BindingMode.TwoWay);
			bindingContext.Value = 10.0f;

			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));

			source.Value = 15.0f;

			Assert.That(bindingContext.Value, Is.EqualTo(source.Value));
		}
	}
}