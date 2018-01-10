using System.ComponentModel;
using WellFired.Guacamole.Data;
using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Views
{
	public class ButtonView : View, IClickable
	{
		[PublicAPI] public static readonly BindableProperty TextProperty = BindableProperty.Create<
			ButtonView, string>(
			string.Empty,
			BindingMode.TwoWay,
			button => button.Text
		);

		[PublicAPI] public static readonly BindableProperty TextColorProperty = BindableProperty
			.Create<ButtonView, UIColor>(
				UIColor.Black,
				BindingMode.TwoWay,
				button => button.TextColor
			);

		[PublicAPI] public static readonly BindableProperty HorizontalTextAlignProperty = BindableProperty
			.Create<ButtonView, UITextAlign>(
				UITextAlign.Start,
				BindingMode.TwoWay,
				button => button.HorizontalTextAlign
			);

		[PublicAPI] public static readonly BindableProperty VerticalTextAlignProperty = BindableProperty
			.Create<ButtonView, UITextAlign>(
				UITextAlign.Middle,
				BindingMode.TwoWay,
				button => button.VerticalTextAlign
			);

		[PublicAPI] public static readonly BindableProperty ButtonPressedCommandProperty = BindableProperty
			.Create<ButtonView, ICommand>(
				new Command(),
				BindingMode.TwoWay,
				button => button.ButtonPressedCommand
			);

		public ButtonView()
		{
			Style = Styling.Styles.Button.Style;
		}

		[PublicAPI]
		public string Text
		{
			get => (string) GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}

		[PublicAPI]
		public UIColor TextColor
		{
			get => (UIColor) GetValue(TextColorProperty);
			set => SetValue(TextColorProperty, value);
		}

		[PublicAPI]
		public UITextAlign HorizontalTextAlign
		{
			get => (UITextAlign) GetValue(HorizontalTextAlignProperty);
			set => SetValue(HorizontalTextAlignProperty, value);
		}

		[PublicAPI]
		public UITextAlign VerticalTextAlign
		{
			get => (UITextAlign) GetValue(VerticalTextAlignProperty);
			set => SetValue(VerticalTextAlignProperty, value);
		}

		[PublicAPI]
		public ICommand ButtonPressedCommand
		{
			get => (ICommand) GetValue(ButtonPressedCommandProperty);
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

		private void CommandChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			if (propertyChangedEventArgs.PropertyName == nameof(ICommand.CanExecute))
				Enabled = ButtonPressedCommand.CanExecute;
		}

		public void Click(int button)
		{
			FocusControl();

			if (!Enabled)
				return;

			if (ButtonPressedCommand.CanExecute)
				ButtonPressedCommand.Execute();
		}
	}
}