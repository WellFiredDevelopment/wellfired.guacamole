using System.ComponentModel;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
	public class Button : View, IClickable
	{
		[PublicAPI] public static readonly BindableProperty TextProperty = BindableProperty.Create<
			Button, string>(
			string.Empty,
			BindingMode.TwoWay,
			button => button.Text
		);

		[PublicAPI] public static readonly BindableProperty TextColorProperty = BindableProperty
			.Create<Button, UIColor>(
				UIColor.Black,
				BindingMode.TwoWay,
				button => button.TextColor
			);

		[PublicAPI] public static readonly BindableProperty HorizontalTextAlignProperty = BindableProperty
			.Create<Button, UITextAlign>(
				UITextAlign.Start,
				BindingMode.TwoWay,
				button => button.HorizontalTextAlign
			);

		[PublicAPI] public static readonly BindableProperty VerticalTextAlignProperty = BindableProperty
			.Create<Button, UITextAlign>(
				UITextAlign.Middle,
				BindingMode.TwoWay,
				button => button.VerticalTextAlign
			);

		[PublicAPI] public static readonly BindableProperty ButtonPressedCommandProperty = BindableProperty
			.Create<Button, ICommand>(
				new Command(),
				BindingMode.TwoWay,
				button => button.ButtonPressedCommand
			);

		public Button()
		{
			Style = Styling.Styles.Button.Style;
		}

		[PublicAPI]
		public string Text
		{
			get { return (string) GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		[PublicAPI]
		public UIColor TextColor
		{
			get { return (UIColor) GetValue(TextColorProperty); }
			set { SetValue(TextColorProperty, value); }
		}

		[PublicAPI]
		public UITextAlign HorizontalTextAlign
		{
			get { return (UITextAlign) GetValue(HorizontalTextAlignProperty); }
			set { SetValue(HorizontalTextAlignProperty, value); }
		}

		[PublicAPI]
		public UITextAlign VerticalTextAlign
		{
			get { return (UITextAlign) GetValue(VerticalTextAlignProperty); }
			set { SetValue(VerticalTextAlignProperty, value); }
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