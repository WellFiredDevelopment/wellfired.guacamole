using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Integration.View.Label.Bindable
{
	public class LabelContextObject : NotifyBase
	{
		private UITextAlign _horizontalTextAlign;
		private string _text;
		private UIColor _textColor;
		private UITextAlign _verticalTextAlign;

		public string Text
		{
			get => _text;
			set => SetProperty(ref _text, value);
		}

		public UIColor TextColor
		{
			get => _textColor;
			set => SetProperty(ref _textColor, value);
		}

		public UITextAlign HorizontalTextAlign
		{
			get => _horizontalTextAlign;
			set => SetProperty(ref _horizontalTextAlign, value);
		}

		public UITextAlign VerticalTextAlign
		{
			get => _verticalTextAlign;
			set => SetProperty(ref _verticalTextAlign, value);
		}
	}
}