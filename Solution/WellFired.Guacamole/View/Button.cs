using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.View
{
    public class Button : ViewBase
    {
        [PublicAPI]
        public static readonly BindableProperty EnabledProperty = BindableProperty.Create<
            Button, bool>(
                defaultValue: true,
                bindingMode: BindingMode.TwoWay,
                getter: button => button.Enabled
            );

        [PublicAPI] public static readonly BindableProperty TextProperty = BindableProperty.Create<
            Button, string>(
                defaultValue: string.Empty,
                bindingMode: BindingMode.TwoWay,
                getter: button => button.Text
            );

        [PublicAPI] public static readonly BindableProperty TextColorProperty = BindableProperty
            .Create<Button, UIColor>(
                defaultValue: UIColor.Black,
                bindingMode: BindingMode.TwoWay,
                getter: button => button.TextColor
            );

        [PublicAPI] public static readonly BindableProperty HorizontalTextAlignProperty = BindableProperty
            .Create<Button, UITextAlign>(
                defaultValue: UITextAlign.Start,
                bindingMode: BindingMode.TwoWay,
                getter: button => button.HorizontalTextAlign
            );

        [PublicAPI] public static readonly BindableProperty VerticalTextAlignProperty = BindableProperty
            .Create<Button, UITextAlign>(
                defaultValue: UITextAlign.Middle,
                bindingMode: BindingMode.TwoWay,
                getter: button => button.VerticalTextAlign
            );

        [PublicAPI] public static readonly BindableProperty ButtonPressedProperty = BindableProperty
            .Create<Button, ICommand>(
                defaultValue: new Command(), 
                bindingMode: BindingMode.TwoWay,
                getter: button => button.Command
            );

        [PublicAPI]
        public bool Enabled
        {
            get { return (bool)GetValue(EnabledProperty); }
            set { SetValue(EnabledProperty, value); }
        }

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

        [PublicAPI]
        public ICommand Command
        {
            get { return (ICommand)GetValue(ButtonPressedProperty); }
            set { SetValue(ButtonPressedProperty, value); }
        }

        public Button()
		{
			// Set some nice defaults
			BackgroundColor = UIColor.FromRGB(66, 66, 66);
            HoverBackgroundColor = UIColor.FromRGB(160, 160, 160);
            ActiveBackgroundColor = UIColor.FromRGB(64, 124, 191);
            OutlineColor = UIColor.FromRGB(125, 125, 125);
	        HorizontalLayout = LayoutOptions.Fill;
	        Padding = 5;
            TextColor = UIColor.White;
            CornerRadius = 8;
            HorizontalTextAlign = UITextAlign.Middle;
            VerticalTextAlign = UITextAlign.Middle;
        }
    }
}