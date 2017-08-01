using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Image;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
    public partial class ToggleView
    {
        [PublicAPI] public static readonly BindableProperty OnProperty = BindableProperty.Create<ToggleView, bool>(
            default(bool),
            BindingMode.TwoWay,
            v => v.On
        );

        [PublicAPI] public static readonly BindableProperty ButtonPressedCommandProperty = BindableProperty.Create<ToggleView, ICommand>(
                new Command(),
                BindingMode.TwoWay,
                v => v.ButtonPressedCommand
            );

        [PublicAPI] public static readonly BindableProperty OnImageSourceProperty = BindableProperty.Create<ToggleView, IImageSource>(
            ImageSource.From(ImageShape.Circle, UIColor.Grey),
            BindingMode.TwoWay,
            v => v.OnImageSource
        );

        [PublicAPI] public static readonly BindableProperty OffImageSourceProperty = BindableProperty.Create<ToggleView, IImageSource>(
            ImageSource.From(ImageShape.Circle, UIColor.Clear, UIColor.Grey),
            BindingMode.TwoWay,
            v => v.OffImageSource
        );

        [PublicAPI]
        public bool On
        {
            get { return (bool) GetValue(OnProperty); }
            set { SetValue(OnProperty, value); }
        }

        [PublicAPI]
        public IImageSource OnImageSource
        {
            get { return (IImageSource) GetValue(OnImageSourceProperty); }
            set { SetValue(OnImageSourceProperty, value); }
        }

        [PublicAPI]
        public IImageSource OffImageSource
        {
            get { return (IImageSource) GetValue(OffImageSourceProperty); }
            set { SetValue(OffImageSourceProperty, value); }
        }

        [PublicAPI]
        public ICommand ButtonPressedCommand
        {
            get { return (ICommand) GetValue(ButtonPressedCommandProperty); }
            set
            {
                var previousValue = ButtonPressedCommand;
                var newValue = value;

                if (!SetValue(ButtonPressedCommandProperty, value))
                    return;

                if (previousValue != null)
                    previousValue.PropertyChanged -= CommandChanged;

                if (newValue != null)
                    newValue.PropertyChanged += CommandChanged;

                Enabled = ButtonPressedCommand.CanExecute;
            }
        }
    }
}