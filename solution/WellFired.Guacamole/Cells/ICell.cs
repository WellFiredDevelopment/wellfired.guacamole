using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Cells
{
    public interface ICell : IView, IBindableObject
    {
        IListView Container { set; }
        bool IsSelected { set; }
        void RecycleWithNewBindingContext();
    }
}