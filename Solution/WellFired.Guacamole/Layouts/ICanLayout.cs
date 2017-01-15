using System.Collections.Generic;

namespace WellFired.Guacamole.Layouts
{
    public interface ICanLayout
    {
        IList<ILayoutable> Children { get; set; }
        void DoLayout();
    }
}