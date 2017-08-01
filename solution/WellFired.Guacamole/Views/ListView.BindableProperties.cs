using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views.BindingContexts;

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

        [PublicAPI] public static readonly BindableProperty OrientationProperty = BindableProperty.Create<ListView, OrientationOptions>(
            OrientationOptions.Vertical, 
            BindingMode.TwoWay,
            v => v.Orientation
        );

        [PublicAPI] public static readonly BindableProperty NumberOfVisibleEntriesProperty = BindableProperty.Create<ListView, int>(
            default(int), 
            BindingMode.TwoWay,
            v => v.NumberOfVisibleEntries
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
            get { return (int) GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        [PublicAPI]
        public INotifyPropertyChanged SelectedItem
        {
            get { return (INotifyPropertyChanged)GetValue(SelectedItemProperty); }
            set
            {
                var oldItem = SelectedItem;
                if (!SetValue(SelectedItemProperty, value)) 
                    return;

                foreach (var itemSource in ItemSource)
                {
                    if (!(itemSource is CellBindingContextBase))
                        return;
                    
                    var cellBindingContextBase = itemSource as CellBindingContextBase;
                    if (cellBindingContextBase.Equals(oldItem))
                        cellBindingContextBase.IsSelected = false;
                    if (cellBindingContextBase.Equals(SelectedItem))
                        cellBindingContextBase.IsSelected = true;
                }
            }
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
            get { return (int) GetValue(EntrySizeProperty); }
            set { SetValue(EntrySizeProperty, value); }
        }
        
        [PublicAPI]
        public OrientationOptions Orientation
        {
            get { return (OrientationOptions) GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        [PublicAPI]
        public int NumberOfVisibleEntries 
        { 
            get { return (int) GetValue(NumberOfVisibleEntriesProperty); }
            set { SetValue(NumberOfVisibleEntriesProperty, value); } 
        }

        [PublicAPI]
        public bool CanScroll
        {
            get { return (bool) GetValue(CanScrollProperty); }
            private set { SetValue(CanScrollProperty, value); }
        }

        public float ScrollOffset 
        { 
            get { return (float) GetValue(ScrollOffsetProperty); }
            set
            {
                var clampedValue = ListViewHelper.ClampScroll(NumberOfVisibleEntries, Spacing, TotalContentSize, EntrySize, value);
                if(SetValue(ScrollOffsetProperty, clampedValue))
                    CalculateVisualDataSet();
            }
        }

        [PublicAPI]
        public UIColor ScrollBarBackgroundColor
        {
            get { return (UIColor) GetValue(ScrollBarBackgroundColorProperty); }
            set { SetValue(ScrollBarBackgroundColorProperty, value); }
        }

        [PublicAPI]
        public UIColor ScrollBarOutlineColor
        {
            get { return (UIColor) GetValue(ScrollBarOutlineColorProperty); }
            set { SetValue(ScrollBarOutlineColorProperty, value); }
        }

        [PublicAPI]
        public double ScrollBarCornerRadius
        {
            get { return (double) GetValue(ScrollBarCornerRadiusProperty); }
            set { SetValue(ScrollBarCornerRadiusProperty, value); }
        }

        [PublicAPI]
        public CornerMask ScrollBarCornerMask
        {
            get { return (CornerMask) GetValue(ScrollBarCornerMaskProperty); }
            set { SetValue(ScrollBarCornerMaskProperty, value); }
        }

        [PublicAPI]
        public int ScrollBarSize
        {
            get { return (int) GetValue(ScrollBarSizeProperty); }
            set { SetValue(ScrollBarSizeProperty, value); }
        }

        [PublicAPI]
        public bool ShouldShowScrollBar
        {
            get { return (bool) GetValue(ShouldShowScrollBarProperty); }
            set { SetValue(ShouldShowScrollBarProperty, value); }
        }
    }
}