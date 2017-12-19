using System.Collections;
using System.Collections.Generic;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Views
{
    public interface IItemsView
    {
        IList ItemSource { get; }
        DataTemplate ItemTemplate { get; }
    }
    
    public interface IItemsView<T>
    {
        IEnumerable<T> ItemSource { get; }
        DataTemplate ItemTemplate { get; }
    }
}