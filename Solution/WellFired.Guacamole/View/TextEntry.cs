using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.View
{
    public class TextEntry : ViewBase
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

	    public TextEntry()
        {
			// Set some nice defaults
            BackgroundColor = UIColor.FromRGB(66, 66, 66);
            OutlineColor = BackgroundColor;
            TextColor = UIColor.White;
		    HorizontalLayout = LayoutOptions.Fill;
			CornerRadius = 8;
			Padding = new UIPadding(5, 5, 5, 5);
		    VerticalTextAlign = UITextAlign.Middle;
        }
    }
}