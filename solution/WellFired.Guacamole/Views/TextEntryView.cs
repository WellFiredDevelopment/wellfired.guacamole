using WellFired.Guacamole.Data;
using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Views
{
	public class TextEntryView : View, ITypeable
	{
		[PublicAPI] public static readonly BindableProperty TextProperty = BindableProperty.Create<TextEntryView, string>(
			string.Empty,
			BindingMode.TwoWay,
			entry => entry.Text
		);

		[PublicAPI] public static readonly BindableProperty TextColorProperty = BindableProperty
			.Create<TextEntryView, UIColor>(
				UIColor.Black,
				BindingMode.TwoWay,
				entry => entry.TextColor
			);

		[PublicAPI] public static readonly BindableProperty HorizontalTextAlignProperty = BindableProperty
			.Create<TextEntryView, UITextAlign>(
				UITextAlign.Start,
				BindingMode.TwoWay,
				entry => entry.HorizontalTextAlign
			);

		[PublicAPI] public static readonly BindableProperty VerticalTextAlignProperty = BindableProperty
			.Create<TextEntryView, UITextAlign>(
				UITextAlign.Middle,
				BindingMode.TwoWay,
				entry => entry.VerticalTextAlign
			);
		
		[PublicAPI] public static readonly BindableProperty PlaceholderTextProperty = BindableProperty.Create<TextEntryView, string>(
			string.Empty,
			BindingMode.TwoWay,
			entry => entry.PlaceholderText
		);

		[PublicAPI] public static readonly BindableProperty PlaceholderTextColorProperty = BindableProperty
			.Create<TextEntryView, UIColor>(
				UIColor.Black,
				BindingMode.TwoWay,
				entry => entry.PlaceholderTextColor
			);

		[PublicAPI] public static readonly BindableProperty PlaceholderHorizontalTextAlignProperty = BindableProperty
			.Create<TextEntryView, UITextAlign>(
				UITextAlign.Start,
				BindingMode.TwoWay,
				entry => entry.PlaceholderHorizontalTextAlign
			);

		[PublicAPI] public static readonly BindableProperty PlaceholderVerticalTextAlignProperty = BindableProperty
			.Create<TextEntryView, UITextAlign>(
				UITextAlign.Middle,
				BindingMode.TwoWay,
				entry => entry.PlaceholderVerticalTextAlign
			);
		
		[PublicAPI] public static readonly BindableProperty OnInputEnterProperty = BindableProperty
			.Create<TextEntryView, Command>(
				new Command(),
				BindingMode.TwoWay,
				entry => entry.OnInputEnter
			);
		
		[PublicAPI] public static readonly BindableProperty OnFocusLostProperty = BindableProperty
			.Create<TextEntryView, Command>(
				new Command(),
				BindingMode.TwoWay,
				entry => entry.OnFocusLost
			);

		public TextEntryView()
		{
			// Set some nice defaults
			Style = Styling.Styles.TextEntryView.Style;
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
		public string PlaceholderText
		{
			get => (string) GetValue(PlaceholderTextProperty);
			set => SetValue(PlaceholderTextProperty, value);
		}

		[PublicAPI]
		public UIColor PlaceholderTextColor
		{
			get => (UIColor) GetValue(PlaceholderTextColorProperty);
			set => SetValue(PlaceholderTextColorProperty, value);
		}

		[PublicAPI]
		public UITextAlign PlaceholderHorizontalTextAlign
		{
			get => (UITextAlign) GetValue(PlaceholderHorizontalTextAlignProperty);
			set => SetValue(PlaceholderHorizontalTextAlignProperty, value);
		}

		[PublicAPI]
		public UITextAlign PlaceholderVerticalTextAlign
		{
			get => (UITextAlign) GetValue(PlaceholderVerticalTextAlignProperty);
			set => SetValue(PlaceholderVerticalTextAlignProperty, value);
		}
		
		[PublicAPI]
		public Command OnInputEnter
		{
			get => (Command) GetValue(OnInputEnterProperty);
			set => SetValue(OnInputEnterProperty, value);
		}
		
		[PublicAPI]
		public Command OnFocusLost
		{
			get => (Command) GetValue(OnFocusLostProperty);
			set => SetValue(OnFocusLostProperty, value);
		}

		public void Type(char key)
		{
			Text += key;
		}
	}
}