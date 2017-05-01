using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
	public partial class View
	{
		[PublicAPI] public static readonly BindableProperty EnabledProperty = BindableProperty.Create<
			View, bool>(
			true,
			BindingMode.TwoWay,
			viewBase => viewBase.Enabled
		);

		[PublicAPI] public static readonly BindableProperty ControlStateProperty = BindableProperty.Create<
			View, ControlState>(
			ControlState.Normal,
			BindingMode.TwoWay,
			viewBase => viewBase.ControlState
		);

		[PublicAPI] public static readonly BindableProperty BackgroundColorProperty = BindableProperty
			.Create<View, UIColor>(
				UIColor.White,
				BindingMode.TwoWay,
				viewBase => viewBase.BackgroundColor
			);

		[PublicAPI] public static readonly BindableProperty OutlineColorProperty = BindableProperty
			.Create<View, UIColor>(
				default(UIColor),
				BindingMode.TwoWay,
				viewBase => viewBase.OutlineColor
			);

		[PublicAPI] public static readonly BindableProperty CornerRadiusProperty = BindableProperty
			.Create<View, double>(
				0.0,
				BindingMode.TwoWay,
				viewBase => viewBase.CornerRadius
			);

		[PublicAPI] public static readonly BindableProperty CornerMaskProperty = BindableProperty
			.Create<View, CornerMask>(
				CornerMask.All,
				BindingMode.TwoWay,
				viewBase => viewBase.CornerMask
			);

		[PublicAPI] public static readonly BindableProperty MinSizeProperty = BindableProperty
			.Create<View, UISize>(
				UISize.Min,
				BindingMode.TwoWay,
				viewBase => viewBase.MinSize
			);

		[PublicAPI] public static readonly BindableProperty MaxSizeProperty = BindableProperty
			.Create<View, UISize>(
				UISize.Max,
				BindingMode.TwoWay,
				viewBase => viewBase.MaxSize
			);

		[PublicAPI] public static readonly BindableProperty HorizontalLayoutProperty = BindableProperty
			.Create<View, LayoutOptions>(
				LayoutOptions.Fill,
				BindingMode.TwoWay,
				viewBase => viewBase.HorizontalLayout
			);

		[PublicAPI] public static readonly BindableProperty VerticalLayoutProperty = BindableProperty
			.Create<View, LayoutOptions>(
				LayoutOptions.Expand,
				BindingMode.TwoWay,
				viewBase => viewBase.VerticalLayout
			);

		[PublicAPI] public static readonly BindableProperty PaddingProperty = BindableProperty
			.Create<View, UIPadding>(
				0,
				BindingMode.TwoWay,
				viewBase => viewBase.Padding
			);

		[PublicAPI]
		public bool Enabled
		{
			get { return (bool) GetValue(EnabledProperty); }
			set { SetValue(EnabledProperty, value); }
		}

		[PublicAPI]
		public LayoutOptions HorizontalLayout
		{
			get { return (LayoutOptions) GetValue(HorizontalLayoutProperty); }
			set { SetValue(HorizontalLayoutProperty, value); }
		}

		[PublicAPI]
		public LayoutOptions VerticalLayout
		{
			get { return (LayoutOptions) GetValue(VerticalLayoutProperty); }
			set { SetValue(VerticalLayoutProperty, value); }
		}

		[PublicAPI]
		public UIPadding Padding
		{
			get { return (UIPadding) GetValue(PaddingProperty); }
			set { SetValue(PaddingProperty, value); }
		}

		[PublicAPI]
		public Style Style
		{
			private get { return _style; }
			set
			{
				if (_style == value)
					return;

				_style = value;
				if (_style != null)
					UpdateNewStyle(_style);
			}
		}

		[PublicAPI]
		public UIColor BackgroundColor
		{
			get { return (UIColor) GetValue(BackgroundColorProperty); }
			set { SetValue(BackgroundColorProperty, value); }
		}

		[PublicAPI]
		public UIColor OutlineColor
		{
			get { return (UIColor) GetValue(OutlineColorProperty); }
			set { SetValue(OutlineColorProperty, value); }
		}

		[PublicAPI]
		public double CornerRadius
		{
			get { return (double) GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}

		[PublicAPI]
		public CornerMask CornerMask
		{
			get { return (CornerMask) GetValue(CornerMaskProperty); }
			set { SetValue(CornerMaskProperty, value); }
		}

		[PublicAPI]
		public UISize MinSize
		{
			get { return (UISize) GetValue(MinSizeProperty); }
			set { SetValue(MinSizeProperty, value); }
		}

		[PublicAPI]
		public UISize MaxSize
		{
			get { return (UISize) GetValue(MaxSizeProperty); }
			set { SetValue(MaxSizeProperty, value); }
		}

		[PublicAPI]
		public ControlState ControlState
		{
			get { return (ControlState) GetValue(ControlStateProperty); }
			set { SetValue(ControlStateProperty, value); }
		}
	}
}