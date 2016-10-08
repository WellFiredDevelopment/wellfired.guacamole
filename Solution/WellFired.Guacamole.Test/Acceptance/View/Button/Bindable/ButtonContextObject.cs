using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Acceptance.View.Button.Bindable
{
	public class ButtonContextObject : NotifyBase
	{
		private string _text;
		private UIColor _textColor;
		private UITextAlign _horizontalTextAlign;
		private UITextAlign _verticalTextAlign;
		private ICommand _buttonPressedCommand;

		public string Text
		{
			get { return _text; }
			set { SetProperty(ref _text, value, nameof(Text)); }
		}

		public UIColor TextColor
		{
			get { return _textColor; }
			set { SetProperty(ref _textColor, value, nameof(TextColor)); }
		}

		public UITextAlign HorizontalTextAlign
		{
			get { return _horizontalTextAlign; }
			set { SetProperty(ref _horizontalTextAlign, value, nameof(HorizontalTextAlign)); }
		}

		public UITextAlign VerticalTextAlign
		{
			get { return _verticalTextAlign; }
			set { SetProperty(ref _verticalTextAlign, value, nameof(VerticalTextAlign)); }
		}

		public ICommand ButtonPressedCommand
		{
			get { return _buttonPressedCommand; }
			set { SetProperty(ref _buttonPressedCommand, value, nameof(ButtonPressedCommand)); }
		}
	}
}