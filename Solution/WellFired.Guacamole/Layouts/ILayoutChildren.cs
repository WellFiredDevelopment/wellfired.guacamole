using System.Collections.Generic;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Layouts
{
    public interface ILayoutChildren
    {
        void Layout(IEnumerable<ILayoutable> layoutables, UIPadding containerPadding, LayoutOptions containerHorizontalLayoutOptions, LayoutOptions containerVerticalLayoutOptions);
        UIRect CalculateValidRextRequest(IEnumerable<ILayoutable> layoutables, UISize minSize);
    }
}