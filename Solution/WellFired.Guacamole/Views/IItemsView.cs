using System.Collections;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Views
{
    public interface IItemsView
    {
        IEnumerable ItemSource { get; }
        DataTemplate ItemTemplate { get; }
    }
}