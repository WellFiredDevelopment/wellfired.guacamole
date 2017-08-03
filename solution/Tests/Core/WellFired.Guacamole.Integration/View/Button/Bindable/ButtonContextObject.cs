using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Integration.View.Button.Bindable
{
	public class ButtonContextObject : NotifyBase
	{
		private ICommand _buttonPressedCommand;
		private UITextAlign _horizontalTextAlign;
		private string _text;
		private UIColor _textColor;
		private UITextAlign _verticalTextAlign;

		public string Text
		{
			get { return _text; }
			set { SetProperty(ref _text, value); }
		}

		public UIColor TextColor
		{
			get { return _textColor; }
			set { SetProperty(ref _textColor, value); }
		}

		public UITextAlign HorizontalTextAlign
		{
			get { return _horizontalTextAlign; }
			set { SetProperty(ref _horizontalTextAlign, value); }
		}

		public UITextAlign VerticalTextAlign
		{
			get { return _verticalTextAlign; }
			set { SetProperty(ref _verticalTextAlign, value); }
		}

		public ICommand ButtonPressedCommand
		{
			get { return _buttonPressedCommand; }
			set { SetProperty(ref _buttonPressedCommand, value); }
		}
	}
}