using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unit.Styling
{
    [TestFixture]
    public class Given_ATrigger
    {
        [Test]
        public void When_ATriggerCanBeFired_Then_ShouldFireCheckReturnsTrue()
        {
            var trigger = Substitute.For<ITrigger>();
            trigger.Property.Returns(View.EnabledProperty);
            trigger.Value.Returns(true);

            // Mock our bindable object to return true for Enabled
            var bindable = Substitute.For<IBindableObject>();
            bindable.GetValue(Arg.Is(View.EnabledProperty)).Returns(true);

            var shouldFireTrigger = StyleHelper.ShouldFiredTrigger(trigger, bindable, View.EnabledProperty.PropertyName);
            Assert.That(shouldFireTrigger, Is.True);
        }
        
        [Test]
        public void When_ATriggerWithOneConditionalThatCanBeFired_Then_ShouldFireCheckReturnsTrue()
        {
            var conditional = Substitute.For<IConditional>();
            conditional.Property.Returns(View.ControlStateProperty);
            conditional.Value.Returns(ControlState.Hover);
            
            var trigger = Substitute.For<ITrigger>();
            trigger.Property.Returns(View.EnabledProperty);
            trigger.Value.Returns(true);
            trigger.Conditionals.Returns(new List<IConditional> { conditional });

            // Mock our bindable object to return true for Enabled
            var bindable = Substitute.For<IBindableObject>();
            bindable.GetValue(Arg.Is(View.EnabledProperty)).Returns(true);
            bindable.GetValue(Arg.Is(View.ControlStateProperty)).Returns(ControlState.Hover);

            var shouldFireTrigger = StyleHelper.ShouldFiredTrigger(trigger, bindable, View.EnabledProperty.PropertyName);
            Assert.That(shouldFireTrigger, Is.True);
        }
        
        [Test]
        public void When_ATriggerWithTwoConditionalThatCanBeFired_Then_ShouldFireCheckReturnsTrue()
        {
            var conditional0 = Substitute.For<IConditional>();
            conditional0.Property.Returns(View.ControlStateProperty);
            conditional0.Value.Returns(ControlState.Hover);
            
            var conditional1 = Substitute.For<IConditional>();
            conditional1.Property.Returns(View.CornerMaskProperty);
            conditional1.Value.Returns(CornerMask.Bottom);
            
            var trigger = Substitute.For<ITrigger>();
            trigger.Property.Returns(View.EnabledProperty);
            trigger.Value.Returns(true);
            trigger.Conditionals.Returns(new List<IConditional> { conditional0, conditional1 });

            // Mock our bindable object to return true for Enabled
            var bindable = Substitute.For<IBindableObject>();
            bindable.GetValue(Arg.Is(View.EnabledProperty)).Returns(true);
            bindable.GetValue(Arg.Is(View.ControlStateProperty)).Returns(ControlState.Hover);
            bindable.GetValue(Arg.Is(View.CornerMaskProperty)).Returns(CornerMask.Bottom);

            var shouldFireTrigger = StyleHelper.ShouldFiredTrigger(trigger, bindable, View.EnabledProperty.PropertyName);
            Assert.That(shouldFireTrigger, Is.True);
        }
        
        [Test]
        public void When_ATriggerCannotBeFired_Then_ShouldFireCheckReturnsFalse()
        {
            var trigger = Substitute.For<ITrigger>();
            trigger.Property.Returns(View.EnabledProperty);
            trigger.Value.Returns(true);

            var bindable = Substitute.For<IBindableObject>();
            bindable.GetValue(Arg.Is(View.EnabledProperty)).Returns(false);

            var shouldFireTrigger = StyleHelper.ShouldFiredTrigger(trigger, bindable, View.EnabledProperty.PropertyName);
            Assert.That(shouldFireTrigger, Is.False);
        }
        
        [Test]
        public void When_ATriggerWithOneConditionalThatCannotBeFired_Then_ShouldFireCheckReturnsFalse()
        {
            var conditional = Substitute.For<IConditional>();
            conditional.Property.Returns(View.ControlStateProperty);
            conditional.Value.Returns(ControlState.Hover);
            
            var trigger = Substitute.For<ITrigger>();
            trigger.Property.Returns(View.EnabledProperty);
            trigger.Value.Returns(true);
            trigger.Conditionals.Returns(new List<IConditional> { conditional });

            // Mock our bindable object to return true for Enabled
            var bindable = Substitute.For<IBindableObject>();
            bindable.GetValue(Arg.Is(View.EnabledProperty)).Returns(true);
            bindable.GetValue(Arg.Is(View.ControlStateProperty)).Returns(ControlState.Disabled);

            var shouldFireTrigger = StyleHelper.ShouldFiredTrigger(trigger, bindable, View.EnabledProperty.PropertyName);
            Assert.That(shouldFireTrigger, Is.False);
        }
        
        [Test]
        public void When_ATriggerWithOneConditionalThatCannotBeFiredAndOneThatCan_Then_ShouldFireCheckReturnsFalse()
        {
            var conditional0 = Substitute.For<IConditional>();
            conditional0.Property.Returns(View.ControlStateProperty);
            conditional0.Value.Returns(ControlState.Hover);
            
            var conditional1 = Substitute.For<IConditional>();
            conditional1.Property.Returns(View.CornerMaskProperty);
            conditional1.Value.Returns(CornerMask.Bottom);
            
            var trigger = Substitute.For<ITrigger>();
            trigger.Property.Returns(View.EnabledProperty);
            trigger.Value.Returns(true);
            trigger.Conditionals.Returns(new List<IConditional> { conditional0, conditional1 });

            // Mock our bindable object to return true for Enabled
            var bindable = Substitute.For<IBindableObject>();
            bindable.GetValue(Arg.Is(View.EnabledProperty)).Returns(true);
            bindable.GetValue(Arg.Is(View.ControlStateProperty)).Returns(ControlState.Disabled);
            bindable.GetValue(Arg.Is(View.CornerMaskProperty)).Returns(CornerMask.Top);

            var shouldFireTrigger = StyleHelper.ShouldFiredTrigger(trigger, bindable, View.EnabledProperty.PropertyName);
            Assert.That(shouldFireTrigger, Is.False);
        }
    }
}