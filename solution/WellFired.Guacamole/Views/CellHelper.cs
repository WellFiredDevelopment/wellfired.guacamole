using System.ComponentModel;
using System.Diagnostics;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
    internal static class CellHelper
    {
        public static ICell CreateDefaultCell(object bindingContext, IListView container)
        {
            var cell = new LabelCell {
                Container = container
            };

            cell.BindingContext = bindingContext as INotifyPropertyChanged;
            cell.Bind(LabelCell.TextProperty, "CellLabelText");
            cell.Bind(Cell.IsSelectedProperty, "IsSelected", BindingMode.TwoWay);

            return cell;
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