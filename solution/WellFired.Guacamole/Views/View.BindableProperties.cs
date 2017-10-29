using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Styling;

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

		[PublicAPI] public static readonly BindableProperty OutlineThicknessProperty = BindableProperty
			.Create<View, double>(
				4.0,
				BindingMode.TwoWay,
				viewBase => viewBase.OutlineThickness
			);

		[PublicAPI] public static readonly BindableProperty OutlineMaskProperty = BindableProperty
			.Create<View, OutlineMask>(
				OutlineMask.All,
				BindingMode.TwoWay,
				viewBase => viewBase.OutlineMask
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
			get => (bool) GetValue(EnabledProperty);
			set => SetValue(EnabledProperty, value);
		}

		[PublicAPI]
		public LayoutOptions HorizontalLayout
		{
			get => (LayoutOptions) GetValue(HorizontalLayoutProperty);
			set => SetValue(HorizontalLayoutProperty, value);
		}

		[PublicAPI]
		public LayoutOptions VerticalLayout
		{
			get => (LayoutOptions) GetValue(VerticalLayoutProperty);
			set => SetValue(VerticalLayoutProperty, value);
		}

		[PublicAPI]
		public UIPadding Padding
		{
			get => (UIPadding) GetValue(PaddingProperty);
			set => SetValue(PaddingProperty, value);
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
			get => (UIColor) GetValue(BackgroundColorProperty);
			set => SetValue(BackgroundColorProperty, value);
		}

		[PublicAPI]
		public UIColor OutlineColor
		{
			get => (UIColor) GetValue(OutlineColorProperty);
			set => SetValue(OutlineColorProperty, value);
		}

		[PublicAPI]
		public double OutlineThickness
		{
			get => (double) GetValue(OutlineThicknessProperty);
			set => SetValue(OutlineThicknessProperty, value);
		}

		[PublicAPI]
		public OutlineMask OutlineMask
		{
			get => (OutlineMask) GetValue(OutlineMaskProperty);
			set => SetValue(OutlineMaskProperty, value);
		}

		[PublicAPI]
		public double CornerRadius
		{
			get => (double) GetValue(CornerRadiusProperty);
			set => SetValue(CornerRadiusProperty, value);
		}

		[PublicAPI]
		public CornerMask CornerMask
		{
			get => (CornerMask) GetValue(CornerMaskProperty);
			set => SetValue(CornerMaskProperty, value);
		}

		[PublicAPI]
		public UISize MinSize
		{
			get => (UISize) GetValue(MinSizeProperty);
			set => SetValue(MinSizeProperty, value);
		}

		[PublicAPI]
		public UISize MaxSize
		{
			get => (UISize) GetValue(MaxSizeProperty);
			set => SetValue(MaxSizeProperty, value);
		}

		[PublicAPI]
		public ControlState ControlState
		{
			get => (ControlState) GetValue(ControlStateProperty);
			set => SetValue(ControlStateProperty, value);
		}
	}
}