using System.ComponentModel;
using System.Diagnostics;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.DataBinding;
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
                    var view = child as ICell;
                    Debug.Assert(view != null, "view != null");
                    if (view.BindingContext.Equals(oldItem))
                        view.IsSelected = false;
                    else if (view.BindingContext.Equals(SelectedItem))
                        view.IsSelected = true;
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
    }
}