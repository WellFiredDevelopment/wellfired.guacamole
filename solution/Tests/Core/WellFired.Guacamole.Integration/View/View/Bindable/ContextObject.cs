using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.View.View.Bindable
{
	public class ContextObject : NotifyBase
	{
		private UIColor _backgroundColor;
		private ControlState _controlState;
		private CornerMask _cornerMask;
		private double _cornerRadius;
		private bool _enabled;
		private LayoutOptions _horizontalLayoutOptions;
		private UISize _maxSize;
		private UISize _minSize;
		private UIColor _outlineColor;
		private UIPadding _padding;
		private LayoutOptions _verticalLayoutOptions;

		public bool Enabled
		{
			get { return _enabled; }
			set { SetProperty(ref _enabled, value); }
		}

		public ControlState ControlState
		{
			get { return _controlState; }
			set { SetProperty(ref _controlState, value); }
		}

		public UIColor BackgroundColor
		{
			get { return _backgroundColor; }
			set { SetProperty(ref _backgroundColor, value); }
		}

		public UIColor OutlineColor
		{
			get { return _outlineColor; }
			set { SetProperty(ref _outlineColor, value); }
		}

		public double CornerRadius
		{
			get { return _cornerRadius; }
			set { SetProperty(ref _cornerRadius, value); }
		}

		public CornerMask CornerMask
		{
			get { return _cornerMask; }
			set { SetProperty(ref _cornerMask, value); }
		}

		public UISize MinSize
		{
			get { return _minSize; }
			set { SetProperty(ref _minSize, value); }
		}

		public UISize MaxSize
		{
			get { return _maxSize; }
			set { SetProperty(ref _maxSize, value); }
		}

		public LayoutOptions HorizontalLayoutOptions
		{
			get { return _horizontalLayoutOptions; }
			set { SetProperty(ref _horizontalLayoutOptions, value); }
		}

		public LayoutOptions VerticalLayoutOptions
		{
			get { return _verticalLayoutOptions; }
			set { SetProperty(ref _verticalLayoutOptions, value); }
		}

		public UIPadding Padding
		{
			get { return _padding; }
			set { SetProperty(ref _padding, value); }
		}
	}
}