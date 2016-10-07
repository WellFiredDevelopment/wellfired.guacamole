using System.ComponentModel;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole.Test.Bindable.UI.ViewBase
{
	public class ViewBaseContextObject : NotifyBase
	{
		private bool _enabled;
		private ControlState _controlState;
		private UIColor _backgroundColor;
		private UIColor _outlineColor;
		private double _cornerRadius;
		private CornerMask _cornerMask;
		private UISize _minSize;
		private UISize _maxSize;
		private LayoutOptions _horizontalLayoutOptions;
		private LayoutOptions _verticalLayoutOptions;
		private UIPadding _padding;

		public bool Enabled
		{
			get { return _enabled; }
			set { SetProperty(ref _enabled, value, nameof(Enabled)); }
		}

		public ControlState ControlState
		{
			get { return _controlState; }
			set { SetProperty(ref _controlState, value, nameof(ControlState)); }
		}

		public UIColor BackgroundColor
		{
			get { return _backgroundColor; }
			set { SetProperty(ref _backgroundColor, value, nameof(BackgroundColor)); }
		}

		public UIColor OutlineColor
		{
			get { return _outlineColor; }
			set { SetProperty(ref _outlineColor, value, nameof(OutlineColor)); }
		}

		public double CornerRadius
		{
			get { return _cornerRadius; }
			set { SetProperty(ref _cornerRadius, value, nameof(CornerRadius)); }
		}

		public CornerMask CornerMask
		{
			get { return _cornerMask; }
			set { SetProperty(ref _cornerMask, value, nameof(CornerMask)); }
		}

		public UISize MinSize
		{
			get { return _minSize; }
			set { SetProperty(ref _minSize, value, nameof(MinSize)); }
		}

		public UISize MaxSize
		{
			get { return _maxSize; }
			set { SetProperty(ref _maxSize, value, nameof(MaxSize)); }
		}

		public LayoutOptions HorizontalLayoutOptions
		{
			get { return _horizontalLayoutOptions; }
			set { SetProperty(ref _horizontalLayoutOptions, value, nameof(HorizontalLayoutOptions)); }
		}

		public LayoutOptions VerticalLayoutOptions
		{
			get { return _verticalLayoutOptions; }
			set { SetProperty(ref _verticalLayoutOptions, value, nameof(VerticalLayoutOptions)); }
		}

		public UIPadding Padding
		{
			get { return _padding; }
			set { SetProperty(ref _padding, value, nameof(Padding)); }
		}
	}
}