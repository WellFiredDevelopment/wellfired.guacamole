using System.Collections.Generic;

namespace WellFired.Guacamole.Layouts
{
    public interface IHasChildren : IView
    {
        IList<ILayoutable> Children { get; }
    }
}