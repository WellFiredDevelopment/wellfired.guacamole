using System.ComponentModel;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.View
{
    public class Button : ViewBase
	{
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
	        set
	        {
		        var previousValue = Command;
		        var newValue = value;
				
		        if (!SetValue(ButtonPressedProperty, value))
					return;

		        if (previousValue != null)
			        previousValue.PropertyChanged -= CommandChanged;

		        if (newValue != null)
			        newValue.PropertyChanged += CommandChanged;
				
				Enabled = Command.CanExecute;
			}
        }

	    private void CommandChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
	    {
			if(propertyChangedEventArgs.PropertyName == nameof(ICommand.CanExecute))
			    Enabled = Command.CanExecute;
	    }

	    public Button()
	    {
		    Style = Styling.Styles.Button.Style;
	    }
    }
}