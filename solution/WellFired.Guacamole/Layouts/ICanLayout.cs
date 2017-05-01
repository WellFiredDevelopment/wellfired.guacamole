using System.Collections.Generic;

namespace WellFired.Guacamole.Layouts
{
    public interface ICanLayout : IView
    {
        IList<ILayoutable> Children { get; }
        ILayoutChildren Layout { get; set; }
    }
}
