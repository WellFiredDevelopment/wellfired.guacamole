using NUnit.Framework;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Tests.Bindable
{
	[TestFixture]
	public class DoubleBindableObjectTests
	{
		private class BindableTestObject : BindableObject
		{
			public static readonly BindableProperty DoubleProperty = BindableProperty.Create<BindableTestObject, double>(
				0.0,
				BindingMode.TwoWay,
				testObject => testObject.Value
			);

			public double Value
			{
				get { return (double) GetValue(DoubleProperty); }
				set { SetValue(DoubleProperty, value); }
			}
		}

		private class ContextObject : NotifyBase
		{
			private double _currentValue;

			public double Value
			{
				get { return _currentValue; }
				set { SetProperty(ref _currentValue, value, nameof(Value)); }
			}
		}

		[Test]
		public void OneWayBindingInverseTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.DoubleProperty, nameof(ContextObject.Value));
			bindingContext.Value = 10.0;

			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));

			source.Value = 15.0;

			Assert.That(bindingContext.Value, Is.Not.EqualTo(source.Value));
		}

		[Test]
		public void OneWayBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.DoubleProperty, nameof(ContextObject.Value));
			bindingContext.Value = 10.0;

			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));
		}

		[Test]
		public void TwoWayBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.DoubleProperty, nameof(ContextObject.Value), BindingMode.TwoWay);
			bindingContext.Value = 10.0;

			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));

			source.Value = 15.0;

			Assert.That(bindingContext.Value, Is.EqualTo(source.Value));
		}
	}
}