using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.View
{
    public class NumberEntry : ViewBase
    {
        [PublicAPI] public static readonly BindableProperty NumberProperty = BindableProperty.Create<NumberEntry, float>
            (
                defaultValue: 10.0f,
                bindingMode: BindingMode.TwoWay,
                getter: entry => entry.Number
            );

        [PublicAPI] public static readonly BindableProperty TextColorProperty = BindableProperty
            .Create<TextEntry, UIColor>(
                defaultValue: UIColor.Black,
                bindingMode: BindingMode.TwoWay,
                getter: entry => entry.TextColor
            );

        [PublicAPI] public static readonly BindableProperty HorizontalTextAlignProperty = BindableProperty
            .Create<TextEntry, UITextAlign>(
                defaultValue: UITextAlign.Start,
                bindingMode: BindingMode.TwoWay,
                getter: entry => entry.HorizontalTextAlign
            );

        [PublicAPI] public static readonly BindableProperty VerticalTextAlignProperty = BindableProperty
            .Create<TextEntry, UITextAlign>(
                defaultValue: UITextAlign.Middle,
                bindingMode: BindingMode.TwoWay,
                getter: entry => entry.VerticalTextAlign
            );

        [PublicAPI]
        public float Number
        {
            get { return (float)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        [PublicAPI]
        public UIColor TextColor
        {
            get { return (UIColor)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        [PublicAPI]
        public UITextAlign HorizontalTextAlign
        {
            get { return (UITextAlign)GetValue(HorizontalTextAlignProperty); }
            set { SetValue(HorizontalTextAlignProperty, value); }
        }

        [PublicAPI]
        public UITextAlign VerticalTextAlign
        {
            get { return (UITextAlign)GetValue(VerticalTextAlignProperty); }
            set { SetValue(VerticalTextAlignProperty, value); }
        }

        public NumberEntry()
        {
            BackgroundColor = UIColor.FromRGB(66, 66, 66);
            OutlineColor = BackgroundColor;
            TextColor = UIColor.White;
        }
    }
}