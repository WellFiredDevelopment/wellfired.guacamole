using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.View
{
	public partial class ViewBase
	{
		[PublicAPI]
		public static readonly BindableProperty EnabledProperty = BindableProperty.Create<
			Button, bool>(
				defaultValue: true,
				bindingMode: BindingMode.TwoWay,
				getter: viewBase => viewBase.Enabled
			);

		[PublicAPI]
		public static readonly BindableProperty ControlStateProperty = BindableProperty.Create<
			Button, ControlState>(
				defaultValue: ControlState.Normal,
				bindingMode: BindingMode.TwoWay,
				getter: viewBase => viewBase.ControlState
			);

		[PublicAPI]
		public static readonly BindableProperty BackgroundColorProperty = BindableProperty
			.Create<ViewBase, UIColor>(
				defaultValue: UIColor.White,
				bindingMode: BindingMode.TwoWay,
				getter: viewBase => viewBase.BackgroundColor
			);

		[PublicAPI]
		public static readonly BindableProperty OutlineColorProperty = BindableProperty
			.Create<ViewBase, UIColor>(
				default(UIColor),
				bindingMode: BindingMode.TwoWay,
				getter: viewBase => viewBase.OutlineColor
			);

		[PublicAPI]
		public static readonly BindableProperty CornerRadiusProperty = BindableProperty
			.Create<TextEntry, double>(
				defaultValue: 0.0,
				bindingMode: BindingMode.TwoWay,
				getter: viewBase => viewBase.CornerRadius
			);

		[PublicAPI]
		public static readonly BindableProperty CornerMaskProperty = BindableProperty
			.Create<TextEntry, CornerMask>(
				defaultValue: CornerMask.All,
				bindingMode: BindingMode.TwoWay,
				getter: viewBase => viewBase.CornerMask
			);

		[PublicAPI]
		public static readonly BindableProperty MinSizeProperty = BindableProperty
			.Create<TextEntry, UISize>(
				defaultValue: UISize.Min,
				bindingMode: BindingMode.TwoWay,
				getter: viewBase => viewBase.MinSize
			);

		[PublicAPI]
		public static readonly BindableProperty MaxSizeProperty = BindableProperty
			.Create<TextEntry, UISize>(
				defaultValue: UISize.Max,
				bindingMode: BindingMode.TwoWay,
				getter: viewBase => viewBase.MaxSize
			);

		[PublicAPI]
		public static readonly BindableProperty HorizontalLayoutProperty = BindableProperty
			.Create<TextEntry, LayoutOptions>(
				defaultValue: LayoutOptions.Fill,
				bindingMode: BindingMode.TwoWay,
				getter: viewBase => viewBase.HorizontalLayout
			);

		[PublicAPI]
		public static readonly BindableProperty VerticalLayoutProperty = BindableProperty
			.Create<TextEntry, LayoutOptions>(
				defaultValue: LayoutOptions.Fill,
				bindingMode: BindingMode.TwoWay,
				getter: viewBase => viewBase.VerticalLayout
			);

		[PublicAPI]
		public static readonly BindableProperty PaddingProperty = BindableProperty
			.Create<TextEntry, UIPadding>(
				defaultValue: 0,
				bindingMode: BindingMode.TwoWay,
				getter: viewBase => viewBase.Padding
			);

		[PublicAPI]
		public bool Enabled
		{
			get { return (bool)GetValue(EnabledProperty); }
			set { SetValue(EnabledProperty, value); }
		}

		[PublicAPI]
		public LayoutOptions HorizontalLayout
		{
			get { return (LayoutOptions)GetValue(HorizontalLayoutProperty); }
			set { SetValue(HorizontalLayoutProperty, value); }
		}

		[PublicAPI]
		public LayoutOptions VerticalLayout
		{
			get { return (LayoutOptions)GetValue(VerticalLayoutProperty); }
			set { SetValue(VerticalLayoutProperty, value); }
		}

		[PublicAPI]
		public UIPadding Padding
		{
			get { return (UIPadding)GetValue(PaddingProperty); }
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
			get { return (UIColor)GetValue(BackgroundColorProperty); }
			set { SetValue(BackgroundColorProperty, value); }
		}

		[PublicAPI]
		public UIColor OutlineColor
		{
			get { return (UIColor)GetValue(OutlineColorProperty); }
			set { SetValue(OutlineColorProperty, value); }
		}

		[PublicAPI]
		public double CornerRadius
		{
			get { return (double)GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}

		[PublicAPI]
		public CornerMask CornerMask
		{
			get { return (CornerMask)GetValue(CornerMaskProperty); }
			set { SetValue(CornerMaskProperty, value); }
		}

		[PublicAPI]
		public UISize MinSize
		{
			get { return (UISize)GetValue(MinSizeProperty); }
			set { SetValue(MinSizeProperty, value); }
		}

		[PublicAPI]
		public UISize MaxSize
		{
			get { return (UISize)GetValue(MaxSizeProperty); }
			set { SetValue(MaxSizeProperty, value); }
		}

		[PublicAPI]
		public ControlState ControlState
		{
			get { return (ControlState)GetValue(ControlStateProperty); }
			set { SetValue(ControlStateProperty, value); }
		}
	}
}