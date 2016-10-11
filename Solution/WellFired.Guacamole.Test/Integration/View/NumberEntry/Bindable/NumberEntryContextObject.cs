using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Integration.View.NumberEntry.Bindable
{
	public class NumberEntryContextObject : NotifyBase
	{
		private UITextAlign _horizontalTextAlign;
		private float _number;
		private UIColor _textColor;
		private UITextAlign _verticalTextAlign;

		public float Number
		{
			get { return _number; }
			set { SetProperty(ref _number, value, nameof(Number)); }
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
	}
}