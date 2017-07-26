using System.ComponentModel;
using System.Diagnostics;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views.BindingContexts;

namespace WellFired.Guacamole.Views
{
    internal static class CellHelper
    {
        public static ICell CreateDefaultCell(object bindingContext, IListView container)
        {
            if (bindingContext is LabelCellBindingContext)
            {
                var labelCell = new LabelCell
                {
                    Container = container
                };

                labelCell.BindingContext = bindingContext as INotifyPropertyChanged;
                labelCell.Bind(LabelCell.TextProperty, "CellLabelText");
                labelCell.Bind(Cell.IsSelectedProperty, "IsSelected", BindingMode.TwoWay);

                return labelCell;
            }
            
            var imageCell = new ImageCell
            {
                Container = container
            };
            
            imageCell.BindingContext = bindingContext as INotifyPropertyChanged;
            imageCell.Bind(ImageCell.ImageSourceProperty, "ImageSource");
            imageCell.Bind(Cell.IsSelectedProperty, "IsSelected", BindingMode.TwoWay);
            
            return imageCell;
        }

        public static ICell CreateCellWith(object caller, DataTemplate itemTemplate, object bindingContext, IListView container)
        {
            var instance = itemTemplate.Create(caller);
            var bindableObject = instance;
            var cell = instance as ICell;
            bindableObject.BindingContext = bindingContext as INotifyPropertyChanged;
            Debug.Assert(cell != null, "cell != null");
            cell.Container = container;
            return cell;
        }

        public static void ReUseCell(ICell entry, object bindingContext)
        {
            entry.ResetBindingContext(bindingContext as INotifyPropertyChanged);
            entry.RecycleWithNewBindingContext();
        }
    }
}