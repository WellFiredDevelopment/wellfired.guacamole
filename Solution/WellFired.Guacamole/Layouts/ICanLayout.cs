using System.Collections.Generic;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Layouts
{
    public interface ICanLayout
    {
        IList<ILayoutable> Children { get; set; }
        void DoLayout();
    }
}