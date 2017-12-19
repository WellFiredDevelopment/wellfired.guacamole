using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Diagnostics;

namespace WellFired.Guacamole.Views
{
    public partial class ListView
    {
        [PublicAPI] public static readonly BindableProperty SpacingProperty = BindableProperty.Create<ListView, int>(
            default(int),
            BindingMode.TwoWay,
            v => v.Spacing
        );
        
        [PublicAPI] public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create<ListView, INotifyPropertyChanged>(
            default(INotifyPropertyChanged),
            BindingMode.TwoWay,
            v => v.SelectedItem
        );
        
        [PublicAPI] public static readonly BindableProperty EntrySizeProperty = BindableProperty.Create<ListView, int>(
            50,
            BindingMode.TwoWay,
            v => v.EntrySize
        );
        
        [PublicAPI] public static readonly BindableProperty HeaderSizeProperty = BindableProperty.Create<ListView, int>(
            50,
            BindingMode.TwoWay,
            v => v.HeaderSize
        );

        [PublicAPI] public static readonly BindableProperty OrientationProperty = BindableProperty.Create<ListView, OrientationOptions>(
            OrientationOptions.Vertical, 
            BindingMode.TwoWay,
            v => v.Orientation
        );

        [PublicAPI] public static readonly BindableProperty AvailableSpaceProperty = BindableProperty.Create<ListView, float>(
            default(float), 
            BindingMode.TwoWay,
            v => v.AvailableSpace
        );

        [PublicAPI] public static readonly BindableProperty CanScrollProperty = BindableProperty.Create<ListView, bool>(
            default(bool), 
            BindingMode.TwoWay,
            v => v.CanScroll
        );

        [PublicAPI] public static readonly BindableProperty ScrollOffsetProperty = BindableProperty.Create<ListView, float>(
            default(float), 
            BindingMode.TwoWay,
            v => v.ScrollOffset
        );

        [PublicAPI] public static readonly BindableProperty ScrollBarBackgroundColorProperty = BindableProperty.Create<ListView, UIColor>(
                UIColor.White,
                BindingMode.TwoWay,
                v => v.ScrollBarBackgroundColor
            );

        [PublicAPI] public static readonly BindableProperty ScrollBarOutlineColorProperty = BindableProperty.Create<ListView, UIColor>(
                default(UIColor),
                BindingMode.TwoWay,
                v => v.ScrollBarOutlineColor
            );

        [PublicAPI] public static readonly BindableProperty ScrollBarCornerRadiusProperty = BindableProperty.Create<ListView, double>(
                0.0,
                BindingMode.TwoWay,
                v => v.ScrollBarCornerRadius
            );

        [PublicAPI] public static readonly BindableProperty ScrollBarCornerMaskProperty = BindableProperty.Create<ListView, CornerMask>(
                CornerMask.All,
                BindingMode.TwoWay,
                v => v.ScrollBarCornerMask
            );

        [PublicAPI] public static readonly BindableProperty ScrollBarOutlineThicknessProperty = BindableProperty.Create<ListView, double>(
            1.0,
            BindingMode.TwoWay,
            v => v.ScrollBarOutlineThickness
        );

        [PublicAPI] public static readonly BindableProperty ScrollBarOutlineMaskProperty = BindableProperty.Create<ListView, OutlineMask>(
            OutlineMask.All,
            BindingMode.TwoWay,
            v => v.ScrollBarOutlineMask
        );

        [PublicAPI] public static readonly BindableProperty ScrollBarSizeProperty = BindableProperty.Create<ListView, int>(
                20,
                BindingMode.TwoWay,
                v => v.ScrollBarSize
            );

        [PublicAPI] public static readonly BindableProperty ShouldShowScrollBarProperty = BindableProperty.Create<ListView, bool>(
            true,
            BindingMode.TwoWay,
            v => v.ShouldShowScrollBar
        );

        [PublicAPI]
        public int Spacing
        {
            get => (int) GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }

