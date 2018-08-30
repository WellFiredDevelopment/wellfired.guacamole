using NUnit.Framework;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Unit.Bindable
{
	[TestFixture]
	public class BooleanBindableObjectTests
	{
		private class BindableTestObject : BindableObject
		{
			public static readonly BindableProperty BooleanProperty = BindableProperty.Create<BindableTestObject, bool>(
				false,
				BindingMode.TwoWay,
				testObject => testObject.Value
			);

			public bool Value
			{
				get => (bool) GetValue(BooleanProperty);
				set => SetValue(BooleanProperty, value);
			}
		}

		private class ContextObject : ObservableBase
		{
			private bool _currentValue;

			public bool Value
			{
				get => _currentValue;
				set => SetProperty(ref _currentValue, value);
			}
		}

		[Test]
		public void OneWayBindingInverseTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.BooleanProperty, nameof(ContextObject.Value), BindingMode.OneWay);
			bindingContext.Value = true;

			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));

			source.Value = false;
			Assert.That(bindingContext.Value, Is.Not.EqualTo(source.Value));
		}

		[Test]
		public void OneWayBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.BooleanProperty, nameof(ContextObject.Value));
			bindingContext.Value = true;

			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));
		}

		[Test]
		public void ReadOnlyBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.BooleanProperty, nameof(ContextObject.Value), BindingMode.ReadOnly);
			
			bindingContext.Value = true;
			Assert.That(source.Value, Is.True);

			source.Value = false;
			Assert.That(source.Value, Is.True);
			Assert.That(bindingContext.Value, Is.EqualTo(bindingContext.Value));
		}

		[Test]
		public void TwoWayBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.BooleanProperty, nameof(ContextObject.Value), BindingMode.TwoWay);
			bindingContext.Value = true;

			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));

			source.Value = false;

			Assert.That(bindingContext.Value, Is.EqualTo(source.Value));
		}
	}
}