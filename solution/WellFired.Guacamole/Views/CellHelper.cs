using System;
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
            string text = null;
            if (bindingContext != null)
                text = bindingContext.ToString();

            return new LabelCell
            {
                Text = text,
                Container = container
            };
        }

        public static ICell CreateCellWith(DataTemplate itemTemplate, object bindingContext, IListView container)
        {
            var instance = Activator.CreateInstance(itemTemplate.Type);
            var bindableObject = instance as IBindableObject;
            var layoutable = instance as ILayoutable;
            var cell = instance as ICell;
            
            Debug.Assert(bindableObject != null, "Data Template type does not implement IBindableObject.");
            Debug.Assert(layoutable != null, "Data Template type does not implement ICell.");
            Debug.Assert(cell != null, "cell != null");
            
            bindableObject.BindingContext = bindingContext as INotifyPropertyChanged;
            cell.Container = container;
            return cell;
        }

        public static void ReUseCell(ICell entry, object bindingContext)
        {
            entry.BindingContext = bindingContext as INotifyPropertyChanged;
            entry.RecycleWithNewBindingContext();
        }
    }
}