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
			get => _enabled;
			set => SetProperty(ref _enabled, value);
		}

		public ControlState ControlState
		{
			get => _controlState;
			set => SetProperty(ref _controlState, value);
		}

		public UIColor BackgroundColor
		{
			get => _backgroundColor;
			set => SetProperty(ref _backgroundColor, value);
		}

		public UIColor OutlineColor
		{
			get => _outlineColor;
			set => SetProperty(ref _outlineColor, value);
		}

		public double CornerRadius
		{
			get => _cornerRadius;
			set => SetProperty(ref _cornerRadius, value);
		}

		public CornerMask CornerMask
		{
			get => _cornerMask;
			set => SetProperty(ref _cornerMask, value);
		}

		public UISize MinSize
		{
			get => _minSize;
			set => SetProperty(ref _minSize, value);
		}

		public UISize MaxSize
		{
			get => _maxSize;
			set => SetProperty(ref _maxSize, value);
		}

		public LayoutOptions HorizontalLayoutOptions
		{
			get => _horizontalLayoutOptions;
			set => SetProperty(ref _horizontalLayoutOptions, value);
		}

		public LayoutOptions VerticalLayoutOptions
		{
			get => _verticalLayoutOptions;
			set => SetProperty(ref _verticalLayoutOptions, value);
		}

		public UIPadding Padding
		{
			get => _padding;
			set => SetProperty(ref _padding, value);
		}
	}
}