using System.ComponentModel;
using System.Diagnostics;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Views.BindingContexts;
using WellFired.Guacamole.Views.Cells;

namespace WellFired.Guacamole.Views
{
    internal static class CellHelper
    {
        public static ICell CreateDefaultCell(object bindingContext, IListView container, IStyleDictionary styleDictionary)
        {
            if (bindingContext is IDefaultCellContext defaultCellContext)
            {
                var labelCell = new LabelCell {
                    Container = container
                };

                labelCell.SetStyleDictionary(styleDictionary);
                labelCell.BindingContext = defaultCellContext;
                labelCell.Bind(LabelCell.TextProperty, "CellLabelText", BindingMode.ReadOnly);
                labelCell.Bind(Cell.IsSelectedProperty, "IsSelected", BindingMode.TwoWay);

                return labelCell;
            }
            
            var imageCell = new ImageCell
            {
                Container = container
            };
            
            imageCell.SetStyleDictionary(styleDictionary);
            imageCell.BindingContext = bindingContext as INotifyPropertyChanged;
            imageCell.Bind(ImageCell.ImageSourceProperty, "ImageSource");
            imageCell.Bind(Cell.IsSelectedProperty, "IsSelected", BindingMode.TwoWay);
            
            return imageCell;
        }

        public static ICell CreateCellWith(object caller, DataTemplate itemTemplate, object bindingContext, IListView container, IStyleDictionary styleDictionary)
        {
            var instance = itemTemplate.Create(caller);
            var bindableObject = instance;
            var cell = instance as ICell;
            cell.SetStyleDictionary(styleDictionary);
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