using System.ComponentModel;
using System.Diagnostics;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Types;

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
                
                foreach (var child in Children)
                {
                    var cell = child as ICell;
                    Debug.Assert(cell != null, "cell != null");
                    if (cell.BindingContext.Equals(oldItem))
                        cell.IsSelected = false;
                    else if (cell.BindingContext.Equals(SelectedItem))
                        cell.IsSelected = true;
                }
            }
        }

        /// <summary>
        /// The size of one Entry into this List View, for the moment, each entry should be the same size, though 
        /// this might change in the future.
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
                var clampedValue = ListViewHelper.ClampScroll(this, value);
                SetValue(ScrollOffsetProperty, clampedValue);
            }
        }
    }
}