using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.View
{
    public class Button : ViewBase
    {
        [PublicAPI] public static readonly BindableProperty TextProperty = BindableProperty.Create<TextEntry, string>(
            defaultValue: string.Empty,
            bindingMode: BindingMode.TwoWay,
            getter: entry => entry.Text
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

        public Button()
        {
            BackgroundColor = UIColor.FromRGB(125, 125, 125);
            HoverBackgroundColor = UIColor.FromRGB(160, 160, 160);
            ActiveBackgroundColor = UIColor.FromRGB(64, 124, 191);
            OutlineColor = UIColor.FromRGB(125, 125, 125);
            TextColor = UIColor.White;
            CornerRadius = 8.0;
            HorizontalTextAlign = UITextAlign.Middle;
            VerticalTextAlign = UITextAlign.Middle;
        }

        protected override UIRect CalculateValidRectRequest()
        {
            return new UIRect(0, 0, 100, 20);
        }
    }
}