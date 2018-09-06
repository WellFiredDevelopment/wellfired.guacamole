using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Integration.View.NumberEntry.Bindable
{
	public class NumberEntryContextObject : ObservableBase
	{
		private UITextAlign _horizontalTextAlign;
		private float _number;
		private UIColor _textColor;
		private UITextAlign _verticalTextAlign;

		public float Number
		{
			get => _number;
			set => SetProperty(ref _number, value);
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