        [PublicAPI]
        public INotifyPropertyChanged SelectedItem
        {
            get => (INotifyPropertyChanged)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        /// <summary>
        /// Migrates the Selected Items Status to all Children.
        /// </summary>
        private void SetSelectedItem()
        {
            var selectedItem = SelectedItem;
            foreach (var itemSource in CompositeCollection)
            {
                if (!(itemSource is ISelectableCell))
                    continue;

                var cellBindingContextBase = itemSource as ISelectableCell;
                if (cellBindingContextBase.IsSelected)
                    cellBindingContextBase.IsSelected = false;
                if (cellBindingContextBase.Equals(selectedItem))
                    cellBindingContextBase.IsSelected = true;
            }

            OnItemSelected(this, new SelectedItemChangedEventArgs(selectedItem));
        }

        /// <summary>
        /// The size of one Entry into this List View, for the moment, each entry should be the same size, though 
        /// this might change in the future.
        /// The EntrySize refers to the size in the direction of Orientation. I.E. If the Orientation is Vertical, the 
        /// EntrySize is the EntryHeight, if the Orientation is Horizontal, the EntrySize refers to the width.
        /// </summary>
        [PublicAPI]
        public int EntrySize
        {
            get => (int) GetValue(EntrySizeProperty);
            set => SetValue(EntrySizeProperty, value);
        }

        /// <summary>
        /// The size of one Header Entry into this List View, for the moment, each Header Entry should be the same size, though 
        /// this might change in the future.
        /// The HeaderSize refers to the size in the direction of Orientation. I.E. If the Orientation is Vertical, the 
        /// HeaderSize is the EntryHeight, if the Orientation is Horizontal, the HeaderSize refers to the width.
        /// </summary>
        [PublicAPI]
        public int HeaderSize
        {
            get => (int) GetValue(HeaderSizeProperty);
            set => SetValue(HeaderSizeProperty, value);
        }
        
        [PublicAPI]
        public OrientationOptions Orientation
        {
            get => (OrientationOptions) GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        [PublicAPI]
        public float AvailableSpace 
        { 
            get => (float) GetValue(AvailableSpaceProperty);
            set => SetValue(AvailableSpaceProperty, value);
        }

        [PublicAPI]
        public bool CanScroll
        {
            get => (bool) GetValue(CanScrollProperty);
            private set => SetValue(CanScrollProperty, value);
        }

        public float ScrollOffset 
        { 
            get => (float) GetValue(ScrollOffsetProperty);
            set
            {
                var viewSize = SizingHelper.GetImportantSize(Orientation, RectRequest);
                var clampedValue = ListViewHelper.ClampScroll(viewSize,  TotalContentSize, value);
                var previousValue = ScrollOffset;

                if (!SetValue(ScrollOffsetProperty, clampedValue)) 
                    return;
                
                InitialOffset -= ScrollOffset - previousValue;
                CalculateVisualDataSet();
            }
        }

        [PublicAPI]
        public UIColor ScrollBarBackgroundColor
        {
            get => (UIColor) GetValue(ScrollBarBackgroundColorProperty);
            set => SetValue(ScrollBarBackgroundColorProperty, value);
        }

        [PublicAPI]
        public UIColor ScrollBarOutlineColor
        {
            get => (UIColor) GetValue(ScrollBarOutlineColorProperty);
            set => SetValue(ScrollBarOutlineColorProperty, value);
        }

        [PublicAPI]
        public double ScrollBarCornerRadius
        {
            get => (double) GetValue(ScrollBarCornerRadiusProperty);
            set => SetValue(ScrollBarCornerRadiusProperty, value);
        }

        [PublicAPI]
        public CornerMask ScrollBarCornerMask
        {
            get => (CornerMask) GetValue(ScrollBarCornerMaskProperty);
            set => SetValue(ScrollBarCornerMaskProperty, value);
        }

        [PublicAPI]
        public double ScrollBarOutlineThickness
        {
            get => (double) GetValue(ScrollBarOutlineThicknessProperty);
            set => SetValue(ScrollBarOutlineThicknessProperty, value);
        }

        [PublicAPI]
        public OutlineMask ScrollBarOutlineMask
        {
            get => (OutlineMask) GetValue(ScrollBarOutlineMaskProperty);
            set => SetValue(ScrollBarOutlineMaskProperty, value);
        }

        [PublicAPI]
        public int ScrollBarSize
        {
            get => (int) GetValue(ScrollBarSizeProperty);
            set => SetValue(ScrollBarSizeProperty, value);
        }

        [PublicAPI]
        public bool ShouldShowScrollBar
        {
            get => (bool) GetValue(ShouldShowScrollBarProperty);
            set => SetValue(ShouldShowScrollBarProperty, value);
        }
    }
}