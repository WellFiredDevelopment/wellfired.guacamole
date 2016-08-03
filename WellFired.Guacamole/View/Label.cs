using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.View
{
    public class Label : ViewBase
    {
        [PublicAPI] public static readonly BindableProperty TextProperty = BindableProperty.Create<Label, string>(
            defaultValue: string.Empty,
            bindingMode: BindingMode.TwoWay,
            getter: entry => entry.Text
            );

        [PublicAPI] public static readonly BindableProperty TextColorProperty = BindableProperty.Create<Label, UIColor>(
            defaultValue: UIColor.Black,
            bindingMode: BindingMode.TwoWay,
            getter: entry => entry.TextColor
            );

        [PublicAPI] public static readonly BindableProperty HorizontalTextAlignProperty = BindableProperty
            .Create<Label, UITextAlign>(
                defaultValue: UITextAlign.Start,
                bindingMode: BindingMode.TwoWay,
                getter: entry => entry.HorizontalTextAlign
            );

        [PublicAPI] public static readonly BindableProperty VerticalTextAlignProperty = BindableProperty
            .Create<Label, UITextAlign>(
                defaultValue: UITextAlign.Middle,
                bindingMode: BindingMode.TwoWay,
                getter: entry => entry.VerticalTextAlign
            );

        [PublicAPI]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
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

        public Label()
        {
            BackgroundColor = UIColor.FromRGB(84, 84, 84);
            OutlineColor = BackgroundColor;
            TextColor = UIColor.White;
            HorizontalTextAlign = UITextAlign.Middle;
            VerticalTextAlign = UITextAlign.Middle;
        }

        protected override UIRect CalculateValidRectRequest()
        {
            return new UIRect(0, 0, 100, 20);
        }
    }
}